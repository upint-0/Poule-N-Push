using UnityEngine;

public class IdleBehaviour : MonoBehaviour
{
    private float _nextChangeTime;
    private Vector3 _currentDirection;
    private float _currentSpeed;
    private ChickenData _chickenData;

    public void Initialize(ChickenData data)
    {
        _chickenData = data;
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
        bool must_be_immobile = Random.value < _chickenData.ImmobilityProbability;

        if(must_be_immobile)
        {
            _currentSpeed = 0f;
        }
        else
        {
            //_currentDirection = new Vector3( Random.Range( -1f, 1f ), 0, Random.Range( -1f, 1f ) );
            // Debug.Log( "HHHHHHHHHHHHHHHHHH" );



            _currentDirection = Random.insideUnitSphere;
            _currentDirection.y = 0;
            _currentSpeed = Random.Range(_chickenData.MinMetersPerSecond, _chickenData.MaxMetersPerSecond);
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
            _nextChangeTime += Random.Range(_chickenData.MinIdleChangePeriod, _chickenData.MaxIdleChangePeriod);
        }
    }

    public void ForceNextChangeTime(float period)
    {
        _nextChangeTime += period;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if(_currentSpeed == 0f)
        {
            Gizmos.DrawSphere(transform.position + Vector3.up * 1.5f, 0.1f);
        }
        else
        {
            Gizmos.DrawLine(transform.position, transform.position + _currentDirection);
        }
    }
}
