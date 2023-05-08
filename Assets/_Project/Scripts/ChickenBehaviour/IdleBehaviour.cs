using UnityEngine;

public class IdleBehaviour : MonoBehaviour
{
    [SerializeField] private float _minChangePeriod;
    [SerializeField] private float _maxChangePeriod;
    [SerializeField][Range(0f, 1f)] private float _immobilityProbability;

    private float _nextChangeTime;
    private Vector3 _currentDirection;
    private float _currentSpeed;
    private float _minMetersPerSecond;
    private float _maxMetersPerSecond;

    public void Initialize(float minMetersPerSecond, float maxMetersPerSecond)
    {
        _minMetersPerSecond = minMetersPerSecond;
        _maxMetersPerSecond = maxMetersPerSecond;
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
        bool must_be_immobile = Random.value < _immobilityProbability;

        if (must_be_immobile)
        {
            _currentSpeed = 0f;
        }
        else
        {
            //_currentDirection = new Vector3( Random.Range( -1f, 1f ), 0, Random.Range( -1f, 1f ) );
            // Debug.Log( "HHHHHHHHHHHHHHHHHH" );



            _currentDirection = Random.insideUnitSphere;
            _currentDirection.y = 0;
            _currentSpeed = Random.Range(_minMetersPerSecond, _maxMetersPerSecond);
            Debug.DrawLine( transform.position, transform.position + _currentDirection, Color.green ,10);
            ;
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
            _nextChangeTime += Random.Range(_minChangePeriod, _maxChangePeriod);
        }
    }

    public void ForceNextChangeTime(float period)
    {
        _nextChangeTime += period;
    }
}
