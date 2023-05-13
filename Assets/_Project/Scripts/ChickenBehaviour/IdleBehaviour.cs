using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : MonoBehaviour
{
    private float _nextChangeTime;
    private ChickenCore _chicken;

    public void Initialize(ChickenCore chicken)
    {
        _chicken = chicken;
    }

    private void SelectNewBehaviour()
    {
        if(Random.value < _chicken.Data.ToWanderingProbability)
        {
            _chicken.ChangeState(ChickenState.Wandering);
        }
    }

    private void Update()
    {
        CheckForChange();
    }

    private void CheckForChange()
    {
        if(Time.time >= _nextChangeTime)
        {
            SelectNewBehaviour();
            _nextChangeTime += Random.Range(_chicken.Data.MinIdleChangePeriod, _chicken.Data.MaxIdleChangePeriod);
        }
    }

    public void ForceNextChangeTime(float period)
    {
        _nextChangeTime += period;
    }
}
