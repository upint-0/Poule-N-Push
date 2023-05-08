using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerTest : MonoBehaviour
{
    public void TestInput(CallbackContext context)
    {
        print($"player {GetComponent<PlayerInput>().playerIndex} pressed on {context.action.name}");
    }
}
