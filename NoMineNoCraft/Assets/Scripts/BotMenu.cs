using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BotMenu : MonoBehaviour
{
    public GameObject canvas;
    public Transform botPosition;
    public InputActionAsset mapAsset;
    private InputActionMap mapPlayer;
    private InputActionMap mapUI;

    void Start()
    {
        mapPlayer = mapAsset.FindActionMap("Player");
        mapUI = mapAsset.FindActionMap("UI");
    }

    public void OpenMenu(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        Cursor.lockState = CursorLockMode.None;

        float distanceToTriggerButton = 2.5f;
        if (Vector3.Distance(botPosition.position, this.transform.position) < distanceToTriggerButton && !canvas.activeInHierarchy)
        {
            canvas.SetActive(true);
            mapPlayer.Disable();
            mapUI.Enable();
        }
    }

    public void CloseMenu(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        
        if (canvas.activeInHierarchy)
        {
            canvas.SetActive(false);
            mapUI.Disable();
            mapPlayer.Enable();
        }
    }
}
