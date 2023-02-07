using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BotMenu : MonoBehaviour
{
    public GameObject canvas;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject UICamera;
    public Transform botPosition;

    private void Awake() {
        Singleton.instance.playerInputActions.Player.Interact.started += OpenMenu;
        Singleton.instance.playerInputActions.UI.CloseMenu.started += CloseMenu;
    }

    public void OpenMenu(InputAction.CallbackContext context)
    {
        Cursor.lockState = CursorLockMode.None;

        float distanceToTriggerButton = 2.5f;
        if (Vector3.Distance(botPosition.position, this.transform.position) < distanceToTriggerButton && !canvas.activeInHierarchy)
        {
            canvas.SetActive(true);
            playerCamera.SetActive(false);
            UICamera.SetActive(true);
            Singleton.instance.SwitchInputMap(Singleton.ActionMap.UI);
        }
    }

    public void CloseMenu(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        Cursor.lockState = CursorLockMode.Locked;
        canvas.SetActive(false);
        playerCamera.SetActive(true);
        UICamera.SetActive(false);
        Singleton.instance.SwitchInputMap(Singleton.ActionMap.Player);
    }
}
