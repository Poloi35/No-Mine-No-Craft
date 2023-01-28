using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This class isn't a definitive class but only a test class that will allow me to well test stuff. 90% of what's here will probably go to garbage once its finished
public class BotShoulder : MonoBehaviour
{


    public Module croissantMod1;
    public Module multi;
    public Module add;
    public Module log1;
    public Module log2;
    public Module pol2cart;
    public Module constant1;
    public Module constant2;

    private void Update()
    {        
        constant1?.Execute();
        constant2?.Execute();
    }

    private void Awake()
    {
        pol2cart = new PolarToCartesian();
        log1 = new DebugLogger("Je suis log1 et je vous apporte l'information :\n");
        log2 = new DebugLogger("Je suis log2 et je vous apporte l'information :\n");
        constant1 = new ConstantNumber(7);
        constant2 = new ConstantNumber(0);

        pol2cart.LinkToOutput(constant1,0);
        pol2cart.LinkToOutput(constant2,1);
        log1.LinkToOutput(pol2cart,outputNb:0);
        log2.LinkToOutput(pol2cart,outputNb:1);
    }
}