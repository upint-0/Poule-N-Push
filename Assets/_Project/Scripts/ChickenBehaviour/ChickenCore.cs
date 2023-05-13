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
    public VisibleCohesion VisibleCohesion { get; private set; }
    public GrainAttraction GrainAttraction { get; private set; }

    public void ChangeState(ChickenState stateType)
    {
        switch(stateType)
        {
            case ChickenState.Idle:
                CurrentState = new IdleChickenState(this);
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
        CurrentState = new IdleChickenState(this);
        Movement = GetComponent<ChickenMovement>();
        Movement.Initialize(Data);
        PlayerAvoidance = GetComponent<PlayerAvoidance>();
        PlayerAvoidance.Initialize(Data);
        WallAvoidance = GetComponent<WallAvoidance>();
        WallAvoidance.Initialize(Data);
        IdleBehaviour = GetComponent<IdleBehaviour>();
        IdleBehaviour.Initialize(Data);
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
        switch(CurrentState.Type)
        {
            case ChickenState.Idle:
                //gizmo set in IdleBehaviour only when not moving
                //immobility could be a state on itself
                break;
            case ChickenState.Dzin:
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(transform.position + Vector3.up * 1.5f, 0.1f);
                break;
            case ChickenState.Eating:
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(transform.position + Vector3.up * 1.5f, 0.1f);
                break;
        }
    }

    public static Vector2 GetFlattenedVector(Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }
}