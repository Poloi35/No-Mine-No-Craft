using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This mod takes two inputs and add them together before outputting the result
public class AddsTwoInputs : Module
{
    public override void Execute()
    {
        TriggerValueChangement(inputs[0].Value + inputs[1].Value);
    }
}
