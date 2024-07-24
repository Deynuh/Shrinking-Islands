using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnControl : MonoBehaviour
{
    public OSC osc;
    public VisualEffect vfx;
    public string oscAddress = "/spawn";
    public string spawnVariable = "spawnRate";
    public float minAmp = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        if (osc) osc.SetAddressHandler(oscAddress, OnReceiveSpawn);
    }

    void OnReceiveSpawn(OscMessage message)
    {
        float val = message.GetFloat(0);
        Mathf.Clamp(val, 0.0f, 1.0f);
        if (vfx)
        {
            if (val < minAmp) val = 0.0f;
            vfx.SetFloat(spawnVariable, val);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
