using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Eagle : MonoBehaviourPun
{
    private float speed = 8;      // 前進速度
    private float sensitivity = 70f;  // マウス感度

    private GameObject playerCamera;
    private Transform cam;

    private float rotationX = 0f;
    private float rotationY = 0f;
    private Rigidbody rb;



    void Awake()
    {

        if (photonView.IsMine)
        {
            cam = transform.GetChild(0);
            // Rigidbody コンポーネントの取得
            rb = GetComponent<Rigidbody>();

            playerCamera = Camera.main.gameObject;
            playerCamera.SetActive(false);
            gameObject.SetActive(true);


            // Rigidbody を使って回転させるために、回転を物理エンジンで制御しない
            rb.freezeRotation = true;
            Invoke("Destroy", 10f);
        }
    }

    private void Destroy()
    {
        if (photonView.IsMine)
        {
            cam.gameObject.SetActive(false);
            playerCamera.SetActive(true);
        }
        PhotonNetwork.Destroy(gameObject);
    }

    void Update()
    {

        if (cam == null)
        {
            return;
        }
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 currentRotation = cam.transform.localEulerAngles;



        if (currentRotation.y > 180f)
        {
            currentRotation.y -= 360f;
        }
        if (currentRotation.x > 180f)
        {
            currentRotation.x -= 360f;
        }

        currentRotation.x -= mouseY;
        currentRotation.y += mouseX;


        cam.transform.localEulerAngles = currentRotation;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            Destroy();
        }




    }

    void FixedUpdate()
    {
        if (cam == null)
        {
            return;
        }

        // カメラの向いている正面方向を取得
        Vector3 forwardDirection = cam.transform.forward;

        // カメラの正面方向に移動する
        Vector3 newPosition = rb.position + forwardDirection * speed * Time.fixedDeltaTime;

        // RigidbodyのMovePositionを使って移動
        rb.MovePosition(newPosition);
    }





}



