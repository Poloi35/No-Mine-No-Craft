using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wire : MonoBehaviour
{
    public Transform[] pinTransforms;
    private LineRenderer lr;
    private EdgeCollider2D ec;

    private void Awake() {
        lr = GetComponent<LineRenderer>();
        ec = GetComponent<EdgeCollider2D>();
        Singleton.instance.playerInputActions.UI.RightClick.performed += DestroyWire;
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
    }

    // Moves the wire based on the pins' transforms
    private void SetWirePoints()
    {
        lr.positionCount = pinTransforms.Length;
        for (int i = 0; i < lr.positionCount; i++)
        {
            Vector3 vertexPos = pinTransforms[i].position;
            vertexPos.z += -0.01f; // Wire can't be rendered on canvas so it's put in front of it
            lr.SetPosition(i, vertexPos);
        }
    }

    // Sets the collider on top of the wire
    private void SetColliderPoints()
    {
        List<Vector2> points = new List<Vector2>(pinTransforms.Length);
        foreach (Transform pinTransform in pinTransforms)
        {
            points.Add(transform.InverseTransformPoint(pinTransform.position));
        }
        ec.SetPoints(points);
    }

    private void Update()
    {
        SetWirePoints();
        SetColliderPoints();
    }
}
