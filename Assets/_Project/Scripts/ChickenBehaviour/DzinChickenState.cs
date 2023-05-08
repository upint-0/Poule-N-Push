using UnityEngine;

public class DzinChickenState : AChickenState
{
    public DzinChickenState(ChickenCore chickenCore) : base(chickenCore) { }

    protected override void SetState()
    {
        State = ChickenState.Dzin;
    }

    protected override Vector3 ComputeDirection()
    {
        Vector3 direction = Vector3.zero;



        if(_chickenCore.MustComputeVisibleCohesion)
        {
            direction += _chickenCore.VisibleCohesion.ComputeDirection(_multipliers);
        }

        return direction;
    }

    protected override float ComputeSpeed()
    {
        return 0f;
    }
}
