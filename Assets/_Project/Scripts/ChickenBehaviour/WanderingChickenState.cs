using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingChickenState : AChickenState
{
    public WanderingChickenState(ChickenCore chickenCore) : base(chickenCore) { }

    protected override void SetState()
    {
        Type = ChickenState.Wandering;
    }

    protected override Vector3 ComputeDirection()
    {
        Vector3 direction = Vector3.zero;

        if(_chickenCore.Data.MustComputeWanderingBehaviour)
        {
            direction += _chickenCore.WanderingBehaviour.ComputeDirection();
        }

        if(_chickenCore.Data.MustComputeVisibleCohesion)
        {
            direction += _chickenCore.VisibleCohesion.ComputeDirection(_multipliers);
        }

        if(_chickenCore.Data.MustComputeGrainAttraction)
        {
            direction += _chickenCore.GrainAttraction.ComputeDirection(_multipliers);
        }

        return direction;
    }

    protected override float ComputeSpeed()
    {
        float speed = 0f;

        if(_chickenCore.Data.MustComputeWanderingBehaviour)
        {
            speed += _chickenCore.WanderingBehaviour.ComputeSpeed();
        }

        if(_chickenCore.Data.MustComputeGrainAttraction)
        {
            speed += _chickenCore.GrainAttraction.ComputeSpeed();
        }

        // la cohésion devrait pouvoir interrompre l’immobilité

        return speed;
    }
}
