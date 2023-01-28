using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BasicModule : Module
{

    //This int is only there to know when we can execute the execute function (so basically when every input has been retrieved)
    protected int inputsReady = 0;

    private int randomNb = 0;

    //The purpose of this list is to stock inputs so that you can access them once you need them but also to quickly check if they are ready (will have the null value if they are not)
    protected List<float?> inputs = new List<float?>();
    public BasicModule()
    {   
        if (randomNb == 0){
            System.Random rnd = new System.Random();
            randomNb = rnd.Next(1, 101);
        }
        Debug.Log("Initilized " + this.GetType().Name);
        Debug.Log(randomNb);
        initializeInputs();
        initializeOutputs();
    }
    public void initializeInputs()
    {
        for (int i = 0; i < GetNumberOfInputs(); i++)
        {
            inputs.Add(null);
        }
    }
    protected abstract int GetNumberOfInputs();
    public delegate void TriggerOutput(float value);
    protected List<Module.TriggerOutput> outputs = new List<Module.TriggerOutput>();

    public void initializeOutputs()
    {
        for (int i = 0; i <= GetNumberOfOutputs(); i++)
        {   
            outputs.Add(null);
        }
    }
    protected abstract int GetNumberOfOutputs();


    //Those four make the output part of every module. You can do it by +=ing on the OnValueChanged with the function you want to be called for this output

    protected void TriggerValueChangement(float value, int outputNb = 0)
    {
        if (outputs[outputNb] != null)
            outputs[outputNb](value);
    }
    public void AddListener(int outputNb, Module.TriggerOutput func)
    {   
        outputs[outputNb] += func;
    }


    //This is the part about the input of modules. You just have to use it like NameOfTheModuleYouWantToAddAnInputTo.addInput(NameOfTheModuleWithTheOutput)
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

    public void LinkToOutput(Module output, int inputNb = 0, int outputNb = 0)
    {
        output.AddListener(outputNb, (float value) => OnEvent(value, inputNb));
    }
}
