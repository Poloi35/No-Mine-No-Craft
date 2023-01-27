using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Chip : MonoBehaviour
{
    private bool chipIsSelected = true;
    private Vector2 mousePos;
    private PlayerInputActions playerInputActions;
    private BoxCollider2D bc;
    
    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        bc.offset = Vector2.zero;
        bc.size = GetComponent<RectTransform>().sizeDelta;
        playerInputActions = new PlayerInputActions();
        playerInputActions.UI.Enable();
        playerInputActions.UI.Point.performed += MoveChip;
        playerInputActions.UI.Click.performed += OnClick;
}

    private void MoveChip(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();

        if (chipIsSelected)
            transform.position = mousePos;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 0 || !bc.bounds.Contains(mousePos))
            return;

        chipIsSelected = !chipIsSelected;
    }
}
