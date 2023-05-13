using UnityEngine;

public class DzinChickenState : AChickenState
{
    public DzinChickenState(ChickenCore chickenCore) : base(chickenCore) { }

    protected override void SetState()
    {
        Type = ChickenState.Dzin;
    }

    protected override Vector3 ComputeDirection()
    {
        Vector3 direction = Vector3.zero;



        if(_chickenCore.Data.MustComputeVisibleCohesion)
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
