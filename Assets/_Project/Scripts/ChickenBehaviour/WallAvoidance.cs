using UnityEngine;

public class WallAvoidance : AChickenModule
{
    private Vector3 rightDetection;
    private Vector3 forwardDetection;
    private Vector3 leftDetection;
    private bool rightIsDetecting;
    private bool forwardIsDetecting;
    private bool leftIsDetecting;

    public override void Initialize(ChickenCore chicken)
    {
        base.Initialize(chicken);

        Type = ChickenModuleType.WallAvoidance;
    }

    public override void Execute(ChickenModuleData moduleData)
    {
        rightDetection = Quaternion.Euler(0, _chicken.Data.WallAvoidanceConeAngle, 0) * transform.forward * _chicken.Data.WallAvoidanceLength;
        forwardDetection = transform.forward * _chicken.Data.WallAvoidanceLength;
        leftDetection = Quaternion.Euler(0, -_chicken.Data.WallAvoidanceConeAngle, 0) * transform.forward * _chicken.Data.WallAvoidanceLength;

        Vector3 normalAverage = Vector3.zero;

        rightIsDetecting = Physics.Raycast(transform.position, rightDetection, out RaycastHit hitInfo, _chicken.Data.WallAvoidanceLength, LayerMask.GetMask("Wall"));

        if(rightIsDetecting)
        {
            normalAverage += hitInfo.normal;
        }

        forwardIsDetecting = Physics.Raycast(transform.position, forwardDetection, out hitInfo, _chicken.Data.WallAvoidanceLength, LayerMask.GetMask("Wall"));

        if(forwardIsDetecting)
        {
            normalAverage += hitInfo.normal;
        }

        leftIsDetecting = Physics.Raycast(transform.position, leftDetection, out hitInfo, _chicken.Data.WallAvoidanceLength, LayerMask.GetMask("Wall"));

        if(leftIsDetecting)
        {
            normalAverage += hitInfo.normal;
        }

        ResultingDirection = normalAverage.normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + ResultingDirection);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = rightIsDetecting ? Color.magenta * new Color(1f, 1f, 1f, 0.5f) : Color.magenta * new Color(1f, 1f, 1f, 0.2f);
        Gizmos.DrawLine(transform.position, transform.position + rightDetection);

        Gizmos.color = forwardIsDetecting ? Color.magenta * new Color(1f, 1f, 1f, 0.5f) : Color.magenta * new Color(1f, 1f, 1f, 0.2f);
        Gizmos.DrawLine(transform.position, transform.position + forwardDetection);

        Gizmos.color = leftIsDetecting ? Color.magenta * new Color(1f, 1f, 1f, 0.5f) : Color.magenta * new Color(1f, 1f, 1f, 0.2f);
        Gizmos.DrawLine(transform.position, transform.position + leftDetection);
    }
}
