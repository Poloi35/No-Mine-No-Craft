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
    public InputActionAsset mapAsset;
    private InputActionMap mapPlayer;
    private InputActionMap mapUI;

    private void Awake() {
        Singleton.instance.playerInputActions.Player.Interact.performed += OpenMenu;
        Singleton.instance.playerInputActions.UI.Interact.started += CloseMenu;
    }

    void Start()
    {
        mapPlayer = mapAsset.FindActionMap("Player");
        mapUI = mapAsset.FindActionMap("UI");
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
            mapPlayer.Disable();
            mapUI.Enable();
        }
    }

    public void CloseMenu(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("ok");
        canvas.SetActive(false);
        playerCamera.SetActive(true);
        UICamera.SetActive(false);
        mapUI.Disable();
        mapPlayer.Enable();
    }
}
