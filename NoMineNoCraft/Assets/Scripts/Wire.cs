using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public Transform[] pinTransforms;
    private LineRenderer lr;
    private EdgeCollider2D ec;

    private void Awake() {
        lr = GetComponent<LineRenderer>();
        ec = GetComponent<EdgeCollider2D>();
    }

    private void SetWirePoints()
    {
        lr.positionCount = pinTransforms.Length;
        for (int i = 0; i < lr.positionCount; i++)
        {
            Vector3 vertPos = pinTransforms[i].position;
            vertPos.z += -0.01f;
            lr.SetPosition(i, vertPos);
        }
    }

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
