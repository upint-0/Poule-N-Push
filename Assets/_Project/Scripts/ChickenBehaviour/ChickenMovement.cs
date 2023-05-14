using UnityEngine;
using UnityEngine.AI;

public class ChickenMovement : MonoBehaviour
{
    public Vector3 CurrentDirection { get; set; }
    public float CurrentSpeed { get; set; }

    private NavMeshAgent _agent = null;
    private ChickenData _chickenData;

    public void Initialize(ChickenData data)
    {
        _chickenData = data;
        _agent = GetComponent<NavMeshAgent>();
    }

    public void ApplyMovement()
    {
        if(CurrentSpeed <= 0.1f)
        {
            return;
        }
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.blue);
        Debug.DrawLine(transform.position, transform.position + CurrentDirection, Color.cyan);
        Quaternion rot = Quaternion.LookRotation(CurrentDirection, Vector3.up);
        rot = Quaternion.RotateTowards(transform.rotation, rot, _chickenData.RotationSpeed * Time.deltaTime);
        transform.rotation = rot;
        _agent.Move(transform.forward * Time.deltaTime * CurrentSpeed);
    }
}
