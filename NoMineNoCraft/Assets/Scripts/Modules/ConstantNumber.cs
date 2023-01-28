using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantNumber : BasicModule
{
    int value;

    public ConstantNumber(int v){
        value = v;
    }
    public override void Execute()
    {
        TriggerValueChangement(value);
    }

    protected override int GetNumberOfInputs()
    {
        return 0;
    }

    protected override int GetNumberOfOutputs()
    {
        return 1;
    }
}
