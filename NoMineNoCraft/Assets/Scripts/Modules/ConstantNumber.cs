using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Always outputs the number given in the constructor
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

    public override int GetNumberOfInputs()
    {
        return 0;
    }

    public override int GetNumberOfOutputs()
    {
        return 1;
    }
}
