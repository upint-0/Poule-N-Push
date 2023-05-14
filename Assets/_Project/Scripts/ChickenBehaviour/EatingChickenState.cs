public class EatingChickenState : AChickenState
{
    public EatingChickenState(ChickenCore chicken) : base(chicken) { }

    protected override void SetState()
    {
        Type = ChickenStateType.Eating;
        // eating animation
    }

    public override void ExecuteModules()
    {
        base.ExecuteModules();

        //use events?
        if(_chicken[ChickenModuleType.PlayerAvoidance].ResultingSpeed > 0f)
        {
            _chicken.ChangeState(ChickenStateType.Eating);
        }
    }
}
