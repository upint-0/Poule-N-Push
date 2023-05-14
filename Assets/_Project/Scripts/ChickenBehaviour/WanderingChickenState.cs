public class WanderingChickenState : AChickenState
{
    public WanderingChickenState(ChickenCore chicken) : base(chicken) { }

    protected override void SetState()
    {
        Type = ChickenStateType.Wandering;
    }
}
