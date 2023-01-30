using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This mod is a mod for developpers only and allows us to check what is going on inside a module chain (or at the end of it more conveniently) you can specify a log prefix in constructor
public class DebugLogger : BasicModule
{
    string prefix;
    public DebugLogger(string p=""){
        prefix = p;
    }
    public override void Execute()
    {
        Debug.Log(prefix + inputs[0].Value);
    }

    public override int GetNumberOfInputs()
    {
        return 1;
    }

    public override int GetNumberOfOutputs()
    {
        return 0;
    }
}
