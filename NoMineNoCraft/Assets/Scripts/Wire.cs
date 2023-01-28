using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public Transform[] pinTransforms;
    private LineRenderer lr;

    private void Awake() {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lr.positionCount = pinTransforms.Length;
        for (int i = 0; i < lr.positionCount; i++)
        {
            Vector3 vertPos = pinTransforms[i].position;
            vertPos.z += -0.01f;
            lr.SetPosition(i, vertPos);
        }
    }
}
