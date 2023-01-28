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
    private PlayerInputActions playerInputActions;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
    }

    public void InstantiateChip()
    {
        Instantiate(chipPrefab, Vector3.zero, Quaternion.identity, transform).transform.localPosition = Vector3.zero;
    }
}
