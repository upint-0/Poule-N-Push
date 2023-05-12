using UnityEngine;

public class IdleChickenState : AChickenState
{
    public IdleChickenState(ChickenCore chickenCore) : base(chickenCore) { }

    protected override void SetState()
    {
        State = ChickenState.Idle;
    }

    protected override Vector3 ComputeDirection()
    {
        Vector3 direction = Vector3.zero;

        if(_chickenCore.Data.MustComputeIdleBehaviour)
        {
            direction += _chickenCore.IdleBehaviour.ComputeDirection();
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

        if(_chickenCore.Data.MustComputeIdleBehaviour)
        {
            speed += _chickenCore.IdleBehaviour.ComputeSpeed();
        }

        if(_chickenCore.Data.MustComputeGrainAttraction)
        {
            speed += _chickenCore.GrainAttraction.ComputeSpeed();
        }

        // la coh�sion devrait pouvoir interrompre l�immobilit�

        return speed;
    }
}
