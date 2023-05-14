using UnityEngine;

public class IdleBehaviour : AChickenModule
{
    private float _nextChangeTime;

    public override void Execute(ChickenModuleData moduleData) { }

    private void CheckForChange()
    {
        if(Time.time >= _nextChangeTime)
        {
            SelectNewBehaviour();
            _nextChangeTime += Random.Range(_chicken.Data.MinIdleChangePeriod, _chicken.Data.MaxIdleChangePeriod);
        }
    }

    private void SelectNewBehaviour()
    {
        if(Random.value < _chicken.Data.ToWanderingProbability)
        {
            _chicken.ChangeState(ChickenStateType.Wandering);
        }
    }

    public void ForceNextChangeTime(float period)
    {
        _nextChangeTime += period;
    }

    private void Update()
    {
        CheckForChange();
    }
}
