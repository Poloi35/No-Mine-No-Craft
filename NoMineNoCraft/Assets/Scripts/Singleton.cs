using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Singleton : MonoBehaviour
{
    public static Singleton instance {get; private set;}
    public PlayerInputActions playerInputActions;
    public Vector3 worldMousePos; // Mouse pos converted to world coordinates

    public enum ActionMap
    {
        Player,
        UI
    }

    private void Awake()
    {
        // Destroy any duplicate of Singleton
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        // Input system
        playerInputActions = new PlayerInputActions();
        SwitchInputMap(ActionMap.Player);

        instance.playerInputActions.UI.Point.performed += UpdateMousePos;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UpdateMousePos(InputAction.CallbackContext context)
    {
        worldMousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    public void SwitchInputMap(ActionMap actionMap)
    {
        playerInputActions.UI.Disable();
        playerInputActions.Player.Disable();

        switch (actionMap)
        {
            case ActionMap.Player:
                playerInputActions.Player.Enable();
                break;

            case ActionMap.UI:
                playerInputActions.UI.Enable();
                break;

            default:
                break;
        }
        
    }
}
