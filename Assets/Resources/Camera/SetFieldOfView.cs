using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFieldOfView : MonoBehaviour
{

    private Camera camera;
    private Camera maincamera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        maincamera = transform.parent.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        camera.fieldOfView = maincamera.fieldOfView;
    }
}
