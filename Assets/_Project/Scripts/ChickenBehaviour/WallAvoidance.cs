using UnityEngine;

public class WallAvoidance : AChickenModule
{
    private Vector3 _rightDetection;
    private Vector3 _forwardDetection;
    private Vector3 _leftDetection;
    private bool _rightIsDetecting;
    private bool _forwardIsDetecting;
    private bool _leftIsDetecting;
    private float _nextDetectionTime;

    public override void Initialize(ChickenCore chicken)
    {
        base.Initialize(chicken);

        Type = ChickenModuleType.WallAvoidance;
    }

    public override void Execute(ChickenModuleData moduleData)
    {
        if(Time.time < _nextDetectionTime)
        {
            return;
        }

        Detect();

        if(ResultingDirection != Vector3.zero)
        {
            _nextDetectionTime = Time.time + _chicken.Data.WallAvoidanceDuration;
        }
    }

    public override void SetEnabled(bool isEnabled)
    {
        base.SetEnabled(isEnabled);

        if(isEnabled)
        {
            _nextDetectionTime = Time.time;
        }
    }

    private void Detect()
    {
        _rightDetection = Quaternion.Euler(0, _chicken.Data.WallAvoidanceConeAngle, 0) * transform.forward * _chicken.Data.WallAvoidanceLength;
        _forwardDetection = transform.forward * _chicken.Data.WallAvoidanceLength;
        _leftDetection = Quaternion.Euler(0, -_chicken.Data.WallAvoidanceConeAngle, 0) * transform.forward * _chicken.Data.WallAvoidanceLength;

        Vector3 normalAverage = Vector3.zero;

        _rightIsDetecting = Physics.Raycast(transform.position, _rightDetection, out RaycastHit hitInfo, _chicken.Data.WallAvoidanceLength, LayerMask.GetMask("Wall"));

        if(_rightIsDetecting)
        {
            normalAverage += hitInfo.normal;
        }

        _forwardIsDetecting = Physics.Raycast(transform.position, _forwardDetection, out hitInfo, _chicken.Data.WallAvoidanceLength, LayerMask.GetMask("Wall"));

        if(_forwardIsDetecting)
        {
            normalAverage += hitInfo.normal;
        }

        _leftIsDetecting = Physics.Raycast(transform.position, _leftDetection, out hitInfo, _chicken.Data.WallAvoidanceLength, LayerMask.GetMask("Wall"));

        if(_leftIsDetecting)
        {
            normalAverage += hitInfo.normal;
        }

        ResultingDirection = normalAverage.normalized;
    }

    private void OnDrawGizmos()
    {
        if(!IsEnabled)
        {
            return;
        }

        //final redirection
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + ResultingDirection);
    }

    private void OnDrawGizmosSelected()
    {
        if(!IsEnabled)
        {
            return;
        }

        //current detections
        Gizmos.color = new Color(0.3f, 0.3f, 0.3f);

        if(_rightIsDetecting)
        {
            Gizmos.DrawLine(transform.position, transform.position + _rightDetection);
        }

        if(_forwardIsDetecting)
        {
            Gizmos.DrawLine(transform.position, transform.position + _forwardDetection);
        }

        if(_leftIsDetecting)
        {
            Gizmos.DrawLine(transform.position, transform.position + _leftDetection);
        }
    }
}
