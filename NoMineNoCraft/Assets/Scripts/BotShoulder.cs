using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices;

//This class isn't a definitive class but only a test class that will allow me to well test stuff. 90% of what's here will probably go to garbage once its finished
public class BotShoulder : MonoBehaviour
{
    Module croissantMod1;
    private void Update()
    {        
        croissantMod1?.Execute();
    }

    private void Awake()
    {
        Module pol2cart = new PolarToCartesian();
        Module log1 = new DebugLogger("Je suis log1 et je vous apporte l'information :\n");
        Module log2 = new DebugLogger("Je suis log2 et je vous apporte l'information :\n");
        Module constant1 = new ConstantNumber(7);
        Module constant2 = new ConstantNumber(0);

        ModuleFunction multBy3 = new ModuleFunction();

        multBy3.CreateInput();
        multBy3.CreateOutput();

        Module multi = new MultipliesByTwo();
        multBy3.AddLinkOnInput(multi);
        Module add = new AddsTwoInputs();
        multBy3.AddLinkOnInput(add);
        add.LinkToOutput(multi,1);
        multBy3.SetLinkOnOutput(add);


        ModuleFunction adds3mod = new ModuleFunction();

        adds3mod.CreateInput();
        adds3mod.CreateInput();
        adds3mod.CreateInput();

        adds3mod.CreateOutput();

        Module add2 = new AddsTwoInputs();
        Module add3 = new AddsTwoInputs();

        adds3mod.AddLinkOnInput(add2,0,0);
        adds3mod.AddLinkOnInput(add2,1,1);
        adds3mod.AddLinkOnInput(add3,2,0);

        add3.LinkToOutput(add2,1);

        adds3mod.SetLinkOnOutput(add3);


        croissantMod1 = new FromZeroToOneMod();
        Module multi2 = new MultipliesByTwo();

        multi2.LinkToOutput(croissantMod1);

        multBy3.LinkToOutput(croissantMod1);

        adds3mod.LinkToOutput(multBy3,0);
        adds3mod.LinkToOutput(multi2,1);
        adds3mod.LinkToOutput(croissantMod1,2);
        log1.LinkToOutput(adds3mod);

        //Pas d'erreur mais rien s'affiche, croissantmod1 output bien, createInput a lieu (pas sur que ça mette qqch dedans mais ya pa de raison)
    }
}