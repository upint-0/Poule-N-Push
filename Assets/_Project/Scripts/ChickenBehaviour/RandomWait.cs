using UnityEngine;

public class RandomWait : AChickenModule
{
    private float _nextChangeTime;

    public override void Initialize(ChickenCore chicken)
    {
        base.Initialize(chicken);

        Type = ChickenModuleType.RandomWait;
    }

    public override void Execute(ChickenModuleData moduleData) { }

    public override void SetEnabled(bool isEnabled)
    {
        base.SetEnabled(isEnabled);

        if(isEnabled)
        {
            _nextChangeTime = Time.time + Random.Range(_chicken.Data.MinIdleChangePeriod, _chicken.Data.MaxIdleChangePeriod);
        }
    }

    private void CheckForChange()
    {
        if(Time.time >= _nextChangeTime)
        {
            SelectNewBehaviour();
            _nextChangeTime = Time.time + Random.Range(_chicken.Data.MinIdleChangePeriod, _chicken.Data.MaxIdleChangePeriod);
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
        _nextChangeTime = Time.time + period;
    }

    private void Update()
    {
        if(!IsEnabled)
        {
            return;
        }

        CheckForChange();
    }
}
