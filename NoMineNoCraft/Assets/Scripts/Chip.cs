using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Chip : MonoBehaviour
{
    private bool chipCanMove = true;
    public Module module { get; set; }
    public BoxCollider2D chipCollider { get; set; }
    private List<Pin> inputPins, outputPins;
    private Camera canvasCam;

    public event Action OnChipMoved;

    private void Awake()
    {   
        inputPins = new List<Pin>();
        outputPins = new List<Pin>();
        canvasCam = Camera.main;

        // Set chip and pin's collider's bounds to their respective shapes
        chipCollider = GetComponent<BoxCollider2D>();
        chipCollider.offset = Vector2.zero;
        chipCollider.size = GetComponent<RectTransform>().sizeDelta;
        foreach (Pin pin in iterateThroughPins())
        {
            CircleCollider2D pinCollider = pin.pinCollider;
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
        {
            transform.position = point;
            OnChipMoved?.Invoke();
        }
    }

    public void EnableChipMovement(bool _chipCanMove)
    {
        chipCanMove = _chipCanMove;
    }

    public IEnumerable<Pin> iterateThroughPins()
    {
        foreach (Pin pin in inputPins)
        {
            yield return pin;
        }
        foreach (Pin pin in outputPins)
        {
            yield return pin;
        }
    }

    public void addInputPin(Pin pin)
    {
        inputPins.Add(pin);
    }

    public void addOutputPin(Pin pin)
    {
        outputPins.Add(pin);
    }

    public Pin getInputPin(int index)
    {
        return inputPins[index];
    }

    public Pin getOutputPin(int index)
    {
        return outputPins[index];
    }
}