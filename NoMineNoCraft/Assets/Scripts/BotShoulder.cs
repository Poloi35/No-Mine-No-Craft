using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This class isn't a definitive class but only a test class that will allow me to well test stuff. 90% of what's here will probably go to garbage once its finished
public class BotShoulder : MonoBehaviour
{

    public Module multi = new MultipliesByTwo();

    public Module croissantMod1 = new FromZeroToOneMod();

    public AddsTwoInputs add = new AddsTwoInputs();

    public Module log = new DebugLogger();

    private void Update()
    {        
        croissantMod1?.execute();
    }

    private void Awake()
    {
        //With this configuration I just debug.log 3 times the value of the FromZeroToOne mod
        multi.addInput(croissantMod1);

        add.addInput(croissantMod1);
        add.addInput(multi);

        log.addInput(add);
    }
}