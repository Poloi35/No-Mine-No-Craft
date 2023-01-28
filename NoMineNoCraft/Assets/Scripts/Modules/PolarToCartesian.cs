using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PolarToCartesian : BasicModule
{
    public override void Execute()
    {
        float r = inputs[0].Value;
        float theta = inputs[1].Value;
        TriggerValueChangement(r*Mathf.Cos(theta), 0);
        TriggerValueChangement(r*Mathf.Sin(theta), 1);
    }

    protected override int GetNumberOfInputs()
    {
        return 2;
    }

    protected override int GetNumberOfOutputs()
    {
        return 2;
    }
}
