using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChickenCore : MonoBehaviour
{
    [field: SerializeField] public ChickenData Data { get; private set; }

    public AChickenState CurrentState { get; private set; }
    public ChickenMovement Movement { get; private set; }
    public AChickenModule[] ChickenModules { get; private set; }
    public AChickenModule this[ChickenModuleType type] => Array.Find(ChickenModules, module => module.Type == type);

    public void ChangeState(ChickenStateType stateType)
    {
        if(!Data[stateType].IsEnabled)
        {
            Debug.LogWarning($"{stateType} is not enabled");

            return;
        }

        switch(stateType)
        {
            case ChickenStateType.Idle:
                CurrentState = new IdleChickenState(this);
                break;
            case ChickenStateType.Wandering:
                CurrentState = new WanderingChickenState(this);
                break;
            case ChickenStateType.Dzin:
                CurrentState = new DzinChickenState(this);
                break;
            case ChickenStateType.Eating:
                CurrentState = new EatingChickenState(this);
                break;
        }
    }

    private void Awake()
    {
        Movement = GetComponent<ChickenMovement>();
        Movement.Initialize(Data);

        ChickenModules = GetComponentsInChildren<AChickenModule>();

        foreach( AChickenModule module in ChickenModules)
        {
            module.Initialize(this);
        }

        CurrentState = new IdleChickenState(this);
    }

    private void Update()
    {
        CurrentState.ExecuteModules();
        Movement.ApplyMovement();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = CurrentState.Type switch
        {
            ChickenStateType.Idle => Color.blue,
            ChickenStateType.Wandering => Color.green,
            ChickenStateType.Dzin => Color.red,
            ChickenStateType.Eating => Color.yellow,
            _ => Color.black
        };

        Gizmos.DrawSphere(transform.position + Vector3.up * 1.5f, 0.1f);
    }

    public static Vector2 GetFlattenedVector(Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }
}