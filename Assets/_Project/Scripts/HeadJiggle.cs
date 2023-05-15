using Eloi;
using UnityEngine;

public class HeadJiggle : MonoBehaviour
{
    [SerializeField] private Vector3 angleCorrection;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform playerDirection;
    [SerializeField] private float k;
    [SerializeField] private float friction;

    private Vector3 _offset;
    private Vector3 _velocity;

    private void Start()
    {
        _offset = targetTransform.position - transform.position;
        targetTransform.SetParent(null);
    }

    private void Update()
    {
        // :TODO: move this only on character move

        Vector3 force = (targetTransform.position - (transform.position + _offset));
        float displacement = force.magnitude;
        force = force.normalized;

        Vector3 springForce = -k * force * displacement * Time.deltaTime;
        Vector3 frictionForce = -_velocity * friction * Time.deltaTime;

        _velocity += springForce + frictionForce;
        targetTransform.position += _velocity;



        //Vector3 dir = ( targetTransform.position - transform.position ).normalized;
        transform.LookAt(targetTransform);
        // transform.rotation = transform.rotation * Quaternion.Euler( angleCorrection.x, angleCorrection.y, angleCorrection.z );
        // transform.rotation =  Quaternion.Euler( angleCorrection.x, angleCorrection.y, angleCorrection.z )* transform.rotation;
        transform.Rotate(new Vector3(angleCorrection.x, angleCorrection.y, angleCorrection.z), Space.Self);
        //   transform.up = dir;




        E_RelocationUtility.GetWorldToLocal_Point(targetTransform.position, playerDirection, out Vector3 localTargetPosition);


        //Debug.DrawLine( Vector3.zero, localTargetPosition.normalized, Color.red, 0.1f );
        //Debug.DrawLine(Vector3.zero, new Vector3(0f, 0.5f, 0.5f), Color.red, 0.1f);

        Vector3 flatForward = localTargetPosition;
        flatForward.x = 0;
        Vector3 flatLeftRight = localTargetPosition;
        flatLeftRight.z = 0;
        m_angleJiggleForward = Vector3.Angle(flatForward, Vector3.forward) - 90;
        m_angleJiggleLeftRight = Vector3.Angle(flatLeftRight, Vector3.right) - 90;



        //Debug.DrawLine(transform.position, transform.position + Vector3.up, Color.green);
        //Debug.DrawLine(transform.position, targetTransform.position, Color.yellow);

        Vector3 directionHead = targetTransform.position - transform.position;
        Quaternion.FromToRotation(Vector3.forward, directionHead);

        transform.rotation = playerDirection.rotation;
        transform.Rotate(new Vector3(angleCorrection.x, angleCorrection.y, angleCorrection.z), Space.Self);
        transform.Rotate(new Vector3(m_angleJiggleForward * m_jiggleFactor, 0, m_angleJiggleLeftRight * m_jiggleFactor), Space.Self);


    }
    public float m_jiggleFactor = 3f;

    [Header("For debug")]
    public float m_angleJiggleForward;
    public float m_angleJiggleLeftRight;
}
