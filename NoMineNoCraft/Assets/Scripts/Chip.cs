using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Chip : MonoBehaviour
{
    private bool chipIsSelected = true;
    private BoxCollider2D chipCollider;
    [SerializeField]
    private CircleCollider2D[] pinColliders;
    private Camera canvasCam;
    public WireInstantiator wireInstantiator;

    public event Action OnChipMoved;
    
    private void Awake()
    {
        canvasCam = Camera.main;
        
        wireInstantiator = GetComponentInParent<WireInstantiator>();

        // Set chip and pin's collider's bounds to their respective shapes
        chipCollider = GetComponent<BoxCollider2D>();
        chipCollider.offset = Vector2.zero;
        chipCollider.size = GetComponent<RectTransform>().sizeDelta;
        foreach(CircleCollider2D pinCollider in pinColliders)
        {
            pinCollider.radius = ((RectTransform)pinCollider.transform).sizeDelta.x/2;
        }
        
        Singleton.instance.playerInputActions.UI.Enable(); // Set Input Action's map to UI
        Singleton.instance.playerInputActions.UI.Point.performed += MoveChip; // Move chip when mouse moves
        Singleton.instance.playerInputActions.UI.Click.performed += OnClick;
}

    private void MoveChip(InputAction.CallbackContext context)
    {
        Vector3 point = Singleton.instance.worldMousePos;
        point.z = 100f; // Distance from camera to screen (shouldn't be hardcoded)
        if (chipIsSelected)
            transform.position = point;
            OnChipMoved?.Invoke(); // Nothing is subrscribed to OnChipMoved rn
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector3 point = Singleton.instance.worldMousePos;
        point.z = 100f; // Distance from camera to screen (shouldn't be hardcoded)

        // Check if pin is clicked
        foreach(CircleCollider2D pinCollider in pinColliders)
        {
            if (!pinCollider.bounds.Contains(point))
                continue;

            // Mouse button down
            if (context.ReadValue<float>() == 1)
            {
                // Save the starting point of the wire
                wireInstantiator.SetStartingPin(pinCollider.transform);
                return;
            } else
            {
                // Create the wire
                wireInstantiator.InstantiateWire(pinCollider.transform);
                return;
            }
        }

        // Check if chip is clicked (if no pin is clicked)
        if (chipCollider.bounds.Contains(point))
        {
            chipIsSelected = context.ReadValue<float>() == 1;
        }
    }
}