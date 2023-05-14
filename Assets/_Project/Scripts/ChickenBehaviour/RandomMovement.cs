using UnityEngine;

public class RandomMovement : AChickenModule
{
    private float _nextChangeTime;

    public override void Execute(ChickenModuleData moduleData) { }

    private void CheckForChange()
    {
        if(Time.time >= _nextChangeTime)
        {
            SelectNewBehaviour();
            _nextChangeTime += Random.Range(_chicken.Data.MinWanderingChangePeriod, _chicken.Data.MaxWanderingChangePeriod);
        }
    }

    private void SelectNewBehaviour()
    {
        if(Random.value < _chicken.Data.ToIdleProbability)
        {
            _chicken.ChangeState(ChickenStateType.Idle);
        }
        else
        {
            Vector2 randomDirection2d = Random.insideUnitCircle.normalized;
            ResultingDirection = new Vector3(randomDirection2d.x, 0f, randomDirection2d.y);
            ResultingSpeed = Random.Range(_chicken.Data.MinMetersPerSecond, _chicken.Data.MaxMetersPerSecond);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + ResultingDirection);
    }
}
