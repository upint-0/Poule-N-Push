using System;
using UnityEngine;

[Serializable]
public struct ChickenMultipliers
{
    [Range(0f, 10f)] public float Dzin; // probability
    [Range(0f, 10f)] public float PlayerAvoidance; // detection distance
    [Range(0f, 10f)] public float FoodAttraction; // detection distance
    [Range(0f, 10f)] public float OtherDzins; // detection distance
    [Range(0f, 10f)] public float OtherChickens; // detection distance
}

public class ChickenCore : MonoBehaviour
{
    [field: SerializeField] public SerializableDictionary<ChickenState, ChickenMultipliers> StateMultipliers { get; private set; }
    [field: SerializeField] public float MinMetersPerSecond { get; private set; }
    [field: SerializeField] public float MaxMetersPerSecond { get; private set; }
    [field: SerializeField] public bool MustComputePlayerAvoidance { get; private set; }
    [field: SerializeField] public bool MustComputeWallAvoidance { get; private set; }
    [field: SerializeField] public bool MustComputeVisibleCohesion { get; private set; }
    [field: SerializeField] public bool MustComputeIdleBehaviour { get; private set; }
    [field: SerializeField] public bool MustComputeGrainAttraction { get; private set; }

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
        PlayerAvoidance = GetComponent<PlayerAvoidance>();
        WallAvoidance = GetComponent<WallAvoidance>();
        IdleBehaviour = GetComponent<IdleBehaviour>();
        IdleBehaviour.Initialize(MinMetersPerSecond, MaxMetersPerSecond);
        VisibleCohesion = GetComponentInChildren<VisibleCohesion>();
        GrainAttraction = GetComponent<GrainAttraction>();
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