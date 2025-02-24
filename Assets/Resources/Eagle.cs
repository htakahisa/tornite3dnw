using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Eagle : MonoBehaviourPun
{
    private float speed = 8;      // �O�i���x


    private GameObject playerCamera;
    private Transform cam;


    private Rigidbody rb;



    void Awake()
    {

        if (photonView.IsMine)
        {
            cam = transform.GetChild(0);
            // Rigidbody �R���|�[�l���g�̎擾
            rb = GetComponent<Rigidbody>();

            gameObject.SetActive(true);


            // Rigidbody ���g���ĉ�]�����邽�߂ɁA��]�𕨗��G���W���Ő��䂵�Ȃ�
            rb.freezeRotation = true;
            Invoke("Destroy", 10f);
        }
    }

    private void Destroy()
    {
        if (photonView.IsMine)
        {
            cam.gameObject.SetActive(false);
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

        // �J�����̐��ʕ����Ɉړ�����
        //Vector3 newPosition = transform.forward * speed * Time.fixedDeltaTime;

        transform.position += transform.forward * speed * Time.deltaTime;

        // Rigidbody��MovePosition���g���Ĉړ�
        //rb.MovePosition(newPosition);
    }
     
    



}



