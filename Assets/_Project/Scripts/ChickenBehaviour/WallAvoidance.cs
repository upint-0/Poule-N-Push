using UnityEngine;

public class WallAvoidance : MonoBehaviour
{
    private ChickenData _chickenData;

    public void Initialize(ChickenData data)
    {
        _chickenData = data;
    }

    public Vector3 ComputeDirection(ChickenMultipliers multipliers)
    {
        Vector3 directionRight = Quaternion.Euler(0, _chickenData.WallAvoidanceConeAngle, 0) * transform.forward * _chickenData.WallAvoidanceLength;
        Vector3 directionForward = transform.forward * _chickenData.WallAvoidanceLength;
        Vector3 directionLeft = Quaternion.Euler(0, -_chickenData.WallAvoidanceConeAngle, 0) * transform.forward * _chickenData.WallAvoidanceLength;

        Vector3 normalAverage = Vector3.zero;

        //bool right = Physics.Raycast(transform.position, directionRight, out RaycastHit hitInfo, Length, LayerMask.GetMask("Wall"));

        //if(right)
        //{
        //    normalAverage += hitInfo.normal;
        //}

        bool forward = Physics.Raycast(transform.position, directionForward, out RaycastHit hitInfo, _chickenData.WallAvoidanceLength, LayerMask.GetMask("Wall"));

        if(forward)
        {
            normalAverage += hitInfo.normal;
        }

        //bool left = Physics.Raycast(transform.position, directionLeft, out hitInfo, Length, LayerMask.GetMask("Wall"));

        //if(left)
        //{
        //    normalAverage += hitInfo.normal;
        //}

        // Debug.DrawRay(transform.position, directionRight, right ? Color.black : Color.white);
        Debug.DrawRay(transform.position, directionForward, forward ? Color.black : Color.white);
        //Debug.DrawRay(transform.position, directionLeft, left ? Color.black : Color.white);

        Vector3 finalDir = Vector3.zero;

        //if ( right || forward || left )
        //{
        //    finalDir = normalAverage;
        //    Debug.DrawRay( transform.position, finalDir, Color.red );
        //}
        if(forward)
        {
            finalDir = normalAverage;
            Debug.DrawRay(transform.position, finalDir, Color.red);
        }

        return finalDir;
    }


    public LayerMask m_allowCollision;
    public bool IsHittingWall(ChickenMultipliers multipliers)
    {
        Vector3 directionForward = transform.forward * _chickenData.WallAvoidanceLength;
        Debug.DrawLine(transform.position, transform.position + directionForward * 2f, Color.red);

        bool forward = Physics.Raycast(transform.position, directionForward, out RaycastHit hitInfo, _chickenData.WallAvoidanceLength, m_allowCollision);
        //Debug.Log( "What ? "+forward );
        return forward;
    }
}
