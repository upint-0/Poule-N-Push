public class DzinChickenState : AChickenState
{
    public DzinChickenState(ChickenCore chicken) : base(chicken) { }

    protected override void SetState()
    {
        Type = ChickenStateType.Dzin;
    }
}
