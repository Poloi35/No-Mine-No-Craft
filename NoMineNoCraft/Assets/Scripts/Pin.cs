using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public CircleCollider2D pinCollider { get; set; }

    public int index { get; set; }

    private void Awake()
    {
        pinCollider = GetComponent<CircleCollider2D>();
    }

    public bool isInput(){
        return index < 0;
    }

    public bool isOutput(){
        return !isInput();
    }

    public static bool CreateLinkBetween(Pin pin1, Pin pin2)
    {
        if (pin1.isInput() && pin2.isOutput())
            pin1.GetComponentInParent<Chip>().module.LinkToOutput(pin2.GetComponentInParent<Chip>().module, -pin1.index-1, pin2.index-1);
        else if (pin2.isInput() && pin1.isOutput())
            pin2.GetComponentInParent<Chip>().module.LinkToOutput(pin1.GetComponentInParent<Chip>().module, -pin2.index-1, pin1.index-1);
        else
            return false;
        return true;
    }
    public static void DeleteLinkBetween(Pin pin1, Pin pin2)
    {
        if (pin1.isInput() && pin2.isOutput())
            pin1.GetComponentInParent<Chip>().module.UnlinkFromOutput(pin2.GetComponentInParent<Chip>().module, -pin1.index-1, pin2.index-1);
        else if (pin2.isInput() && pin1.isOutput())
            pin2.GetComponentInParent<Chip>().module.UnlinkFromOutput(pin1.GetComponentInParent<Chip>().module, -pin2.index-1, pin1.index-1);
    }
}
