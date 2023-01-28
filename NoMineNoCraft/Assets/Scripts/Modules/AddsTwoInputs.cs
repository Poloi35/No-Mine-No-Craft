using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This mod takes two inputs and add them together before outputting the result
public class AddsTwoInputs : BasicModule
{

    public override void Execute()
    {
        TriggerValueChangement(inputs[0].Value + inputs[1].Value);
    }

    protected override int GetNumberOfInputs()
    {
        return 2;
    }

    protected override int GetNumberOfOutputs()
    {
        return 1;
    }
}
