using System;
using UnityEngine;

[CreateAssetMenu]
public class ChickenData : ScriptableObject
{
    [SerializeField] private SerializableDictionary<ChickenState, ChickenMultipliers> _stateMultipliers;
    [SerializeField] private bool _mustComputePlayerAvoidance = true;
    [SerializeField] private bool _mustComputeWallAvoidance = true;
    [SerializeField] private bool _mustComputeVisibleCohesion = true;
    [SerializeField] private bool _mustComputeIdleBehaviour = true;
    [SerializeField] private bool _mustComputeGrainAttraction = true;
    [Header("Movement")]
    [SerializeField] private float _minMetersPerSecond = 1f;
    [SerializeField] private float _maxMetersPerSecond = 4f;
    [SerializeField] private float _rotationSpeed = 60f;
    [Header("Player avoidance")]
    [SerializeField] private float _playerAvoidanceRadius = 5f;
    [Header("Wall avoidance")]
    [SerializeField] private float _wallAvoidanceConeAngle = 30f;
    [SerializeField] private float _wallAvoidanceLength = 2f;
    [Header("IdleBehaviour")]
    [SerializeField] private float _minIdleChangePeriod = 2f;
    [SerializeField] private float _maxIdleChangePeriod = 10f;
    [SerializeField][Range(0f, 1f)] private float _immobilityProbability = 0.6f;
    [Header("GrainAttraction")]
    [SerializeField] private float _attractionRadius = 5f;
    [SerializeField] private float _attractionSpeed = 10f;

    public SerializableDictionary<ChickenState, ChickenMultipliers> StateMultipliers => _stateMultipliers;
    public float MinMetersPerSecond => _minMetersPerSecond;
    public float MaxMetersPerSecond => _maxMetersPerSecond;
    public bool MustComputePlayerAvoidance => _mustComputePlayerAvoidance;
    public bool MustComputeWallAvoidance => _mustComputeWallAvoidance;
    public bool MustComputeVisibleCohesion => _mustComputeVisibleCohesion;
    public bool MustComputeIdleBehaviour => _mustComputeIdleBehaviour;
    public bool MustComputeGrainAttraction => _mustComputeGrainAttraction;
    public float RotationSpeed => _rotationSpeed;
    public float PlayerAvoidanceRadius => _playerAvoidanceRadius;
    public float WallAvoidanceConeAngle => _wallAvoidanceConeAngle;
    public float WallAvoidanceLength => _wallAvoidanceLength;
    public float MinIdleChangePeriod => _minIdleChangePeriod;
    public float MaxIdleChangePeriod => _maxIdleChangePeriod;
    public float ImmobilityProbability => _immobilityProbability;
    public float AttractionRadius => _attractionRadius;
    public float AttractionSpeed => _attractionSpeed;
}

[Serializable]
public struct ChickenMultipliers
{
    [Range(0f, 10f)] public float Dzin; // probability
    [Range(0f, 10f)] public float PlayerAvoidance; // detection distance
    [Range(0f, 10f)] public float FoodAttraction; // detection distance
    [Range(0f, 10f)] public float OtherDzins; // detection distance
    [Range(0f, 10f)] public float OtherChickens; // detection distance
}
