using UnityEngine;

public class ChickenCore : MonoBehaviour
{
    [field: SerializeField] public ChickenData Data { get; private set; }

    public AChickenState CurrentState { get; set; }
    public ChickenMovement Movement { get; private set; }
    public PlayerAvoidance PlayerAvoidance { get; private set; }
    public WallAvoidance WallAvoidance { get; private set; }
    public IdleBehaviour IdleBehaviour { get; private set; }
    public VisibleCohesion VisibleCohesion { get; private set; }
    public GrainAttraction GrainAttraction { get; private set; }

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

    public static Vector2 GetFlattenedVector(Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }
}