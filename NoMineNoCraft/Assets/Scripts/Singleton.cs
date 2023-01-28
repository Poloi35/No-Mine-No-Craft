using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Singleton : MonoBehaviour
{
    public static Singleton instance {get; private set;}
    public PlayerInputActions playerInputActions;
    public Vector3 worldMousePos; // Mouse pos converted to world coordinates

    private void Awake()
    {
        // Destroy any duplicate of Singleton
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        // Input system
        playerInputActions = new PlayerInputActions();
        instance.playerInputActions.UI.Point.performed += UpdateMousePos;
    }

    private void UpdateMousePos(InputAction.CallbackContext context)
    {
        worldMousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }
}