using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CameraController : MonoBehaviourPunCallbacks {
    float x, z;
    float speed = 0.04f;

    private float stepTimer = 0f;
    public float wallDetectionDistance = 0.5f;

    private Vector3 moveDirection;

    private Vector3 lastMoveInput;

    private float servicespeed = 1;

    private Vector3 airMoveVector;
    private int hasAppliedAirMove = 0;

    private GameObject cam;
    Quaternion cameraRot, characterRot;
    static float NormalSensityvity = 1f;
    float Xsensityvity = 1f, Ysensityvity = 1f;

    private float zoomSensitivityMultiplier = 0.25f;


    private float cowardRange = 10f;

    //�ϐ��̐錾(�p�x�̐����p)
    float minX = -90f, maxX = 90f;

    private bool IsJump = false;

    public GameObject flash; // �O���l�[�h�̃v���n�u
    private float throwForce = 4f; // �������
    private float upwardForce = 1.5f; // ������̗�

    GameObject coward;
    private Ability ability;

    // �G�t�F�N�g�����������ʒu�̃I�t�Z�b�g
    public Vector3 effectOffset = new Vector3(0, 1, 2);

    // �G�t�F�N�g�����������ʒu�̃I�t�Z�b�g
    public Vector3 diable = new Vector3(0, 1, 2);

    private bool IsKamaitachi = false;  

    //�@�ʏ�̃W�����v��
    [SerializeField]
    private float jumpPower = 20000;
    private Rigidbody rb;
    private float distanceToGround = 0.03f;

    RayController rc;

    // Start is called before the first frame update
    void Start() {

        rb = GetComponent<Rigidbody>();
        // �Q�[���J�n���Ƀ}�E�X���\���ɂ��A�����Ƀ��b�N����
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        ability = Camera.main.transform.parent.GetComponent<Ability>();

        coward = (GameObject)Resources.Load("SurveillanceCamera");

    }

    // Update is called once per frame
    void Update() {


        if (Input.GetKeyDown(KeyCode.L)) {
           //Aqua();
        }




        // Escape�L�[�������ꂽ�Ƃ��̏���
        if (Input.GetKeyDown(KeyCode.Escape)) {
            // �}�E�X���\������Ă��Ȃ��ꍇ�͕\�����A���R�ɓ�������悤�ɂ���
            if (!Cursor.visible) {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            // �}�E�X���\������Ă���ꍇ�͔�\���ɂ��A�����Ƀ��b�N����
            else {
                Cursor.visible = false;
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
            this.rc = cam.GetComponent<RayController>();
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

        //Update�̒��ō쐬�����֐����Ă�
        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;
    }
    private IEnumerator Stun(float duration, float magnitude) {
        float elapsed = 0.0f;

        while (elapsed < duration) {

            recoil(Random.Range(-1f, 1f) * magnitude, Random.Range(-1f, 1f) * magnitude);

            elapsed += Time.deltaTime;

            yield return null;
        }

        
    }



    private void FixedUpdate() {


     

        
        if (photonView == null || !photonView.IsMine) {
            return;
        }


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
            Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

            if (IsGrounded()) {

                //// �C����̈ړ������ŃL�����N�^�[���ړ�
                //rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
                x = Input.GetAxisRaw("Horizontal") * speed;
                z = Input.GetAxisRaw("Vertical") * speed;
                float ratio = 1f;
                bool issnake = false;
                if (z != 0 && x != 0) {
                    ratio /= 2;
                }
                if (Input.GetKey(KeyCode.LeftShift)) {
                    ratio /= 1.5f;
                    issnake = true;
                }
                if (z != 0 || x != 0) {
                    if (!issnake) {
                        stepTimer += Time.deltaTime;
                        // ������0.5�b���ɍĐ�
                        if (stepTimer >= 0.5f) {
                            SoundManager.sm.PlaySound("walk");
                            stepTimer = 0f; // �^�C�}�[�����Z�b�g
                        }
                    }
                    //rb.velocity = new Vector3(0, rb.velocity.y, 0);  // ���x���Z�b�g
                    this.speed = 0.04f * ratio * servicespeed;
                    hasAppliedAirMove = 0;
                    lastMoveInput = Vector3.zero;

                }
            } else if (hasAppliedAirMove <= 1 && moveInput != Vector3.zero) {
                // �󒆂ł̈ړ������F�ړ����͂��ύX���ꂽ�Ƃ��̂ݎ��s
                if (moveInput != lastMoveInput) {
                    x = Input.GetAxisRaw("Horizontal") * speed;
                    z = Input.GetAxisRaw("Vertical") * speed;
                    float ratio = 0.5f;
                    bool issnake = false;
                    //if (z != 0 && x != 0) {
                    //    ratio /= 2;
                    //}
                    if (Input.GetKey(KeyCode.LeftShift)) {
                        ratio /= 1.5f;
                        issnake = true;
                    }
                    if (z != 0 || x != 0) {
                        if (!issnake) {
                            stepTimer += Time.deltaTime;
                            // ������0.5�b���ɍĐ�
                            if (stepTimer >= 0.5f) {
                                SoundManager.sm.PlaySound("walk");
                                stepTimer = 0f; // �^�C�}�[�����Z�b�g
                            }
                        }
                        //rb.velocity = new Vector3(0, rb.velocity.y, 0);  // ���x���Z�b�g
                        this.speed = 0.04f * ratio * servicespeed;
                        lastMoveInput = moveInput;
                    }
                    //// �C����̈ړ������ŃL�����N�^�[���ړ�
                    //rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
                    // �󒆂ł̈ړ�����x�����ݒ�
                    hasAppliedAirMove++;
                }
            }

            // ���͂���ړ�������ݒ�
            moveDirection = new Vector3(x, 0, z).normalized;

            // �ǂɐڐG���Ă��邩�ǂ������m�F
            if (moveDirection != Vector3.zero && IsWallInFront()) {
                // �ǂɉ��������Ɉړ����C��
                moveDirection = Vector3.ProjectOnPlane(moveDirection, GetWallNormal());
            }
            if (cam != null) {
                Vector3 comFoward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;
                Vector3 pos = comFoward * z + cam.transform.right * x;
                transform.position += pos;
            }

        }


    



        //x = Input.GetAxisRaw("Horizontal") * speed;
        //z = Input.GetAxisRaw("Vertical") * speed;



        //transform.position += cam.transform.forward * z + cam.transform.right * x;

        if (rc != null) {
            if (rc.GetIsZooming()) {
                Xsensityvity = NormalSensityvity * zoomSensitivityMultiplier;
                Ysensityvity = NormalSensityvity * zoomSensitivityMultiplier;
            } else {
                Xsensityvity = NormalSensityvity;
                Ysensityvity = NormalSensityvity;
            }
        }
    }


    // �ǂɐڐG���Ă��邩�ǂ����̔���
    private bool IsWallInFront() {
        RaycastHit hit;
        return Physics.Raycast(transform.position, moveDirection, out hit, wallDetectionDistance);
    }

    // �ǂ̖@���x�N�g�����擾
    private Vector3 GetWallNormal() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection, out hit, wallDetectionDistance)) {
            return hit.normal;
        }
        return Vector3.zero;
    }


    public void Stuned(float duration, float magnitude) {

        StartCoroutine(Stun(duration,magnitude));

    }

    private void recoil(float yRot, float xRot) {

     

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        //Update�̒��ō쐬�����֐����Ă�
        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;
    }

    private void jump() {




        if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.W) && isTouchingWall() && !IsGrounded() && !IsJump && IsKamaitachi) {
            hasAppliedAirMove++;
            IsJump = true;
            WallKickAction();
            IsJump = false;
        } else

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !IsJump) {
            IsJump = true;
            ////�@�����Ĉړ����Ă��鎞�̓W�����v�͂��グ��
            //if (runFlag && velocity.magnitude > 0f) {
            //    velocity.y += dashJumpPower;
            //} else {
            //    velocity.y += jumpPower;
            //}
            //velocity.y += jumpPower;
            rb.AddForce(new Vector3(0, jumpPower, 0));

            SoundManager.sm.PlaySound("jump");
            IsJump = false;
        }
           
        
    }

    public bool isTouchingWall() {

        RaycastHit hit;
        return Physics.Raycast(gameObject.transform.position, transform.forward, 0.5f);

    }        

            
    public void UpDraft() {
        if (photonView == null || !photonView.IsMine) {
            return;
        }
        if (IsGrounded()) { 
        rb.velocity = new Vector3(0, 0, 0);  // �c�����̑��x���Z�b�g
        rb.AddForce(new Vector3(0, jumpPower * 2, 0));
        SoundManager.sm.PlaySound("phantom");
        ability.Spend(2);
        }

    }

    public void Flash() {
        if (photonView == null || !photonView.IsMine) {
            return;
        }
        Throw();
    }
    public void Smoke() {
        if (photonView == null || !photonView.IsMine) {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            PhotonNetwork.Instantiate("WhiteSmoke", hit.point, Quaternion.identity);
        }

    }

    public void RireDuDiable() {
        if (photonView == null || !photonView.IsMine) {
            return;
        }

               
            PhotonNetwork.Instantiate("Diable", gameObject.transform.position, Quaternion.identity);
        

    }
    public void Coward() {
        if (photonView == null || !photonView.IsMine) {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Quaternion rota = transform.localRotation;
        rota *= Quaternion.Euler(0, 180, 0);
       
        if (Physics.Raycast(ray, out hit, cowardRange)) {
          
            Instantiate(coward, hit.point, rota);
            ability.Spend(2);
        }


    }

    public void Stray() {

        if (photonView == null || !photonView.IsMine) {
            return;
        }
        PinManager.pm.StrayChild();

    }





    public void BlueLight() {
        if (photonView == null || !photonView.IsMine) {
            return;
        }
        PinManager.pm.BlueLight();

    }

    public void Aqua()
    {
        if (photonView == null || !photonView.IsMine)
        {
            return;
        }
        PinManager.pm.Aqua();

    }


    void Throw() {
        Vector3 spawnDirection = Camera.main.transform.forward;
        Vector3 spawnPosition = transform.position + transform.forward * 1.3f + transform.up * 1.5f;
        GameObject grenade = PhotonNetwork.Instantiate("Flash", spawnPosition, Quaternion.LookRotation(spawnDirection));
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

    }

    public void StraDarts() {

        if (photonView == null || !photonView.IsMine) {
            return;
        }
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.2f;

        // �J�����̈ʒu���擾
        Vector3 cameraPosition = Camera.main.transform.position;

        // �J�����̌����Ă���������擾
        Vector3 spawnDirection = Camera.main.transform.forward;

        // �I�u�W�F�N�g�𐶐�
        PhotonNetwork.Instantiate("Stradarts", spawnPosition, Quaternion.LookRotation(spawnDirection));
    }
    public void Eagle() {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.2f;
        if (photonView == null || !photonView.IsMine) {
            return;
        }

        // �J�����̈ʒu���擾
        Vector3 cameraPosition = Camera.main.transform.position;

        // �J�����̌����Ă���������擾
        Vector3 spawnDirection = Camera.main.transform.forward;

        // �I�u�W�F�N�g�𐶐�
        PhotonNetwork.Instantiate("Eagle_Elite", spawnPosition, Quaternion.LookRotation(spawnDirection));
    }

    public void Wolf() {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.2f;
        if (photonView == null || !photonView.IsMine) {
            return;
        }

        // �J�����̈ʒu���擾
        Vector3 cameraPosition = Camera.main.transform.position;

        // �J�����̌����Ă���������擾
        Vector3 spawnDirection = Camera.main.transform.forward;

        // �I�u�W�F�N�g�𐶐�
        PhotonNetwork.Instantiate("Wolf", spawnPosition, Quaternion.LookRotation(spawnDirection));
    }

    public void Boostio() {
        if (IsGrounded() && !IsJump) {
            rb.velocity = new Vector3(0, 0, 0);  // ���x���Z�b�g
            IsJump = true;
            SoundManager.sm.PlaySound("boostio");
            // �O���l�[�h�ɗ͂�������
            Vector3 throwDirection = transform.forward*1.5f + transform.up;
            rb.AddForce(throwDirection * 4, ForceMode.VelocityChange);
            IsJump = false;
            ability.Spend(1);
        }

    }
    public void Kamaitachi() {

        if (photonView == null || !photonView.IsMine) {
            return;
        }
        RayController.rc.Classic();
        StartCoroutine(ChangeServiceSpeed());

    }

    void WallKickAction() {

        // �v���C���[�̔��Ε����ɕǃL�b�N�̗͂�������
        Vector3 kickDirection = -transform.forward*1.5f + Vector3.up*1.2f;
        rb.velocity = new Vector3(0, 0, 0);  // �c�����̑��x���Z�b�g
        rb.AddForce(kickDirection * 220, ForceMode.Impulse);

    }

    IEnumerator ChangeServiceSpeed() {
        // servicespeed��1.5�ɐݒ�
        servicespeed = 1.5f;
        IsKamaitachi = true;
        // 10�b�ҋ@
        yield return new WaitForSeconds(10f);

        // servicespeed��1�ɖ߂�
        servicespeed = 1.0f;
        IsKamaitachi = false;
    }


    public void UpdateCursorLock() {
        if (photonView == null || !photonView.IsMine) {
            return;
        }


    }

    //�p�x�����֐��̍쐬
    private Quaternion ClampRotation(Quaternion q) {
        if (q.x == 0 && q.y == 0 && q.z == 0) {
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
    public void SensityvityChange(float sensi) {
        //if (photonView == null || !photonView.IsMine) {
        //    return;
        //}
        NormalSensityvity = sensi;


    }

    public bool IsGrounded() {
 
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
