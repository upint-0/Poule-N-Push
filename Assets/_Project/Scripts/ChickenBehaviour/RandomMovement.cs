using UnityEngine;

public class RandomMovement : AChickenModule
{
    private float _nextChangeTime;

    public override void Initialize(ChickenCore chicken)
    {
        base.Initialize(chicken);

        Type = ChickenModuleType.RandomMovement;
    }

    public override void Execute(ChickenModuleData moduleData) { }

    public override void SetEnabled(bool isEnabled)
    {
        base.SetEnabled(isEnabled);

        if(isEnabled)
        {
            SelectNewBehaviour(canChangeState: false);
            _nextChangeTime = Time.time + Random.Range(_chicken.Data.MinWanderingChangePeriod, _chicken.Data.MaxWanderingChangePeriod);
        }
    }

    private void CheckForChange()
    {
        if(Time.time >= _nextChangeTime)
        {
            SelectNewBehaviour(canChangeState: true);
            _nextChangeTime = Time.time + Random.Range(_chicken.Data.MinWanderingChangePeriod, _chicken.Data.MaxWanderingChangePeriod);
        }
    }

    private void SelectNewBehaviour(bool canChangeState)
    {
        if(canChangeState && Random.value < _chicken.Data.ToIdleProbability)
        {
            _chicken.ChangeState(ChickenStateType.Idle);
        }
        else
        {
            ComputeNewMovement();
        }
    }

    private void ComputeNewMovement()
    {
        Vector2 randomDirection2d = Random.insideUnitCircle.normalized;
        ResultingDirection = new Vector3(randomDirection2d.x, 0f, randomDirection2d.y);
        ResultingSpeed = Random.Range(_chicken.Data.MinMetersPerSecond, _chicken.Data.MaxMetersPerSecond);
    }

    private void Update()
    {
        if(!IsEnabled)
        {
            return;
        }

        CheckForChange();
    }

    private void OnDrawGizmos()
    {
        if(!IsEnabled)
        {
            return;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + ResultingDirection * ResultingSpeed);
    }
}
