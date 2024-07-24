using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Rendering.HighDefinition;

public class TriggerLightning : MonoBehaviour
{
    // Start is called before the first frame update

    public OSC osc;
    public string triggerAddress = "/triggerLight";
    private VisualEffect vfx;
    private int shaderID;
    
    private int shaderID1;
    private int shaderID2;


    //light stuff
    //public GameObject light;
    //private HDAdditionalLightData lightData;

    void Start()
    {
        if (osc) osc.SetAddressHandler(triggerAddress, OnReceiveTrigger);
        vfx = GetComponent<VisualEffect>();
        shaderID = Shader.PropertyToID("OnTrigger");
        shaderID1 = Shader.PropertyToID("OnTrigger1");
        shaderID2 = Shader.PropertyToID("OnTrigger2");

        //lightData = GetComponent<HDAdditionalLightData>(); 
    }

    // Update is called
    int counter = 0;
    void OnReceiveTrigger(OscMessage message)
    {
        int val = message.GetInt(0);
        if (val == 1) {
            if (counter == 0) {
                vfx.SendEvent(shaderID);
            }
            else if (counter == 1) {
                vfx.SendEvent(shaderID1);
            }
            else if (counter == 2) {
                vfx.SendEvent(shaderID2);
            }
            counter++;
            if (counter >= 3) counter = 0;
            
            //lightData.SetIntensity(40f);
        }


        //also change directional light?

    }
    void Update()
    {
        
    }
}


/*
light intensity default: 32332.46
                new:    31539.53
*/