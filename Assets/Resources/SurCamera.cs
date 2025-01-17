using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurCamera : MonoBehaviourPun {

    private Transform cam;
    private Transform sur;
    private GameObject playerCamera;
    private SoundManager sm;

    // Start is called before the first frame update
    void Start() {

    }

    private void Awake() {
        if (!photonView.IsMine)
        {
            return;
        }
        sm = Camera.main.transform.parent.GetComponent<SoundManager>();
        sm.PlaySound("coward");
        playerCamera = Camera.main.gameObject;
        playerCamera.SetActive(false);
        gameObject.SetActive(true);        
        Invoke("Destroy", 10f);
    }

    // Update is called once per frame
    void Update() {
        
        cam = transform.GetChild(0).GetChild(0);
        if (!photonView.IsMine)
        {
            cam.gameObject.SetActive(false);
            return;
        }
        sur = transform.GetChild(0);
        if (sur.gameObject.activeSelf) {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            Vector3 currentRotation = cam.transform.localEulerAngles;



            if (currentRotation.y > 180f) {
                currentRotation.y -= 360f;
            }
            if (currentRotation.x > 180f) {
                currentRotation.x -= 360f;
            }

            currentRotation.x -= mouseY;
            currentRotation.y += mouseX;

            currentRotation.y = Mathf.Clamp(currentRotation.y, -90, 90);
            currentRotation.x = Mathf.Clamp(currentRotation.x, -90, 90);


            cam.transform.localEulerAngles = currentRotation;
        }







        if (Input.GetKeyDown(KeyCode.Q)) {
            Switch();
        }

    }

    private void Switch() {

        sur.gameObject.SetActive(!sur.gameObject.activeSelf);
        playerCamera.SetActive(!playerCamera.activeSelf);

    }

    private void Destroy() {
        sur.gameObject.SetActive(false);
        playerCamera.SetActive(true);
        PhotonNetwork.Destroy(gameObject);
      
    }
}
