using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Chip : MonoBehaviour
{
    private bool chipIsSelected = false;
    private Vector3 worldPos;
    private PlayerInputActions playerInputActions;
    private BoxCollider2D chipCollider;
    [SerializeField]
    private CircleCollider2D[] pinColliders;
    private Camera canvasCam;
    
    private void Awake()
    {
        canvasCam = Camera.main;
        
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
        worldPos = canvasCam.ScreenToWorldPoint(context.ReadValue<Vector2>());
        worldPos.z = 100f; // Shouldn't be hardcoded

        if (chipIsSelected)
            transform.position = worldPos;
            
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        foreach(CircleCollider2D pinCollider in pinColliders)
        {
            if (context.ReadValue<float>() == 1 && pinCollider.bounds.Contains(worldPos))
            {
                // Create wire
                return;
            }
        }

        if (chipCollider.bounds.Contains(worldPos))
        {
            Debug.Log("chip selected");
            chipIsSelected = context.ReadValue<float>() == 1;
        }
    }
}
