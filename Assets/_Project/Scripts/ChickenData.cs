using System;
using UnityEngine;

[CreateAssetMenu]
public class ChickenData : ScriptableObject
{
    [Header("States")]
    [SerializeField] private SerializableDictionary<ChickenStateType, ChickenStateData> _stateDataMap;
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
    [Header("Random wait")]
    [SerializeField] private float _minIdleChangePeriod = 2f;
    [SerializeField] private float _maxIdleChangePeriod = 10f;
    [SerializeField][Range(0f, 1f)] private float _toWanderingProbability = 0.6f;
    [Header("Random movement")]
    [SerializeField] private float _minWanderingChangePeriod = 2f;
    [SerializeField] private float _maxWanderingChangePeriod = 10f;
    [SerializeField][Range(0f, 1f)] private float _toIdleProbability = 0.6f;
    [Header("Grain attraction")]
    [SerializeField] private float _attractionRadius = 5f;
    [SerializeField] private float _attractionSpeed = 10f;

    public ChickenStateData this[ChickenStateType state] => _stateDataMap[state];
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
public struct ChickenStateData
{
    [SerializeField] private bool _isEnabled;
    [SerializeField] private SerializableDictionary<ChickenModuleType, ChickenModuleData> _moduleDataMap;

    public bool IsEnabled => _isEnabled;
    public ChickenModuleData this[ChickenModuleType module] => _moduleDataMap[module];
}

[Serializable]
public struct ChickenModuleData
{
    [SerializeField] private bool _isEnabled;
    [Tooltip("Determines the movement influence over the other modules")]
    [SerializeField] private float _weight;
    [Tooltip(
        "- PlayerAvoidance: detection distance multiplier\r\n" +
        "- VisibleCohesion: detection distance multiplier\r\n" +
        "- GrainAttraction: detection distance multiplier"
    )]
    [SerializeField] private float _multiplier;

    public bool IsEnabled => _isEnabled;
    public float Weight => _weight;
    public float Multiplier => _multiplier;
}