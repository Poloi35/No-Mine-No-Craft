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
    public WireInstantiator wireInstantiator;
    
    private void Awake()
    {
        canvasCam = Camera.main;
        
        wireInstantiator = GetComponentInParent<WireInstantiator>();

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
            if (!pinCollider.bounds.Contains(worldPos))
                continue;

            if (context.ReadValue<float>() == 1)
            {
                wireInstantiator.SetStartingPin(pinCollider.transform);
                return;
            } else
            {
                wireInstantiator.InstantiateWire(pinCollider.transform);
                return;
            }
        }

        if (chipCollider.bounds.Contains(worldPos))
        {
            chipIsSelected = context.ReadValue<float>() == 1;
        }
    }
}