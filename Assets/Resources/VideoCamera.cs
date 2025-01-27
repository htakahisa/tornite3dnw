using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VideoCamera : MonoBehaviour
{

    [SerializeField]
    GameObject camera;

    [SerializeField]
    GameObject secondCamera;

    // Start is called before the first frame update
    void Start()
    {
        camera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            camera.SetActive(true);
            CinemachineVirtualCamera virtualCamera = camera.GetComponent<CinemachineVirtualCamera>();
            GameObject targetObject = GameObject.Find("Arte(Clone)");
            virtualCamera.Follow = targetObject.transform;
            virtualCamera.LookAt = targetObject.transform;
        }

      

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            secondCamera.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            camera.SetActive(false);

        }
#endif

    }
}
