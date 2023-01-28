using System.Collections.Generic;
using UnityEngine;

public class ModuleFunction : Module
{
    private List<List<(Module linkedModule, int linkedModuleInputNb)>> inputs;

    private List<(Module linkedModule, int linkedModuleOutputNb)> outputs;

    public ModuleFunction(){
        inputs = new List<List<(Module,int)>>();
        outputs = new List<(Module,int)>();
    }

    public void CreateInput(){
        inputs.Add(new List<(Module,int)>()); // Si l'accession par les valeurs linkedModule et linkedModuleInputNb ne se fait pas correctement, ajouter les noms ici
    }

    public void RemoveInput(int inputNb){
        inputs.RemoveAt(inputNb);
    }

    public void CreateOutput(){
        outputs.Add((null,0));
    }

    public void RemoveOutput(int outputNb){
        outputs.RemoveAt(outputNb);
    }

    public void Execute()
    {
    }

    public void LinkToOutput(Module module, int inputNb = 0, int outputNb = 0)
    {
        foreach ((Module linkedModule, int linkedModuleInputNb) input in inputs[inputNb])
        {
            input.linkedModule.LinkToOutput(module, input.linkedModuleInputNb, outputNb);
        }
    }
    public void AddLinkOnInput(Module module, int inputNb = 0, int linkedModuleInputNb = 0){
        inputs[inputNb].Add((module,linkedModuleInputNb));
    }
    public void RemoveLinkOnInput(Module module, int inputNb = 0, int linkedModuleInputNb = 0){
        inputs[inputNb].Remove((module,linkedModuleInputNb));
    }



    public void AddListener(int outputNb, Module.TriggerOutput func)
    {
        outputs[outputNb].linkedModule.AddListener(outputs[outputNb].linkedModuleOutputNb,func);
    }
    public void SetLinkOnOutput(Module module, int outputNb = 0, int linkedModuleOutputNb = 0){
        outputs[outputNb] = (module,linkedModuleOutputNb);
    }
    public void UnsetLinkOnOutput(int outputNb){
        outputs[outputNb] = (null,0);
    }
}