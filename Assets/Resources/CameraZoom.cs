using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) { 
            cam.fieldOfView -= 20;
        }
        if (Input.GetMouseButtonUp(1)) {
            cam.fieldOfView += 20;
        }

    }
}
