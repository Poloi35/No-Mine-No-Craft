using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This mod is a mod for developpers only and allows us to check what is going on inside a module chain (or at the end of it more conveniently)
public class DebugLogger : Module
{
    public override void Execute()
    {
        Debug.Log(inputs[0].Value);
    }
}
