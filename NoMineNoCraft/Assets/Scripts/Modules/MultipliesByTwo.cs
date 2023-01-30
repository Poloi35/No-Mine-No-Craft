using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This mod takes one input and outputs its value multiplied by two
public class MultipliesByTwo : BasicModule
{

    public override void Execute()
    {
        TriggerValueChangement(inputs[0].Value * 2);  // Ça ça crash si inputs[0] contient "null" mais c'est pas censé être le cas avec
                                                      // le reste du programme qui appelle execute que quand tout ce qu'il y a dans inputs est rempli
                                                      // Si besoin ajouter un if (inputs[0] == null) avant le trigger mais normalement pas besoin mdr
    }

    public override int GetNumberOfInputs(){
        return 1;
    }

    public override int GetNumberOfOutputs()
    {
        return 1;
    }
}