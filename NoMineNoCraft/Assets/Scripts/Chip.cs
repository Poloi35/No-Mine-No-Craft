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

    private static int nbChips = 0;
    private static int nbChipsVisited = 0;
    
    private void Awake()
    {
        canvasCam = Camera.main;
        nbChips++;
        
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
            // If startingPin is not null it means a pin was found on an other chip
            if (wireInstantiator.startingPin && context.ReadValue<float>() == 1)
                return;

            if (!pinCollider.bounds.Contains(point))
                continue;

            // Mouse button down
            if (context.ReadValue<float>() == 1)
            {
                // Save the starting point of the wire
                wireInstantiator.startingPin = pinCollider.transform;
            } else
            {
                // Create the wire
                if (wireInstantiator.startingPin)
                    wireInstantiator.InstantiateWire(pinCollider.transform);
                    wireInstantiator.startingPin = null;
                    nbChipsVisited = 0;
            }
            return;
        }

        if (nbChipsVisited++ == nbChips)
        {
            Debug.Log("No pin found");
            wireInstantiator.startingPin = null;
            nbChipsVisited = 0;
        }
        
        // Check if chip is clicked (if no pin is clicked)
        if (chipCollider.bounds.Contains(point))
        {
            chipIsSelected = context.ReadValue<float>() == 1;
        }
    }
}