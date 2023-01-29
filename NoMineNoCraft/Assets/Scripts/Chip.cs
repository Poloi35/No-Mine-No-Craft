using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Chip : MonoBehaviour
{
    private bool chipCanMove = true;
    private BoxCollider2D chipCollider;
    [SerializeField] private CircleCollider2D[] _pinColliders;
    public CircleCollider2D[] pinColliders { get { return _pinColliders; } set { _pinColliders = value; } }
    private Camera canvasCam;

    public event Action OnChipMoved;

    private void Awake()
    {
        canvasCam = Camera.main;

        // Set chip and pin's collider's bounds to their respective shapes
        chipCollider = GetComponent<BoxCollider2D>();
        chipCollider.offset = Vector2.zero;
        chipCollider.size = GetComponent<RectTransform>().sizeDelta;
        foreach (CircleCollider2D pinCollider in pinColliders)
        {
            pinCollider.radius = ((RectTransform)pinCollider.transform).sizeDelta.x / 2;
        }

        Singleton.instance.playerInputActions.UI.Enable(); // Set Input Action's map to UI
        Singleton.instance.playerInputActions.UI.Point.performed += MoveChip; // Move chip when mouse moves
    }

    private void MoveChip(InputAction.CallbackContext context)
    {
        Vector3 point = Singleton.instance.worldMousePos;
        point.z = 100f; // Distance from camera to screen (shouldn't be hardcoded)
        if (chipCanMove)
            transform.position = point;
        OnChipMoved?.Invoke(); // Nothing is subrscribed to OnChipMoved rn
    }

    public void EnableChipMovement(bool _chipCanMove)
    {
        chipCanMove = _chipCanMove;
    }
}