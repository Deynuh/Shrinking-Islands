using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{

    public OSC osc;
    public string position = "/pos";
    public GameObject[] particleSystems;
    private ParticlePosition[] particles;

    // Start is called before the first frame update
    void Start()
    {
        particles = new ParticlePosition[particleSystems.Length];
        if (osc) osc.SetAddressHandler(position, OnReceivePosition);
        int counter = 0;
        foreach (GameObject g in particleSystems)
        {
            particles[counter++] = g.GetComponent<ParticlePosition>();
        }
        Debug.Log("there are " + particles.Length + " particle Systems");

    }

    void OnReceivePosition(OscMessage message)
    {
        if (message.Count() < 2) return;
        Vector3 pos = new Vector3(message.GetFloat(0), message.GetFloat(1), 0.0f);
        foreach (ParticlePosition p in particles) p.UpdatePosition(pos);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
