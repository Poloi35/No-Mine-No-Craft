using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ChipInstantiator : MonoBehaviour
{
    [SerializeField]
    private GameObject chipPrefab;
    [SerializeField]
    private Button button;

    public void InstantiateChip()
    {
        Vector3 point = Singleton.instance.worldMousePos;
        point.z = 100f; // Distance from camera to screen (shouldn't be hardcoded)
        GameObject chip = (GameObject)Instantiate(chipPrefab, point, Quaternion.identity, transform);
    }
}
