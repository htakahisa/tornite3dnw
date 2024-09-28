using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CameraController : MonoBehaviourPunCallbacks {
    float x, z;
    float speed = 0.5f;

    private GameObject cam;
    Quaternion cameraRot, characterRot;
    float Xsensityvity = 1f, Ysensityvity = 1f;

    //変数の宣言(角度の制限用)
    float minX = -90f, maxX = 90f;



    //　通常のジャンプ力
    [SerializeField]
    private float jumpPower = 50000f;
    private Rigidbody rb;
    private float distanceToGround = 1f;


    // Start is called before the first frame update
    void Start() {

        rb = GetComponent<Rigidbody>();

        // マウスを中央で固定
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update() {






        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Cursor.lockState == CursorLockMode.Locked) {
                Cursor.lockState = CursorLockMode.None;
            } else {
                Cursor.lockState = CursorLockMode.Locked;
            }
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
        jump();

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

    private void jump() {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            ////　走って移動している時はジャンプ力を上げる
            //if (runFlag && velocity.magnitude > 0f) {
            //    velocity.y += dashJumpPower;
            //} else {
            //    velocity.y += jumpPower;
            //}
            //velocity.y += jumpPower;
            rb.AddForce(new Vector3(0, jumpPower, 0));

        }
    }

    private void FixedUpdate() {

        if (photonView == null || !photonView.IsMine) {
            return;
        }
        x = 0;
        z = 0;
        x = Input.GetAxisRaw("Horizontal") * speed;
        z = Input.GetAxisRaw("Vertical") * speed;
        if (z != 0 && x != 0 || Input.GetKey(KeyCode.LeftShift)) {
            this.speed = 0.05f;
        } else {
            this.speed = 0.1f;
        }
        x = Input.GetAxisRaw("Horizontal") * speed;
        z = Input.GetAxisRaw("Vertical") * speed;

        Vector3 comFoward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;
        Vector3 pos = comFoward * z + cam.transform.right * x;
        transform.position += pos;


        //transform.position += cam.transform.forward * z + cam.transform.right * x;

    }

    public void UpdateCursorLock() {
        if (photonView == null || !photonView.IsMine) {
            return;
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
    public void SensityvityChange(float sensi) {
        Xsensityvity = sensi;
        Ysensityvity = sensi;

    }

    private bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround);
    }


    protected bool WallCheck(Vector3 targetPosition, Vector3 desiredPosition, LayerMask wallLayers, out Vector3 wallHitPosition) {
        if (Physics.Raycast(targetPosition, desiredPosition - targetPosition, out RaycastHit wallHit, Vector3.Distance(targetPosition, desiredPosition), wallLayers, QueryTriggerInteraction.Ignore)) {
            wallHitPosition = wallHit.point;
            return true;
        } else {
            wallHitPosition = desiredPosition;
            return false;
        }
    }






}
