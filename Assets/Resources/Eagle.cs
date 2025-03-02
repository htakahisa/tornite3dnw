using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Eagle : MonoBehaviourPun
{
    private float speed = 8;      // 前進速度

    private Transform cam;


    private Rigidbody rb;



    void Awake()
    {

       
            cam = transform.GetChild(0);

        if (photonView.IsMine)
        {
            // Rigidbody コンポーネントの取得
            rb = GetComponent<Rigidbody>();

            gameObject.SetActive(true);


            // Rigidbody を使って回転させるために、回転を物理エンジンで制御しない
            rb.freezeRotation = true;
            Invoke("Destroy", 10f);
        }
        else
        {
            cam.gameObject.SetActive(false);
        }
    }

    private void Destroy()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.Instantiate("EagleCollect", transform.position, transform.rotation);
            PhotonNetwork.Destroy(gameObject);
        }
     
    }

    void Update()
    {


        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 currentRotation = transform.localEulerAngles;



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


        transform.localEulerAngles = currentRotation;

        if (Input.GetKeyDown(KeyCode.Q))
        {

            Destroy();
        }




    }

    void FixedUpdate()
    {

        // カメラの正面方向に移動する
        //Vector3 newPosition = transform.forward * speed * Time.fixedDeltaTime;

        transform.position += transform.forward * speed * Time.deltaTime;

        // RigidbodyのMovePositionを使って移動
        //rb.MovePosition(newPosition);
    }
     
    



}



