using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSinking : MonoBehaviour
{
    public OSC osc;
    public string sinkAddress = "/sink";

    //how many units the island sinks by to be submerged completely
    float heightChange = 380; //-47 to -427
    //time for island to be submerged completely in minutes
    float minutes = 4; 
    float sinkRate = 0;

    float stopChecker = 380; //height change duplicate for iterator
    

    // Start is called before the first frame update
    void Start()
    {
        if (osc) {
            osc.SetAddressHandler(sinkAddress, OnSink);
        }

        //sinkRate = (heightChange / (minutes * 60)) / 24;
        sinkRate = (heightChange / (minutes * 60));
    }

    bool shouldSink = false;
    void OnSink(OscMessage message) {
        int val = message.GetInt(0);

        if (val == 1) {
            shouldSink = true;
        }
    }

   
    // Update is called once per frame
    void Update()
    {
        //((height change) / minutes * 60) / 24
        if (shouldSink) {
            transform.position = new Vector3(0.0f, transform.position.y - (sinkRate * Time.deltaTime), 0.0f);
            //transform.position = new Vector3(0.0f, transform.position.y-0.0554166667f, 0.0f);
            stopChecker -= sinkRate * Time.deltaTime;
            if (stopChecker <= 0) {
                shouldSink = false;
            }
        }
    }
}
