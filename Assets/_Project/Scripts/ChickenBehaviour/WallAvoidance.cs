using UnityEngine;

public class WallAvoidance : AChickenModule
{
    public override void Execute(ChickenModuleData moduleData)
    {
        Vector3 directionRight = Quaternion.Euler(0, _chicken.Data.WallAvoidanceConeAngle, 0) * transform.forward * _chicken.Data.WallAvoidanceLength;
        Vector3 directionForward = transform.forward * _chicken.Data.WallAvoidanceLength;
        Vector3 directionLeft = Quaternion.Euler(0, -_chicken.Data.WallAvoidanceConeAngle, 0) * transform.forward * _chicken.Data.WallAvoidanceLength;

        Vector3 normalAverage = Vector3.zero;

        //bool right = Physics.Raycast(transform.position, directionRight, out RaycastHit hitInfo, Length, LayerMask.GetMask("Wall"));

        //if(right)
        //{
        //    normalAverage += hitInfo.normal;
        //}

        bool forward = Physics.Raycast(transform.position, directionForward, out RaycastHit hitInfo, _chicken.Data.WallAvoidanceLength, LayerMask.GetMask("Wall"));

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

        //if ( right || forward || left )
        //{
        //    finalDir = normalAverage;
        //    Debug.DrawRay( transform.position, finalDir, Color.red );
        //}
        if(forward)
        {
            Debug.DrawRay(transform.position, finalDir, Color.red);
            ResultingDirection = normalAverage;
        }
    }

    public LayerMask m_allowCollision;
    public bool IsHittingWall(ChickenModuleData moduleData)
    {
        Debug.DrawLine(transform.position, transform.position + directionForward * 2f, Color.red);
        Vector3 directionForward = transform.forward * _chicken.Data.WallAvoidanceLength;

        bool forward = Physics.Raycast(transform.position, directionForward, out RaycastHit hitInfo, _chicken.Data.WallAvoidanceLength, m_allowCollision);
        //Debug.Log( "What ? "+forward );
        return forward;
    }
}
