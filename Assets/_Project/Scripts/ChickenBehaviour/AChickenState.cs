using UnityEngine;

public enum ChickenState
{
    Idle,
    Dzin,
    Eating,
}

public abstract class AChickenState
{
    public ChickenState State { get; set; }
    protected ChickenCore _chickenCore;
    protected ChickenMultipliers _multipliers;

    public AChickenState(ChickenCore chickenCore)
    {
        _chickenCore = chickenCore;
        SetState();
        _multipliers = _chickenCore.StateMultipliers[State];
    }

    public void ApplyBehaviour()
    {
        // probabilitÚ de dzin en fonction de la distance du joueur et de la distance des poulets dzinÚs

        if (_chickenCore.MustComputeWallAvoidance)
        {
            Vector3 wallAvoidanceDirection = _chickenCore.WallAvoidance.ComputeDirection(_multipliers);

            if (wallAvoidanceDirection != Vector3.zero)
            {
                _chickenCore.Movement.SetCurrentDirection( wallAvoidanceDirection);
                _chickenCore.transform.forward = wallAvoidanceDirection;
                _chickenCore.IdleBehaviour.ForceNextChangeTime(1.0f);

                return;
            }
        }

        if (_chickenCore.MustComputePlayerAvoidance)
        {
            Vector3 playerAvoidanceDirection = _chickenCore.PlayerAvoidance.ComputeDirection(_multipliers);
            _chickenCore.Movement.SetCurrentDirection( playerAvoidanceDirection);
            float playerAvoidanceSpeed = _chickenCore.PlayerAvoidance.ComputeSpeed();

            if (_chickenCore.Movement.CurrentSpeed < playerAvoidanceSpeed)
            {
                _chickenCore.Movement.CurrentSpeed = playerAvoidanceSpeed;
            }

            if (playerAvoidanceDirection != Vector3.zero)
            {
                if (_chickenCore.CurrentState.State == ChickenState.Eating)
                {
                    _chickenCore.CurrentState = new IdleChickenState(_chickenCore);
                }

                return;
            }
        }

        _chickenCore.Movement.SetCurrentDirection( ComputeDirection());
        _chickenCore.Movement.CurrentSpeed = ComputeSpeed();
    }

    protected abstract Vector3 ComputeDirection();
    protected abstract float ComputeSpeed();
    protected abstract void SetState();
}
