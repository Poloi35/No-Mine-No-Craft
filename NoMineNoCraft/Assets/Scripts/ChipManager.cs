using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ChipManager : MonoBehaviour
{
    private List<GameObject> chips = new List<GameObject>();
    [SerializeField] private GameObject chipPrefab;
    [SerializeField] private Button button;
    private WireInstantiator wireInstantiator;

    private void Awake() {
        Singleton.instance.playerInputActions.UI.Click.performed += OnClick;
        wireInstantiator = GetComponentInParent<WireInstantiator>();
    }

    public void InstantiateChip()
    {
        Vector3 point = Singleton.instance.worldMousePos;
        point.z = 100f; // Distance from camera to screen (shouldn't be hardcoded)
        GameObject chip = (GameObject)Instantiate(chipPrefab, point, Quaternion.identity, transform);
        chips.Add(chip);
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector3 point = Singleton.instance.worldMousePos;
        point.z = 100f; // Distance from camera to screen (shouldn't be hardcoded)

        foreach (GameObject chipGameObject in chips)
        {
            Chip chip = chipGameObject.GetComponent<Chip>();
            foreach (CircleCollider2D pinCollider in chip.pinColliders)
            {
                // If point is in pin's bounds
                if (!pinCollider.bounds.Contains(point))
                    continue;
                
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
                }
                return;
            }
        }

        foreach (GameObject chipGameObject in chips)
        {
            Chip chip = chipGameObject.GetComponent<Chip>();
            wireInstantiator.startingPin = null;

            if (chip.GetComponent<BoxCollider2D>().bounds.Contains(point))
            {
                chip.EnableChipMovement(context.ReadValue<float>() == 1);
            }
        }
    }
}