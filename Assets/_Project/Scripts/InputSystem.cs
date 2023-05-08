using UnityEngine;

public static class InputSystem
{
    // -- FIELDS

    private static Inputs InputAsset = null;

    // -- PROPERTIES

    public static Vector2 MovementInput => InputAsset.Player.Movement.ReadValue<Vector2>();
    public static Vector2 AimInput => InputAsset.Player.Aim.ReadValue<Vector2>();

    // -- EVENTS

    public delegate void ActionPressedHandler();

    public static event ActionPressedHandler OnInteractionPressed;

    // -- METHODS

    public static void Initialize()
    {
        InputAsset = new Inputs();

        BindEvents();
        Enable();
    }

    private static void BindEvents()
    {
        InputAsset.Player.Interaction.performed += _ => OnInteractionPressed.Invoke();
    }

    public static void Enable()
    {
        InputAsset.Enable();
    }

    public static void Disable()
    {
        InputAsset.Disable();
    }
}
