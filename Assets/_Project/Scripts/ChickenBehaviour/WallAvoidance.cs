using UnityEngine;

public class WallAvoidance : MonoBehaviour
{
    [SerializeField] private float ConeAngle = 30.0f;
    [SerializeField] private float Length = 5.0f;

    public Vector3 ComputeDirection( ChickenMultipliers multipliers )
    {
        Vector3 directionRight = Quaternion.Euler( 0, ConeAngle, 0 ) * transform.forward * Length;
        Vector3 directionForward = transform.forward * Length;
        Vector3 directionLeft = Quaternion.Euler( 0, -ConeAngle, 0 ) * transform.forward * Length;

        Vector3 normalAverage = Vector3.zero;

        //bool right = Physics.Raycast(transform.position, directionRight, out RaycastHit hitInfo, Length, LayerMask.GetMask("Wall"));

        //if(right)
        //{
        //    normalAverage += hitInfo.normal;
        //}

        bool forward = Physics.Raycast( transform.position, directionForward, out RaycastHit hitInfo, Length, LayerMask.GetMask( "Wall" ) );

        if ( forward )
        {
            normalAverage += hitInfo.normal;
        }

        //bool left = Physics.Raycast(transform.position, directionLeft, out hitInfo, Length, LayerMask.GetMask("Wall"));

        //if(left)
        //{
        //    normalAverage += hitInfo.normal;
        //}

        // Debug.DrawRay(transform.position, directionRight, right ? Color.black : Color.white);
        Debug.DrawRay( transform.position, directionForward, forward ? Color.black : Color.white );
        //Debug.DrawRay(transform.position, directionLeft, left ? Color.black : Color.white);

        Vector3 finalDir = Vector3.zero;

        //if ( right || forward || left )
        //{
        //    finalDir = normalAverage;
        //    Debug.DrawRay( transform.position, finalDir, Color.red );
        //}
        if ( forward )
        {
            finalDir = normalAverage;
            Debug.DrawRay( transform.position, finalDir, Color.red );
        }

        return finalDir;
    }


    public LayerMask m_allowCollision;
    public bool IsHittingWall( ChickenMultipliers multipliers )
    {
        Vector3 directionForward = transform.forward * Length;
        Debug.DrawLine( transform.position, transform.position + directionForward  *2f , Color.red  );

        bool forward = Physics.Raycast( transform.position, directionForward, out RaycastHit hitInfo, Length, m_allowCollision );
        //Debug.Log( "What ? "+forward );
        return forward;
    }
}
