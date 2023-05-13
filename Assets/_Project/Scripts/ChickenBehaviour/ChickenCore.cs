using UnityEditor;
using UnityEngine;

public class ChickenCore : MonoBehaviour
{
    [field: SerializeField] public ChickenData Data { get; private set; }

    public AChickenState CurrentState { get; private set; }
    public ChickenMovement Movement { get; private set; }
    public PlayerAvoidance PlayerAvoidance { get; private set; }
    public WallAvoidance WallAvoidance { get; private set; }
    public IdleBehaviour IdleBehaviour { get; private set; }
    public WanderingBehaviour WanderingBehaviour { get; private set; }
    public VisibleCohesion VisibleCohesion { get; private set; }
    public GrainAttraction GrainAttraction { get; private set; }

    public void ChangeState(ChickenState stateType)
    {
        switch(stateType)
        {
            case ChickenState.Idle:
                CurrentState = new IdleChickenState(this);
                break;
            case ChickenState.Wandering:
                if(Data.CanBeInWanderingState)
                {
                    CurrentState = new WanderingChickenState(this);
                }
                break;
            case ChickenState.Dzin:
                if(Data.CanBeInDzinState)
                {
                    CurrentState = new DzinChickenState(this);
                }
                break;
            case ChickenState.Eating:
                if(Data.CanBeInEatingState)
                {
                    CurrentState = new EatingChickenState(this);
                }
                break;
        }
    }

    private void Awake()
    {
        if(Data.CanBeInWanderingState)
        {
            CurrentState = Random.value > 0.5f ? new IdleChickenState(this) : new WanderingChickenState(this);
        }
        else
        {
            CurrentState = new IdleChickenState(this);
        }

        Movement = GetComponent<ChickenMovement>();
        Movement.Initialize(Data);
        PlayerAvoidance = GetComponent<PlayerAvoidance>();
        PlayerAvoidance.Initialize(Data);
        WallAvoidance = GetComponent<WallAvoidance>();
        WallAvoidance.Initialize(Data);
        IdleBehaviour = GetComponent<IdleBehaviour>();
        IdleBehaviour.Initialize(this);
        WanderingBehaviour = GetComponent<WanderingBehaviour>();
        WanderingBehaviour.Initialize(this);
        VisibleCohesion = GetComponentInChildren<VisibleCohesion>();
        GrainAttraction = GetComponent<GrainAttraction>();
        GrainAttraction.Initialize(Data);
    }

    private void Update()
    {
        CurrentState.ApplyBehaviour();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = CurrentState.Type switch
        {
            ChickenState.Idle => Color.blue,
            ChickenState.Wandering => Color.green,
            ChickenState.Dzin => Color.red,
            ChickenState.Eating => Color.yellow,
            _ => Color.black
        };

        Gizmos.DrawSphere(transform.position + Vector3.up * 1.5f, 0.1f);
    }

    public static Vector2 GetFlattenedVector(Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }
}