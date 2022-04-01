using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviourPunCallbacks {



    float x, z;
    float speed = 0.2f;

    private GameObject cam;
    Quaternion cameraRot, characterRot;
    float Xsensityvity = 0.5f, Ysensityvity = 0.5f;

    bool cursorLock = true;

    //変数の宣言(角度の制限用)
    float minX = -90f, maxX = 90f;

    // Start is called before the first frame update
    void Start() {


        // マウスを中央で固定
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.W)) {
            Vector3 velocity = cam.transform.rotation * new Vector3(speed * 5, 0, speed);
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            Vector3 velocity = cam.transform.rotation * new Vector3(-speed * 5, 0, speed);
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            transform.position += new Vector3(-0.8f, 0, z);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            transform.position += new Vector3(x, 0, 0.8f);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            transform.position += new Vector3(x, 2, z);
        }

            if (photonView == null || !photonView.IsMine) {
            return;
        }

        if (GetComponentInChildren<Camera>() == null) {
            return;
        } else {
            cam = GetComponentInChildren<Camera>().gameObject;
            cameraRot = cam.transform.localRotation;
            characterRot = transform.localRotation;
        }

        move();

        UpdateCursorLock();
    }

    private void move() {

        float xRot = Input.GetAxis("Mouse X") * Ysensityvity;
        float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        //Updateの中で作成した関数を呼ぶ
        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;
    }


 


    public void UpdateCursorLock() {
        if (photonView == null || !photonView.IsMine) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            cursorLock = false;
        } else if (Input.GetMouseButton(0)) {
            cursorLock = true;
        }


        if (cursorLock) {
            Cursor.lockState = CursorLockMode.Locked;
        } else if (!cursorLock) {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //角度制限関数の作成
    private Quaternion ClampRotation(Quaternion q) {
        if (q.x == 0 && q.y == 0 && q.z == 0) {
            q.w = 1f;
            return q;
        }

        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        angleX = Mathf.Clamp(angleX, minX, maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }
}

   
   


