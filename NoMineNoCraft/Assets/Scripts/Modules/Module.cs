using System.Collections.Generic;
using UnityEngine;

//This is the class that is above everything that is module-like (for now only basic modules are done, functions will come later)
public interface Module
{
    //This is the main function of every module, which is to be redefined in every subclass
    public abstract void Execute();

    //Usage : Module1.LinkToOutput(Module2, index) with Module1 the module whose input number inputNb is to be linked to the output number outputNb of Module2
    public abstract void LinkToOutput(Module mod, int inputNb = 0, int outputNb = 0);

    public delegate void TriggerOutput(float value);
    public void AddListener(int outputNb, TriggerOutput func);
}