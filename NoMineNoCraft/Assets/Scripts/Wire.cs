using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wire : MonoBehaviour
{
    public Pin[] pins;
    private LineRenderer lr;
    private EdgeCollider2D ec;

    private void Awake() {
        lr = GetComponent<LineRenderer>();
        ec = GetComponent<EdgeCollider2D>();
        Singleton.instance.playerInputActions.UI.RightClick.performed += DestroyWire;
    }

    private void Start() {
        SetWirePoints();
        SetColliderPoints();
    }

    public void SetSubscriptionsOnChipMove()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].parentChip.OnChipMoved += SetWirePoints;
            pins[i].parentChip.OnChipMoved += SetColliderPoints;
        }
    }

    private void DestroyWire(InputAction.CallbackContext context)
    {
        // A better condition would be to calculate the distance bewtween mouse and collider
        if (ec.bounds.Contains(Singleton.instance.worldMousePos))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Singleton.instance.playerInputActions.UI.RightClick.performed -= DestroyWire;
        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].parentChip.OnChipMoved -= SetWirePoints;
            pins[i].parentChip.OnChipMoved -= SetColliderPoints;
        }
    }

    // Moves the wire based on the pins' transforms
    private void SetWirePoints()
    {
        lr.positionCount = pins.Length;
        for (int i = 0; i < lr.positionCount; i++)
        {
            Vector3 vertexPos = pins[i].transform.position;
            vertexPos.z += -0.01f; // Wire can't be rendered on canvas so it's put in front of it
            lr.SetPosition(i, vertexPos);
        }
    }

    // Sets the collider on top of the wire
    private void SetColliderPoints()
    {
        List<Vector2> points = new List<Vector2>(pins.Length);
        foreach (Pin pin in pins)
        {
            points.Add(transform.InverseTransformPoint(pin.transform.position));
        }
        ec.SetPoints(points);
    }
}
