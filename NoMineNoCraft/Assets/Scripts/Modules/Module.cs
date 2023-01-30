using System.Collections.Generic;
using UnityEngine;

//This is the class that is above everything that is module-like (for now only basic modules are done, functions will come later)
public interface Module
{
    //This is the main function of every module, which is to be redefined in every subclass
    public abstract void Execute();

    //Usage : Module1.LinkToOutput(Module2, inputNb, outputNb) with Module1 the module whose input number inputNb is to be linked to the output number outputNb of Module2
    public abstract void LinkToOutput(Module mod, int inputNb = 0, int outputNb = 0);

    //Just a delegate type. Be sure to use Module.TriggerOutput in the children to have the exact same
    public delegate void TriggerOutput(float value);

    //Usage : Module1.AddListener(outputNb, OnEvent) where Module1 is the module we want to listen the output of number outputNb of, and OnEvent is the function that will be called when said output outputs
    public void AddListener(int outputNb, TriggerOutput func);

    public int GetNumberOfOutputs();
    public int GetNumberOfInputs();
}