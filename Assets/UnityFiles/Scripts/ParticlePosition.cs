using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ParticlePosition : MonoBehaviour
{

    public Camera mainCamera;
    public float distanceFromCamera = 12.0f;
    public float speed = 4.0f;

    private Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        mousePosition = new Vector3(0, 0, distanceFromCamera);

    }

 

    // Update is called once per frame

    public void UpdatePosition(Vector3 newPos)
    {
        mousePosition = new Vector3(newPos.x, newPos.y, newPos.z);
    }

    void Update()
    {
       mousePosition.z = distanceFromCamera;
       Vector3 mouseScreenToWorld = mainCamera.ScreenToWorldPoint(mousePosition);
       Vector3 pos = Vector3.Lerp(transform.position, mouseScreenToWorld, 1.0f - Mathf.Exp(-speed * Time.deltaTime));
       transform.position = pos;

    }
}
