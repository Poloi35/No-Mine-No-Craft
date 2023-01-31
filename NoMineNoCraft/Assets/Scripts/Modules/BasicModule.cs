using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BasicModule : Module
{

    //This int is only there to know when we can execute the execute function (so basically when every input has been retrieved)
    protected int inputsReady = 0;

    //The purpose of this list is to stock inputs so that you can access them once you need them but also to quickly check if they are ready (will have the null value if they are not)
    protected List<float?> inputs = new List<float?>();

    protected List<Module.TriggerOutput> listeningFuncs = new List<Module.TriggerOutput>();

    public BasicModule()
    {
        initializeInputs();
        initializeOutputs();
    }

    //This is to initialize the inputs list but as I don't know the number of outputs I put it in the GetNumberOfInputs() function that will have to be defined in children
    public void initializeInputs()
    {
        for (int i = 0; i < GetNumberOfInputs(); i++)
        {
            inputs.Add(null);
            listeningFuncs.Add(null);
        }
    }
    public abstract int GetNumberOfInputs();

    //This is the output initialization part and works mainly the same way the input part does
    public delegate void TriggerOutput(float value);
    protected List<Module.TriggerOutput> outputs = new List<Module.TriggerOutput>();

    public void initializeOutputs()
    {
        for (int i = 0; i <= GetNumberOfOutputs(); i++)
        {
            outputs.Add(null);
        }
    }
    public abstract int GetNumberOfOutputs();



    //To trigger the output of your function just use this with the value you want to output and the number of the output you want it to output to
    protected void TriggerValueChangement(float value, int outputNb = 0)
    {
        if (outputs[outputNb] != null)
            outputs[outputNb](value);
    }
    //This is for another module to connect to you. The other module will tell which output he wants to listen and which function will be called when this output outputs
    public void AddListener(int outputNb, Module.TriggerOutput func)
    {
        outputs[outputNb] += func;
    }

    public void RemoveListener(int outputNb, Module.TriggerOutput func)
    {
        outputs[outputNb] -= func;
    }


    protected void OnEvent(float value, int inputNb)
    {
        //I have this inputsReady int that will be incremented when i get a value that wasnt already there
        if (inputs[inputNb] == null)
            inputsReady++;
        inputs[inputNb] = value; //Then ofc I get the value in my inputs at the right place

        //Then when all my inputs are ready I just have to execute the module and reset everything (number of inputs ready to 0 and every input to null)
        if (inputsReady == inputs.Count)
        {
            this.Execute();
            inputsReady = 0;
            for (int i = 0; i < inputs.Count; i++)
            {
                inputs[i] = null;
            }
        }
    }

    public abstract void Execute();

    //To link Module1 output to Module2 input just do Module2.LinkToOutput(Module1, the number of the input of Module2, the number of the output of Module1)
    public void LinkToOutput(Module output, int inputNb = 0, int outputNb = 0)
    {
        listeningFuncs[inputNb] = (float value) => OnEvent(value, inputNb);
        output.AddListener(outputNb, listeningFuncs[inputNb]);
    }

    public void UnlinkFromOutput(Module output, int inputNb = 0, int outputNb = 0)
    {
        output.RemoveListener(outputNb, listeningFuncs[inputNb]);
    }
}
