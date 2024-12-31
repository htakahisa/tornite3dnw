using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Eagle : MonoBehaviourPun
{
    private float speed = 8;      // �O�i���x
    private float sensitivity = 70f;  // �}�E�X���x

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
            // Rigidbody �R���|�[�l���g�̎擾
            rb = GetComponent<Rigidbody>();

            playerCamera = Camera.main.gameObject;
            playerCamera.SetActive(false);
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

        // �J�����̌����Ă��鐳�ʕ������擾
        Vector3 forwardDirection = cam.transform.forward;

        // �J�����̐��ʕ����Ɉړ�����
        Vector3 newPosition = rb.position + forwardDirection * speed * Time.fixedDeltaTime;

        // Rigidbody��MovePosition���g���Ĉړ�
        rb.MovePosition(newPosition);
    }





}



