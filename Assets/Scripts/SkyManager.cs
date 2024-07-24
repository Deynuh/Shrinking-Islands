using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class SkyManager : MonoBehaviour
{
    public OSC osc;
    public string changeAddress = "/change";
    private HDAdditionalLightData lightTemp;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (osc) {
            osc.SetAddressHandler(changeAddress, OnChange);
        }

        lightTemp = GetComponent<HDAdditionalLightData>();
    }

    bool shouldChange = false;
    void OnChange(OscMessage message) {
        int val = message.GetInt(0);
        if (val == 1) {
            shouldChange = true;
        }
        //float e = message.GetFloat(0);
        //emissionMod.rateOverTime = e;
    }

    float rotationChange = 9;
    float stopChecker = 9; //same as rotationChange but will be manipulated
    float seconds = 20;
    float changeRate = 0;


    // how many units light temp changes by in Kelvin
    float lightTempChange = 5050;
    float lightTempChangeRate = 0;
    float initialLight = 6450;
    float lightNumber = 0;
    // Update is called once per frame
    void Update()
    {
        if (shouldChange) {
            changeRate = (rotationChange / seconds) / 24;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - changeRate, 
            transform.localEulerAngles.y, transform.localEulerAngles.z);
            
            lightTempChangeRate = (lightTempChange / seconds) / 24;
            lightNumber = initialLight += lightTempChangeRate;
            lightTemp.SetColor(Color.white, lightNumber);

            stopChecker -= changeRate;
            if (stopChecker <= 0) {
                shouldChange = false;
            }
        }
    }
}


/* 
NOTES

*default*
DIRECTIONAL LIGHT 
Emission-Temperature: 6450
Rotation: X 3.0 DONE

*new*
DIRECTIONAL LIGHT
Emission-Temperature: 11500?
Rotation: X -6.0 DONE
*/