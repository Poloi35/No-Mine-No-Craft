using System.Collections.Generic;
using UnityEngine;

//This is the class that is above everything that is module-like (for now only basic modules are done, functions will come later)
public abstract class Module
{

    //The purpose of this list is to stock inputs so that you can access them once you need them but also to quickly check if they are ready (will have the null value if they are not)
    protected List<float?> inputs = new List<float?>();

    //This int is only there to know when we can execute the execute function (so basically when every input has been retrieved)
    protected int inputsReady = 0;

    //This is the main function of every module, which is to be redefined in every subclass
    public abstract void Execute();


    //Those four make the output part of every module. You can do it by +=ing on the OnValueChanged with the function you want to be called for this output
    public delegate void TriggerOutput(float value);
    private event TriggerOutput OnValueChanged;
    protected void TriggerValueChangement(float value)
    {
        OnValueChanged(value);
    }
    public void AddListener(TriggerOutput func){
        OnValueChanged += func;
    }


    //This is the part about the input of modules. You just have to use it like NameOfTheModuleYouWantToAddAnInputTo.addInput(NameOfTheModuleWithTheOutput)
    protected void OnEvent(float value, int inputNb)
    {   
        //I have this inputsReady int that will be incremented when i get a value that wasnt already there
        if (inputs[inputNb] == null)
            inputsReady++;
        inputs[inputNb] = value; //Then ofc I get the value in my inputs at the right place

        //Then when all my inputs are ready I just have to execute the module and reset everything (number of inputs ready to 0 and every input to null)
        if (inputsReady == inputs.Count){
            this.Execute();
            inputsReady = 0;
            for (int i = 0; i < inputs.Count; i++){
                inputs[i] = null;
            }
        }
    }

    public void AddInput(Module input)
    {
        int nbInputs = inputs.Count;
        //This works so that you know which input has been given and where it should go in the input list up there
        input.AddListener((float value) => OnEvent(value, nbInputs));
        inputs.Add(null); //Initializes the input to the "didnt receive input yet" value
    }
}