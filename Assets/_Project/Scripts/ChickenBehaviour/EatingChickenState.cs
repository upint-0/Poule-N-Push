using UnityEngine;

public class EatingChickenState : AChickenState
{
    public EatingChickenState(ChickenCore chickenCore) : base(chickenCore) { }

    protected override void SetState()
    {
        Type = ChickenState.Eating;
        // eating animation
    }

    protected override Vector3 ComputeDirection()
    {
        Vector3 direction = Vector3.zero;

        return direction;
    }

    protected override float ComputeSpeed()
    {
        return 0f;
    }
}
