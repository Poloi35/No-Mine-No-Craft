using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class ChipManager : MonoBehaviour
{
    private List<Chip> chips = new List<Chip>();
    [SerializeField] private GameObject chipPrefab;
    [SerializeField] private GameObject pinPrefab;
    [SerializeField] private Button button;
    private WireInstantiator wireInstantiator;

    private List<Chip> generatorChips = new List<Chip>(); // Temporaire de fou
    private bool simulating = false;

    private void Awake()
    {
        Singleton.instance.playerInputActions.UI.Click.performed += OnClick;
        wireInstantiator = GetComponentInParent<WireInstantiator>();
    }

    private Chip InstantiateChip<ModuleType>() where ModuleType : Module, new()
    {
        Vector3 point = Singleton.instance.worldMousePos;
        point.z = 100f; // Distance from camera to screen (shouldn't be hardcoded)
        GameObject chipGameObject = (GameObject)Instantiate(chipPrefab, point, Quaternion.identity, transform);
        Chip chip = chipGameObject.GetComponent<Chip>();
        chip.module = new ModuleType();

        int nbInput = chip.module.GetNumberOfInputs();
        int nbOutput = chip.module.GetNumberOfOutputs();
        for (int i = 0; i < nbInput; i++)
        {
            int j = -nbInput + 1 + 2 * i;
            GameObject pinGameObject = (GameObject)Instantiate(pinPrefab, new Vector3(0, 0, 0), Quaternion.identity, chip.transform);
            float x = -chip.chipCollider.size.x / 2;
            float y = j * chip.chipCollider.size.y / (2 * (nbInput + 1));
            pinGameObject.transform.localPosition = new Vector3(x, y, 0);
            Pin pin = pinGameObject.GetComponent<Pin>();
            pin.index = -(i + 1);
            chip.addInputPin(pin);
        }
        for (int i = 0; i < nbOutput; i++)
        {
            int j = -nbOutput + 1 + 2 * i;
            GameObject pinGameObject = (GameObject)Instantiate(pinPrefab, new Vector3(0, 0, 0), Quaternion.identity, chip.transform);
            float x = chip.chipCollider.size.x / 2;
            float y = j * chip.chipCollider.size.y / (2 * (nbOutput + 1));
            pinGameObject.transform.localPosition = new Vector3(x, y, 0);
            Pin pin = pinGameObject.GetComponent<Pin>();
            pin.index = i + 1;
            chip.addOutputPin(pin);
        }

        chips.Add(chip);
        return chip;
    }

    public void InstantiateFromZeroToOneMod()
    {
        Chip chip = InstantiateChip<FromZeroToOneMod>();
        generatorChips.Add(chip);
    }

    public void InstantiateDebugLogger()
    {
        InstantiateChip<DebugLogger>();
    }

    public void StartEndSimulation()
    {
        simulating = !simulating;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector3 point = Singleton.instance.worldMousePos;
        point.z = 100f; // Distance from camera to screen (shouldn't be hardcoded)

        foreach (Chip chip in chips)
        {
            foreach (Pin pin in chip.iterateThroughPins())
            {
                // If point is in pin's bounds
                if (!pin.pinCollider.bounds.Contains(point))
                    continue;

                if (context.ReadValue<float>() == 1)
                {
                    // Save the starting point of the wire
                    wireInstantiator.startingPin = pin;
                }
                else
                {
                    // Create the wire
                    if (wireInstantiator.startingPin)
                    {
                        if (Pin.CreateLinkBetween(pin,wireInstantiator.startingPin))
                            wireInstantiator.InstantiateWire(pin);
                    }
                    wireInstantiator.startingPin = null;
                }
                return;
            }
        }

        foreach (Chip chip in chips)
        {
            wireInstantiator.startingPin = null;

            if (chip.chipCollider.bounds.Contains(point))
            {
                chip.EnableChipMovement(context.ReadValue<float>() == 1);
            }
        }
    }

    private void Update()
    {
        if (simulating)
        {
            foreach (Chip chip in generatorChips)
            {
                chip.module.Execute();
            }
        }
    }
}