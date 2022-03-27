using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviourPunCallbacks
{
    float x, z;
    float speed = 0.1f;

    private GameObject cam;
    Quaternion cameraRot, characterRot;
    float Xsensityvity = 0.3f, Ysensityvity = 0.3f;

    bool cursorLock = true;

    //�ϐ��̐錾(�p�x�̐����p)
    float minX = -90f, maxX = 90f;

    // Start is called before the first frame update
    void Start()
    {


        // �}�E�X�𒆉��ŌŒ�
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView == null || !photonView.IsMine)
        {
            return;
        }

        if (GetComponentInChildren<Camera>() == null)
        {
            return;
        } else
        {
            cam = GetComponentInChildren<Camera>().gameObject;
            cameraRot = cam.transform.localRotation;
            characterRot = transform.localRotation;
        }

        move();

        UpdateCursorLock();
    }

    private void move()
    {

        float xRot = Input.GetAxis("Mouse X") * Ysensityvity;
        float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        //Update�̒��ō쐬�����֐����Ă�
        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;
    }


    private void FixedUpdate()
    {
        if (photonView == null || !photonView.IsMine)
        {
            return;
        }
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;
        z = Input.GetAxisRaw("Vertical") * speed;

        //transform.position += new Vector3(x,0,z);

        transform.position += cam.transform.forward * z + cam.transform.right * x;
    }


    public void UpdateCursorLock()
    {
        if (photonView == null || !photonView.IsMine)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;
        }
        else if (Input.GetMouseButton(0))
        {
            cursorLock = true;
        }


        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //�p�x�����֐��̍쐬
    private Quaternion ClampRotation(Quaternion q)
    {
        if (q.x == 0 && q.y == 0 && q.z == 0)
        {
            q.w = 1f;
            return q;
        }

        //q = x,y,z,w (x,y,z�̓x�N�g���i�ʂƌ����j�Fw�̓X�J���[�i���W�Ƃ͖��֌W�̗ʁj)

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
