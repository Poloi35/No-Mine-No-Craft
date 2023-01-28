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
        
        Singleton.instance.playerInputActions.UI.Enable();
        Singleton.instance.playerInputActions.UI.Point.performed += MoveChip;
        Singleton.instance.playerInputActions.UI.Click.performed += OnClick;
}

    private void MoveChip(InputAction.CallbackContext context)
    {
        Vector3 point = Singleton.instance.worldMousePos;
        point.z = 100f;
        if (chipIsSelected)
            transform.position = point;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector3 point = Singleton.instance.worldMousePos;
        point.z = 100f;
        foreach(CircleCollider2D pinCollider in pinColliders)
        {
            if (!pinCollider.bounds.Contains(point))
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

        if (chipCollider.bounds.Contains(point))
        {
            chipIsSelected = context.ReadValue<float>() == 1;
        }
    }
}