using System;
using UnityEngine;

[Serializable]
public enum ChickenModuleType
{
    IdleBehaviour = 0,
    WanderingBehaviour = 1,
    PlayerAvoidance = 2,
    WallAvoidance = 3,
    VisibleCohesion = 4,
    GrainAttraction = 5,
    DzinRisk = 6,
    DzinDetection = 7,
}

public abstract class AChickenModule : MonoBehaviour
{
    protected ChickenCore _chicken;
    public ChickenModuleType Type { get; private set; }
    public Vector3 ResultingDirection { get; protected set; }
    public float ResultingSpeed { get; protected set; }

    public virtual void Initialize(ChickenCore chicken)
    {
        _chicken = chicken;
    }

    public abstract void Execute(ChickenModuleData moduleData);
}
