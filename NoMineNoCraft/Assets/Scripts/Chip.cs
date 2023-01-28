using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Chip : MonoBehaviour
{
    private bool chipIsSelected = false;
    private Vector2 mousePos;
    private PlayerInputActions playerInputActions;
    private BoxCollider2D chipCollider;
    [SerializeField]
    private CircleCollider2D[] pinColliders;
    [SerializeField] private Camera canvasCam;
    
    private void Awake()
    {
        chipCollider = GetComponent<BoxCollider2D>();
        chipCollider.offset = Vector2.zero;
        chipCollider.size = GetComponent<RectTransform>().sizeDelta;
        foreach(CircleCollider2D pinCollider in pinColliders)
        {
            pinCollider.radius = ((RectTransform)pinCollider.transform).sizeDelta.x/2;
        }
        
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
        foreach(CircleCollider2D pinCollider in pinColliders)
        {
            if (context.ReadValue<float>() == 1 && pinCollider.bounds.Contains(mousePos))
            {
                // Create wire
                return;
            }
        }

        if (chipCollider.bounds.Contains(mousePos))
        {
            chipIsSelected = context.ReadValue<float>() == 1;
        }
    }
}
