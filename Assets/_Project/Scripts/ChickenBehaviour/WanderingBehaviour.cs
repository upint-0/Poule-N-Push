using UnityEngine;

public class WanderingBehaviour : MonoBehaviour
{
    private float _nextChangeTime;
    private Vector3 _currentDirection;
    private float _currentSpeed;
    private ChickenCore _chicken;

    public void Initialize(ChickenCore chicken)
    {
        _chicken = chicken;
    }

    public Vector3 ComputeDirection()
    {
        return _currentDirection;
    }

    public float ComputeSpeed()
    {
        return _currentSpeed;
    }

    private void SelectNewBehaviour()
    {
        if(Random.value < _chicken.Data.ToIdleProbability)
        {
            _chicken.ChangeState(ChickenState.Idle);
        }
        else
        {
            //_currentDirection = new Vector3( Random.Range( -1f, 1f ), 0, Random.Range( -1f, 1f ) );
            // Debug.Log( "HHHHHHHHHHHHHHHHHH" );



            _currentDirection = Random.insideUnitSphere;
            _currentDirection.y = 0;
            _currentSpeed = Random.Range(_chicken.Data.MinMetersPerSecond, _chicken.Data.MaxMetersPerSecond);
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
            _nextChangeTime += Random.Range(_chicken.Data.MinWanderingChangePeriod, _chicken.Data.MaxWanderingChangePeriod);
        }
    }

    public void ForceNextChangeTime(float period)
    {
        _nextChangeTime += period;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + _currentDirection);
    }
}
