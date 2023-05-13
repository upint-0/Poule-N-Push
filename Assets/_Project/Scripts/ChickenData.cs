using System;
using UnityEngine;

[CreateAssetMenu]
public class ChickenData : ScriptableObject
{
    [SerializeField] private SerializableDictionary<ChickenState, ChickenMultipliers> _stateMultipliers;
    [SerializeField] private bool _canBeInWanderingState = true;
    [SerializeField] private bool _canBeInDzinState = true;
    [SerializeField] private bool _canBeInEatingState = true;
    [SerializeField] private bool _mustComputePlayerAvoidance = true;
    [SerializeField] private bool _mustComputeWallAvoidance = true;
    [SerializeField] private bool _mustComputeVisibleCohesion = true;
    [SerializeField] private bool _mustComputeIdleBehaviour = true;
    [SerializeField] private bool _mustComputeWanderingBehaviour = true;
    [SerializeField] private bool _mustComputeGrainAttraction = true;
    [Header("Movement")]
    [SerializeField] private float _minMetersPerSecond = 1f;
    [SerializeField] private float _maxMetersPerSecond = 4f;
    [SerializeField] private float _rotationSpeed = 60f;
    [Header("Player avoidance")]
    [SerializeField] private float _farthestPlayerDetection = 5f;
    [SerializeField] private float _distanceForMatchingSpeed = 2f;
    [Header("Wall avoidance")]
    [SerializeField] private float _wallAvoidanceConeAngle = 30f;
    [SerializeField] private float _wallAvoidanceLength = 2f;
    [Header("Idle behaviour")]
    [SerializeField] private float _minIdleChangePeriod = 2f;
    [SerializeField] private float _maxIdleChangePeriod = 10f;
    [SerializeField][Range(0f, 1f)] private float _toWanderingProbability = 0.6f;
    [Header("Wandering behaviour")]
    [SerializeField] private float _minWanderingChangePeriod = 2f;
    [SerializeField] private float _maxWanderingChangePeriod = 10f;
    [SerializeField][Range(0f, 1f)] private float _toIdleProbability = 0.6f;
    [Header("Grain attraction")]
    [SerializeField] private float _attractionRadius = 5f;
    [SerializeField] private float _attractionSpeed = 10f;

    public SerializableDictionary<ChickenState, ChickenMultipliers> StateMultipliers => _stateMultipliers;
    public bool CanBeInWanderingState => _canBeInWanderingState;
    public bool CanBeInDzinState => _canBeInDzinState;
    public bool CanBeInEatingState => _canBeInEatingState;
    public bool MustComputePlayerAvoidance => _mustComputePlayerAvoidance;
    public bool MustComputeWallAvoidance => _mustComputeWallAvoidance;
    public bool MustComputeVisibleCohesion => _mustComputeVisibleCohesion;
    public bool MustComputeIdleBehaviour => _mustComputeIdleBehaviour;
    public bool MustComputeWanderingBehaviour => _mustComputeWanderingBehaviour;
    public bool MustComputeGrainAttraction => _mustComputeGrainAttraction;
    public float MinMetersPerSecond => _minMetersPerSecond;
    public float MaxMetersPerSecond => _maxMetersPerSecond;
    public float RotationSpeed => _rotationSpeed;
    public float FarthestPlayerDetection => _farthestPlayerDetection;
    public float DistanceForMatchingSpeed => _distanceForMatchingSpeed;
    public float WallAvoidanceConeAngle => _wallAvoidanceConeAngle;
    public float WallAvoidanceLength => _wallAvoidanceLength;
    public float MinIdleChangePeriod => _minIdleChangePeriod;
    public float MaxIdleChangePeriod => _maxIdleChangePeriod;
    public float ToWanderingProbability => _toWanderingProbability;
    public float MinWanderingChangePeriod => _minWanderingChangePeriod;
    public float MaxWanderingChangePeriod => _maxWanderingChangePeriod;
    public float ToIdleProbability => _toIdleProbability;
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
