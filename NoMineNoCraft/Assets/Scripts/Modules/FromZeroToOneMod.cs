using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This mod doesnt take input and outputs one value that changes over time to grow from zero up to one and then will reset to zero
public class FromZeroToOneMod : Module
{

    public float value = 0f;

    public override void execute()
    {
        value += 0.001f;
        if (value > 1)
        {
            value = 0f;
        }
        triggerValueChangement(value);
    }
}