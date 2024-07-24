using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainManager : MonoBehaviour
{
    public OSC osc; 
    private ParticleSystem.MainModule ps;
    private ParticleSystem.EmissionModule emissionMod;
    public string emit = "/emit";
    public string size = "/size";
    public string start = "/start";

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>().main;
        emissionMod = GetComponent<ParticleSystem>().emission;

        if (osc) {
            osc.SetAddressHandler(emit, OnReceiveEmission);
            osc.SetAddressHandler(size, OnReceiveSize);
            osc.SetAddressHandler(start, OnReceiveStart);
        }

        emissionMod.enabled = false;
    }

    void OnReceiveEmission(OscMessage message) {
        float e = message.GetFloat(0);
        emissionMod.rateOverTime = e;
    }
    
    void OnReceiveSize(OscMessage message) {
        float s = message.GetFloat(0);

        ParticleSystem.MinMaxCurve psmmc = new ParticleSystem.MinMaxCurve(s, s+0.1f);
        psmmc.mode = ParticleSystemCurveMode.TwoConstants;

        
        ps.startSizeX = psmmc;
        ps.startSizeZ = psmmc;

    }
    
    bool enable = false;
    void OnReceiveStart(OscMessage message) {
        int val = message.GetInt(0);

        if (val == 1) {
            enable = true;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (enable) {
            emissionMod.enabled = true;
        }
    }
}



/*
NOTES

*Default Settings*

PARTICLE SYSTEMS
Emission-Rate over Time: 1000
Rainfall-3D Start Size: X(0.2, 0.3) Y(1,10) Z(0.2, 0.3)

*More Intense Maximums*

PARTICLE SYSTEMS
Emission-Rate over time: 10000
Rainfall-3D Start Size: X(0.3, 0.4) Y(1,10) Z(0.3, 0.4)
*/