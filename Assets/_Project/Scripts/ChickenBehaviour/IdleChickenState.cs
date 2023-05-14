public class IdleChickenState : AChickenState
{
    public IdleChickenState(ChickenCore chicken) : base(chicken) { }

    protected override void SetState()
    {
        Type = ChickenStateType.Idle;
    }

    // la cohésion devrait pouvoir interrompre l’immobilité
}
