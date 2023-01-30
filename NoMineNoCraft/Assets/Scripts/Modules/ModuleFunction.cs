using System.Collections.Generic;
using UnityEngine;

//To create a function of modules
public class ModuleFunction : Module
{
    private List<List<(Module linkedModule, int linkedModuleInputNb)>> inputs;

    private List<(Module linkedModule, int linkedModuleOutputNb)> outputs;

    public ModuleFunction(){
        inputs = new List<List<(Module,int)>>();
        outputs = new List<(Module,int)>();
    }

    //Creates a new input for the function
    public void CreateInput(){
        inputs.Add(new List<(Module,int)>()); // Si l'accession par les valeurs linkedModule et linkedModuleInputNb ne se fait pas correctement, ajouter les noms ici
    }

    //Removes a specific input from the function by index
    public void RemoveInput(int inputNb){
        inputs.RemoveAt(inputNb);
    }

    //Creates a new output for the function
    public void CreateOutput(){
        outputs.Add((null,0));
    }

    //Removes a specific output from the function by index
    public void RemoveOutput(int outputNb){
        outputs.RemoveAt(outputNb);
    }

    //Currently does nothing, maybe I should make it do something idk
    public void Execute()
    {
    }



    //This part concerns the inputs of the function

    //This will get every module that is linked to the input number inputNb of this function and link the output number outputNb of the Module module to every one of them
    public void LinkToOutput(Module module, int inputNb = 0, int outputNb = 0)
    {
        foreach ((Module linkedModule, int linkedModuleInputNb) input in inputs[inputNb])
        {
            input.linkedModule.LinkToOutput(module, input.linkedModuleInputNb, outputNb);
        }
    }
    //This will link the input linkedModuleInputNb of a module (module) inside the function to the input inputNb of that function
    public void AddLinkOnInput(Module module, int inputNb = 0, int linkedModuleInputNb = 0){
        inputs[inputNb].Add((module,linkedModuleInputNb));
    }
    //This does the opposite
    public void RemoveLinkOnInput(Module module, int inputNb = 0, int linkedModuleInputNb = 0){
        inputs[inputNb].Remove((module,linkedModuleInputNb));
    }


    //This will make the func that wants to listen our function's output outputNb instead listen the output of the module that is set at the output of the function
    public void AddListener(int outputNb, Module.TriggerOutput func)
    {
        outputs[outputNb].linkedModule.AddListener(outputs[outputNb].linkedModuleOutputNb,func);
    }
    //This sets a module (module)'s output linkedModuleOutputNb to the output outputNb of this function. Its set and not add because an output can only output one mod at a time
    public void SetLinkOnOutput(Module module, int outputNb = 0, int linkedModuleOutputNb = 0){
        outputs[outputNb] = (module,linkedModuleOutputNb);
    }
    //This removes the module from the outputs of the function on the output outputNb
    public void UnsetLinkOnOutput(int outputNb){
        outputs[outputNb] = (null,0);
    }


    public int GetNumberOfInputs(){
        return inputs.Count;
    }

    public int GetNumberOfOutputs(){
        return outputs.Count;
    }
}