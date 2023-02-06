using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public Chip parentChip { get; set; }
    public CircleCollider2D pinCollider { get; set; }
    public int index { get; set; }

    private void Awake()
    {
        pinCollider = GetComponent<CircleCollider2D>();
    }

    public bool isInput()
    {
        return index < 0;
    }

    public bool isOutput()
    {
        return !isInput();
    }

    public static bool CreateLinkBetween(Pin pin1, Pin pin2)
    {
        Chip inputChip = (pin1.isInput()) ? pin1.GetComponentInParent<Chip>() : pin2.GetComponentInParent<Chip>();
        Chip outputChip = (pin1.isOutput()) ? pin1.GetComponentInParent<Chip>() : pin2.GetComponentInParent<Chip>();
        int inputPinIndex = -((pin1.isInput()) ? pin1 : pin2).index - 1;
        int outputPinIndex = ((pin1.isOutput()) ? pin1 : pin2).index - 1;

        //Inputs are indexed as negative and outputs as positives so if multiplication is positive it means that both are the same sign so both are inputs or both are outputs
        if (inputChip == outputChip || pin1.index * pin2.index > 0 || inputChip.module.inputIsConnected(inputPinIndex))
            return false;

        inputChip.module.LinkToOutput(outputChip.module, inputPinIndex, outputPinIndex);
        return true;
    }
    
    public static void DeleteLinkBetween(Pin pin1, Pin pin2)
    {
        if (pin1.isInput() && pin2.isOutput())
            pin1.GetComponentInParent<Chip>().module.UnlinkFromOutput(pin2.GetComponentInParent<Chip>().module, -pin1.index - 1, pin2.index - 1);
        else if (pin2.isInput() && pin1.isOutput())
            pin2.GetComponentInParent<Chip>().module.UnlinkFromOutput(pin1.GetComponentInParent<Chip>().module, -pin2.index - 1, pin1.index - 1);
    }
}
