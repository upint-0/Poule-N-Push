using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] public float MaxSpeed { get; private set; } = 2.0f;
    private NavMeshAgent _controller;

    public Vector2 Input { get; private set; }

    public void AxisInput(CallbackContext context)
    {
        Input = context.ReadValue<Vector2>();
    }

    private void Awake()
    {
        _controller = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector3 vel = new Vector3( Input.x, 0, Input.y );
        _controller.velocity = vel * MaxSpeed;
        if ( vel.magnitude > 0.1f )
        {
            transform.forward = vel.normalized;
        }
    }
}
