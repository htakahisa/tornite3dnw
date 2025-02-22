using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CameraController : MonoBehaviourPunCallbacks {
    float x, z;
    float speed = 1.8f;

    float OriginalSpeed = 1.8f;

    private float stepTimer = 0f;
    private float wallDetectionDistance = 0.17f;

    public GameObject WallCheck;

    private Vector3 lastMoveInput;

    public float servicespeed = 1;
    public LayerMask HitMask;

    private int hasAppliedAirMove = 0;

    private GameObject cam;
    Quaternion cameraRot, characterRot;
    static float NormalSensityvity = 1f;
    float Xsensityvity = 1f, Ysensityvity = 1f;

    private float zoomSensitivityMultiplier = 1f;

    public LayerMask AbilityHitMask;
    public LayerMask GroundLayer;

    private float cowardRange = 10f;

    private float crouchInterval = 0.5f;

    //�ϐ��̐錾(�p�x�̐����p)
    float minX = -90f, maxX = 90f;

    private bool IsJump = false;

    private GameObject stray;

    private float approach = 0.4f;

    private Ability ability;

    // �G�t�F�N�g�����������ʒu�̃I�t�Z�b�g
    public Vector3 effectOffset = new Vector3(0, 1, 2);

    // �G�t�F�N�g�����������ʒu�̃I�t�Z�b�g
    public Vector3 diable = new Vector3(0, 1, 2);

    public bool IsKamaitachi = false;

    //�@�ʏ�̃W�����v��

    private float jumpPower = 0.9f;
    // private Rigidbody rb;
    private float distanceToGround = 0.07f;

    RayController rc;
    private SoundManager sm;


    private float katarinaInterval = 0;
    public GameObject foot;
    // public GameObject stepClimbCheck;

    private Animator animator;

    private float gravity = -16f;    // �d�͂̋���

    private CharacterController controller;  // CharacterController�R���|�[�l���g
    private Vector3 velocity;


    public GameObject pinPrefab;

    public bool WalkAble = true;
    public bool AbilityAble = true;

    private bool isCursorLocked = true;


    private GameObject abilitycheck;

    private Coroutine currentCoroutine = null;

    private int multikatarina = 0;

    private float weaponspeed = 1;

    /** �_�̏�艺�� */
    private bool isNearBottomBar = false; // ���̃V�����_�[�ɐG��Ă��邩
    private Transform topBarTransform; // ��̃V�����_�[��Transform
    private Transform BottomBarTransform; // ��̃V�����_�[��Transform
    private bool isClimbing = false; // �o���Ă���Œ���
    private Vector3 targetPosition; // �ڕW�n�_
    private float climbSpeed = 2.0f; // �o�鑬�x
    private float climbSoundInterval = 0.5f;

    private string weapon;

    public int kamaitachi = 20;

    private float kamaitachiInterval = 0;

    public Vector3 boxSize = new Vector3(0.3f, 0.1f, 0.3f); // ����p��Box�T�C�Y

    // Start is called before the first frame update
    void Start() {

        if (PhaseManager.pm == null)
        {
            return;
        }



        //rb = GetComponent<Rigidbody>();
        // �Q�[���J�n���Ƀ}�E�X��\��
        if (photonView != null)
        {
            if (photonView.IsMine)
            {

                if (!Application.isEditor)
                {
                    abilitycheck = Instantiate(ResourceManager.resourcemanager.GetObject("AbilityCheck"), new Vector3(0, -100, 0), Quaternion.identity);
                    abilitycheck.SetActive(false);
                }

            }
        }

        NormalSensityvity = PlayerPrefs.GetFloat("Sensitivity");

        ability = GetComponent<Ability>();
        sm = GetComponent<SoundManager>();


        animator = GetComponent<Animator>();
        // CharacterController���擾
        controller = GetComponent<CharacterController>();

        SetCursorState(isCursorLocked);

    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 boxCenter = transform.position + Vector3.down * 0.17f;
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2)) // �}�E�X��������
        {
            PlacePinAtClickPosition();
        }
        if (rc != null)
        {
            if (rc.GetWeaponNumber() == 13)
            {
                weaponspeed = 1.4f;
            }
            else
            {
                weaponspeed = 1f;
            }
        }




#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.L))
        {

            ability.number1++;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {

            Flash();
        }
#endif

        if (AbilityAble)
        {
            
            if (ability != null && ability.GetAbility(1) == "Kamaitachi")
            {

                if (PhaseManager.pm.GetPhase().Equals("Battle"))
                {

                    kamaitachiInterval += Time.deltaTime;

                    if (kamaitachiInterval >= 0.2f)
                    {
                        if (IsKamaitachi)
                        {
                            kamaitachi -= 2;
                        }
                        if (!IsKamaitachi)
                        {
                            kamaitachi++;
                        }
                        kamaitachiInterval = 0f;
                    }

                    if (kamaitachi <= 0)
                    {
                        StopKamaitachi();
                        kamaitachi = 0;
                    }

                    if (kamaitachi >= 100)
                    {
                        kamaitachi = 100;
                    }

                    AbilityMeter.abilitymeter.SetValue(kamaitachi);

                }



            }
        }


        // Escape�L�[�Ń}�E�X�̃��b�N�ƕ\����؂�ւ���
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCursorLocked = !isCursorLocked;
            SetCursorState(isCursorLocked);
        }




        if (photonView == null || !photonView.IsMine)
        {
            return;
        }

        if (GetComponentInChildren<Camera>() == null)
        {
            return;
        }
        else
        {
            cam = GetComponentInChildren<Camera>().gameObject;
            cameraRot = cam.transform.localRotation;
            characterRot = transform.localRotation;
            this.rc = RayController.rc;
        }

        move();
        jump();

        UpdateCursorLock();

    }

    private void SetCursorState(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked; // �}�E�X����ʒ����ɌŒ�
            Cursor.visible = false;                  // �}�E�X�J�[�\�����\��
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;  // �}�E�X���b�N����
            Cursor.visible = true;                   // �}�E�X�J�[�\����\��
        }
    }

    private void DestroyPun()
    {
        BattleData.bd.DetectEnd();
        ScanCamera.sc.InActiveScan();
    }

    public void Dead() {
        PhotonNetwork.Destroy(gameObject);

    }



    public void move() {

        float xRot = Input.GetAxis("Mouse X") * Ysensityvity;
        float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        //Update�̒��ō쐬�����֐����Ă�
        cameraRot = ClampRotation(cameraRot);
        if (cam != null)
        {
            cam.transform.localRotation = cameraRot;
        }
        transform.localRotation = characterRot;
    }
    private IEnumerator Stun(float duration, float magnitude) {
        float elapsed = 0.0f;

        while (elapsed < duration) {

            StartCoroutine(recoil(Random.Range(-1, 2) * magnitude, magnitude, 0.1f));

            elapsed += Time.deltaTime;

            yield return null;
        }


    }
    void PlacePinAtClickPosition()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100, GroundLayer))
        {
            Debug.Log(hit.collider.gameObject.name);
            Instantiate(pinPrefab, hit.point, Quaternion.identity);

        }

    }



    /**
     * �_�̏�艺��
     * */
    private void StartClimbing()
    {

        BarStatus barstatus = topBarTransform.GetComponent<BarStatus>();

        if (PhaseManager.pm.GetPhase() == "Buy")
        {
            if (!barstatus.GetCanBuyPhase())
            {
                return;
            }
        }
        climbSoundInterval = 0;
        transform.position = BottomBarTransform.position;
        AbilityAble = false;
        WalkAble = false;
        rc.CanShoot = false;

        climbSpeed = barstatus.GetSpeed();
        if (topBarTransform != null)
        {
            // �ڕW�n�_����̃V�����_�[�̏㕔�ɐݒ�
            targetPosition = topBarTransform.position + Vector3.up * (topBarTransform.localScale.y / 2);
            isClimbing = true;

        }
    }

    private void MoveTowardsTarget()
    {

        float adjustedClimbSpeed = climbSpeed;
        climbSoundInterval -= Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftControl)) {
            adjustedClimbSpeed = adjustedClimbSpeed / 2;
        }
        // �����L�[��������Ă���ꍇ�͉������Ȃ��B
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            if (climbSoundInterval <= 0)
            {
                sm.PlaySound("barClimb");
                climbSoundInterval = 0.1f;
            }
        }

        // ���݂̈ʒu����ڕW�n�_�Ɍ������ď��X�Ɉړ�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, adjustedClimbSpeed * Time.deltaTime);

        // �ڕW�n�_�ɓ��B������ړ����~
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            isClimbing = false;
            AbilityAble = true;
            WalkAble = true;
            rc.CanShoot = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // �V�����_�[�ɐG�ꂽ�ꍇ
        if (other.CompareTag("Bar"))
        {

            isNearBottomBar = true;

            // �G�ꂽ�̂����̏ꍇ
            if (other.name == "bottom_bar")
            {

                // �e�I�u�W�F�N�g (bar) ���擾
                Transform barParent = other.transform.parent;

                ClimbBar climbbar = barParent.GetComponent<ClimbBar>();

                // barParent ���� top_bar ��T��
                topBarTransform = climbbar.GetTopBar();
                BottomBarTransform = climbbar.GetBottomBar();
            }
            else
            {


                // �e�I�u�W�F�N�g (bar) ���擾
                Transform barParent = other.transform.parent;

                ClimbBar climbbar = barParent.GetComponent<ClimbBar>();

                // barParent ���� top_bar ��T��
                BottomBarTransform = climbbar.GetTopBar();
                topBarTransform = climbbar.GetBottomBar();
            }
        }
    }




    private void FixedUpdate() {

        if (photonView == null || !photonView.IsMine) {
            return;
        }


        // �o�[���߂��AC�L�[���������ꍇ�ɓo��
        if (isNearBottomBar && Input.GetMouseButton(3) && !isClimbing)
        {
            StartClimbing();
            return;
        }

        // ��艺�肵�Ă���ꍇ�A�ړ��������s��
        if (isClimbing)
        {
            MoveTowardsTarget();
            return;
        }
        else
        {
            isNearBottomBar = false;
        }



        // �n�ʂɂ���ꍇ�A�������x�����Z�b�g
        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2f; // �������̒l�ɂ��Ēn�ʂɖ���������
            velocity.x = 0f;  // ���������̑��x�����Z�b�g
            velocity.z = 0f;
        }

        // �d�͂�K�p
        velocity.y += gravity * Time.fixedDeltaTime;

        // ���������̈ړ�
        controller.Move(velocity * Time.fixedDeltaTime);

        if (false)
        {
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                crouchInterval = 0;
            }

            if (Input.GetKey(KeyCode.LeftShift) && (crouchInterval >= 0.5f))
            {

                animator.SetBool("crouching", true);
                // GameObject�̌��݂�position���擾
                Vector3 newPosition = Camera.main.transform.position;

                // y���W��1�ɕύX
                newPosition.y = transform.position.y + 1;

                // �V����position��K�p
                Camera.main.transform.position = newPosition;



            }

            else
            {
                animator.SetBool("crouching", false);
                // GameObject�̌��݂�position���擾
                Vector3 newPosition = Camera.main.transform.position;

                // y���W��1.7�ɕύX
                newPosition.y = transform.position.y + 1.7f;

                // �V����position��K�p
                Camera.main.transform.position = newPosition;

                crouchInterval += Time.fixedDeltaTime;

            }



        }


        RaycastHit hit;


        if (!IsGrounded())
        {
            this.speed = OriginalSpeed * servicespeed * weaponspeed;
            Move();
        }


        

        if (WalkAble)
        {
            if (IsGrounded())
            {
                x = Input.GetAxisRaw("Horizontal") * speed;
                z = Input.GetAxisRaw("Vertical") * speed;
                hasAppliedAirMove = 0;
                lastMoveInput = Vector3.zero;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

                if (IsGrounded())
                {

                    //// �C����̈ړ������ŃL�����N�^�[���ړ�
                    //rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
                    x = Input.GetAxisRaw("Horizontal") * speed;
                    z = Input.GetAxisRaw("Vertical") * speed;
                    float ratio = 1f;
                    bool issnake = false;

                    if (z != 0 && x != 0)
                    {
                        ratio /= 2;

                    }
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        ratio /= 1.25f;
                        issnake = true;
                    }
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        ratio /= 2f;
                        issnake = true;

                    }

                    if (Input.GetMouseButton(1))
                    {
                        ratio /= 1.25f;
                    }
                    if (z != 0 || x != 0)
                    {
                        if (!issnake)
                        {
                            stepTimer += Time.fixedDeltaTime;
                            approach -= Time.fixedDeltaTime;
                            // ������0.5�b���ɍĐ�
                            if (stepTimer >= 0.4f && sm != null)
                            {
                                sm.PlaySound("walk");
                                stepTimer = 0f; // �^�C�}�[�����Z�b�g
                            }
                            if (approach >= 0)
                            {
                                ratio /= 2f;
                            }
                        }

                       

                        //rb.velocity = new Vector3(0, rb.velocity.y, 0);  // ���x���Z�b�g
                        this.speed = OriginalSpeed * ratio * servicespeed * weaponspeed;
                        

                    }
                    Move();
                }
                else if (hasAppliedAirMove <= 1 && moveInput != Vector3.zero)
                {

                    // �󒆂ł̈ړ������F�ړ����͂��ύX���ꂽ�Ƃ��̂ݎ��s
                    if (moveInput != lastMoveInput)
                    {

                        float ratio = 0.5f;
                        x = Input.GetAxisRaw("Horizontal") * speed;
                        z = Input.GetAxisRaw("Vertical") * speed;

                        bool issnake = false;

                        //if (z != 0 && x != 0) {
                        //    ratio /= 2;
                        //}
                        if (Input.GetKey(KeyCode.LeftControl))
                        {
                            ratio /= 1.25f;
                            issnake = true;
                        }
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            ratio /= 2f;
                            issnake = true;

                        }


                        if (z != 0 || x != 0)
                        {
                            if (!issnake)
                            {
                                stepTimer += Time.fixedDeltaTime;
                                // ������0.5�b���ɍĐ�
                                if (stepTimer >= 0.4f)
                                {
                                    sm.PlaySound("walk");
                                    stepTimer = 0f; // �^�C�}�[�����Z�b�g
                                }
                            }
                        }

                        this.speed = OriginalSpeed * ratio * servicespeed * weaponspeed;
                        lastMoveInput = moveInput;
                        // �󒆂ł̈ړ�����x�����ݒ�
                        hasAppliedAirMove++;
                        Move();
                    }




                }






            }
            else
            {
                stepTimer = 0f;
                approach = 0.4f;
                animator.SetBool("walking", false);
            }

        }
       

    



    



        //x = Input.GetAxisRaw("Horizontal") * speed;
        //z = Input.GetAxisRaw("Vertical") * speed;



        //transform.position += cam.transform.forward * z + cam.transform.right * x;

        if (rc != null) {
            if (rc.GetIsZooming()) {
                //zoomSensitivityMultiplier = 80 / Camera.main.fieldOfView;
                zoomSensitivityMultiplier = 1;
                Xsensityvity = NormalSensityvity * zoomSensitivityMultiplier;
                Ysensityvity = NormalSensityvity * zoomSensitivityMultiplier;
            } else {
                Xsensityvity = NormalSensityvity;
                Ysensityvity = NormalSensityvity;
            }
        }
    }

    public void AIWalk(Vector3 inputDirection)
    {
            float ratio = 1;

       
            stepTimer += Time.fixedDeltaTime;
            approach -= Time.fixedDeltaTime;
            // ������0.4�b���ɍĐ�
            if (stepTimer >= 0.4f)
            {
               
                stepTimer = 0f; // �^�C�}�[�����Z�b�g
                
                
            }

       

        this.speed = OriginalSpeed * ratio * servicespeed * weaponspeed;
        // �J������Y����]����ɉ�]���v�Z
        Quaternion cameraRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        // ���͂��J�����̉�]�Ɋ�Â��ĉ�]
        Vector3 moveDirection = cameraRotation * inputDirection;
        if (CanWalk(moveDirection))
        {
            if (CanWalkAnime(moveDirection))
            {


                animator.SetBool("walking", true);
                //StepClimb(moveDirection);

            }
            else
            {
                animator.SetBool("walking", false);
            }
            // �ړ�����
            //transform.position += moveDirection * speed;
            // �L�����N�^�[���ړ�
            //controller.Move(moveDirection * speed * Time.deltaTime);

            controller.Move(inputDirection * speed * Time.deltaTime);

        }
    }



    private void Move()
    {
        if (cam != null)
        {
            // �J������Y����]����ɉ�]���v�Z
            Quaternion cameraRotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);

            // ���̓x�N�g�����`�ix: ���E, z: �O��j
            Vector3 inputDirection = new Vector3(x, 0, z).normalized;

            // ���͂��J�����̉�]�Ɋ�Â��ĉ�]
            Vector3 moveDirection = cameraRotation * inputDirection;

            if (CanWalk(moveDirection))
            {
                if (CanWalkAnime(moveDirection))
                {


                    animator.SetBool("walking", true);
                    //StepClimb(moveDirection);

                }
                else
                {
                    animator.SetBool("walking", false);
                }
                // �ړ�����
                //transform.position += moveDirection * speed;
                // �L�����N�^�[���ړ�
                controller.Move(moveDirection * speed * Time.deltaTime);

            }
        }
    }

    public void Recoiled(float Ymagnitude, float Xmagnitude, float duration)
    {

        photonView.RPC("recoil", RpcTarget.Others, Ymagnitude, Xmagnitude, duration);

    }

    public void Stuned(float duration, float magnitude) {

        photonView.RPC("PunStun", RpcTarget.All, duration, magnitude);

    }
    [PunRPC]
    private void PunStun(float duration, float magnitude)
    {
        StartCoroutine(Stun(duration, magnitude));
    }

    [PunRPC]
    public IEnumerator recoil(float yRot, float xRot, float duration) {

        float xRandomRot = Random.Range(-xRot, xRot);

        for (int count = 0; count < 10; count ++)
        {
            cameraRot *= Quaternion.Euler(-yRot / (10 - count), 0, 0);
            characterRot *= Quaternion.Euler(0, xRandomRot / (10 - count), 0);

            //Update�̒��ō쐬�����֐����Ă�
            cameraRot = ClampRotation(cameraRot);

            cam.transform.localRotation = cameraRot;
            transform.localRotation = characterRot;
            yield return new WaitForSeconds(duration / 9);
        }

    }

 

    private void jump() {

        if (!WalkAble)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.W) && isTouchingWall() && !IsGrounded() && !IsJump && IsKamaitachi) {
            hasAppliedAirMove++;
            IsJump = true;
            WallKickAction();
            IsJump = false;
        } else

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !IsJump) {
            
           
            IsJump = true;

            //velocity.y += jumpPower;

            //rb.AddForce(new Vector3(0, jumpPower, 0));

            velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity); // �W�����v�̏����x

            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
            {
                sm.PlaySound("jump");
            }
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
        velocity.y = Mathf.Sqrt(jumpPower * -7f * gravity); // �W�����v�̏����x
        //rb.velocity = new Vector3(0, 0, 0);  // �c�����̑��x���Z�b�g
        //rb.AddForce(new Vector3(0, jumpPower * 2, 0));
        sm.PlaySound("phantom");
        ability.Spend(2, 1);
        }

    }

    public void Flash() {
        if (photonView == null || !photonView.IsMine) {
            return;
        }
        Throw();
    }
    public void Molesta()
    {
        if (photonView == null || !photonView.IsMine)
        {
            return;
        }
        Vector3 spawnDirection = Camera.main.transform.forward;
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.3f;
        PhotonNetwork.Instantiate("Molesta", spawnPosition, Quaternion.LookRotation(spawnDirection));
    }

    public void Arte()
    {
        if (photonView == null || !photonView.IsMine)
        {
            return;
        }
        Vector3 spawnDirection = Camera.main.transform.forward;
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.2f;
        PhotonNetwork.Instantiate("Arte", spawnPosition, Quaternion.LookRotation(spawnDirection));

    }
    public void Smoke() {
        if (photonView == null || !photonView.IsMine) {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, AbilityHitMask)) {
            PhotonNetwork.Instantiate("WhiteSmoke", hit.point, Quaternion.identity);
        }

    }

    public void KatarinaSmoke()
    {
        if (photonView == null || !photonView.IsMine)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, AbilityHitMask))
        {
            PhotonNetwork.Instantiate("KatarinaSmoke", hit.point, Quaternion.identity);
        }

    }

    public void C4()
    {
        if (photonView == null || !photonView.IsMine)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
     
        if (Physics.Raycast(ray, out hit, 8, AbilityHitMask))
        {


            abilitycheck.SetActive(true);
            abilitycheck.transform.position = hit.point;
            Vector3 abilityUnder = new Vector3(abilitycheck.transform.position.x, abilitycheck.transform.position.y+1, abilitycheck.transform.position.z); 
            if (Physics.Raycast(abilityUnder, Vector3.down, 1.5f, GroundLayer))
            {
                
                
                    // �����̃R���[�`��������Β�~
                    if (currentCoroutine != null)
                    {
                        StopCoroutine(currentCoroutine);
                    }

                    // �V�����R���[�`�����J�n���A�ێ�
                    currentCoroutine = StartCoroutine(WaitC4(hit));

                    

                
            }
        }


    }
    IEnumerator WaitC4(RaycastHit hit)
    {


        Debug.Log("������ҋ@��...");

        // ���������������܂őҋ@
        yield return new WaitUntil(() => Input.GetMouseButtonDown(3));
        currentCoroutine = null;
        abilitycheck.SetActive(false);
        PhotonNetwork.Instantiate("C4", hit.point, Quaternion.identity);
        ability.Spend(2, 1);
    }




    public void RireDuDiable() {
        if (photonView == null || !photonView.IsMine)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, AbilityHitMask))
        {
            PhotonNetwork.Instantiate("Diable", hit.point, Quaternion.identity);
        }
     

    }
    public void Coward()
    {
        abilitycheck.SetActive(false);
        if (photonView == null || !photonView.IsMine)
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Quaternion rota = transform.localRotation;
        rota *= Quaternion.Euler(0, 180, 0);

        if (Physics.Raycast(ray, out hit, cowardRange, AbilityHitMask))
        {
            abilitycheck.SetActive(true);
            abilitycheck.transform.position = hit.point;
            abilitycheck.transform.rotation = rota;
            // �����̃R���[�`��������Β�~
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            // �V�����R���[�`�����J�n���A�ێ�
            currentCoroutine = StartCoroutine(WaitCoward(hit, rota));


        }
    }
    IEnumerator WaitCoward(RaycastHit hit, Quaternion rota)
    {
        

        Debug.Log("������ҋ@��...");

        // ���������������܂őҋ@
        yield return new WaitUntil(() => Input.GetMouseButtonDown(3));
        currentCoroutine = null;
        PhotonNetwork.Instantiate("SurveillanceCamera", hit.point, rota);
        abilitycheck.SetActive(false);
        ability.Spend(2, 1);
    }




    public void Stray()
    {

        if (photonView == null || !photonView.IsMine)
        {
            return;
        }

        if (stray == null)
        {
            if (ability.number2 >= 1)
            {


                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 5, AbilityHitMask))
                {


                    abilitycheck.SetActive(true);
                    abilitycheck.transform.position = hit.point;
                    Vector3 abilityUnder = new Vector3(abilitycheck.transform.position.x, abilitycheck.transform.position.y + 1, abilitycheck.transform.position.z);
                    if (Physics.Raycast(abilityUnder, Vector3.down, 1.5f, GroundLayer))
                    {


                        // �����̃R���[�`��������Β�~
                        if (currentCoroutine != null)
                        {
                            StopCoroutine(currentCoroutine);
                        }

                        // �V�����R���[�`�����J�n���A�ێ�
                        currentCoroutine = StartCoroutine(WaitStray(hit));




                    }


                }
            }
        }
            else
        {
            sm.PlaySound("stray");
            controller.enabled = false;
            transform.position = stray.transform.position;
            transform.rotation = stray.transform.rotation;
            controller.enabled = true;
            stray.GetComponent<Stray>().PunDestroy();
            StartCoroutine(Motionless(false, false, false, 1.5f));
        }
    }
    IEnumerator WaitStray(RaycastHit hit)
    {


        Debug.Log("������ҋ@��...");

        // ���������������܂őҋ@
        yield return new WaitUntil(() => Input.GetMouseButtonDown(3));
        currentCoroutine = null;
        abilitycheck.SetActive(false);
        this.stray = PhotonNetwork.Instantiate("Stray", hit.point, Quaternion.identity);
        ability.Spend(2, 1);
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
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.3f;
        PhotonNetwork.Instantiate("Flash", spawnPosition, Quaternion.LookRotation(spawnDirection));

    }

    public void StraDarts() {

        if (photonView == null || !photonView.IsMine) {
            return;
        }
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.2f;


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

    public void Cat()
    {

        if (photonView == null || !photonView.IsMine)
        {
            return;
        }
        PinManager.pm.Cat();

    
    }

    public void Boostio() {
        if (IsGrounded() && !IsJump) {
            velocity = new Vector3(0, 0, 0);
            IsJump = true;
            sm.PlaySound("boostio");
          
            // �O���l�[�h�ɗ͂�������
            // ���������̃W�����v���x
            velocity.y = Mathf.Sqrt(jumpPower * -3f * gravity);
            // ���������̃u�[�X�g���x�i�O�����Ɉړ��j
            Vector3 boostDirection = transform.forward.normalized; // �O����
            velocity += boostDirection * 10f;
            StartCoroutine(Motionless(false, false, true, 1f));
            IsJump = false;
            ability.Spend(1, 1);


            

        }

    }

    IEnumerator Motionless(bool walk, bool shoot, bool ability, float cooltime)
    {
        WalkAble = walk;
        RayController.rc.CanShoot = shoot;
        AbilityAble = ability;
        yield return new WaitForSeconds(cooltime);
        WalkAble = true;
        RayController.rc.CanShoot = true;
        AbilityAble = true;
        yield break;
    }


    public void Boost(float y, float x)
    {
        
            velocity = new Vector3(0, 0, 0);
            IsJump = true;
            velocity.y = Mathf.Sqrt(jumpPower * -y * gravity);

            // ���������̃u�[�X�g���x�i�O�����Ɉړ��j
            Vector3 boostDirection = transform.forward.normalized; // �O����
            velocity += boostDirection * x;
            IsJump = false;
            

        

    }

    public void Kamaitachi() {

        if (photonView == null || !photonView.IsMine) {
            return;
        }

        if (IsKamaitachi)
        {
            StopKamaitachi();
        }
        else if(kamaitachi >= 0)
        {
            StartKamaitachi();
        }
            

    }

    public void StartKamaitachi()
    {
       
        IsKamaitachi = true;
        weapon = rc.UseWepon;
        rc.Knife();
        rc.Onibi();
        servicespeed = 1.5f;
    }

    public void StopKamaitachi()
    {
        RayController.rc.DestroyAbilityCheck();
        IsKamaitachi = false;
        RayController.rc.Invoke(weapon, 0);
        servicespeed = 1f;
    }


    void WallKickAction() {

        // �v���C���[�̔��Ε����ɕǃL�b�N�̗͂�������
        Vector3 kickDirection = -transform.forward * 3f;
        // ���������̃W�����v���x
        velocity.y = Mathf.Sqrt(jumpPower * -1.5f * gravity);

        velocity += kickDirection;
        //�C��
        //rb.velocity = new Vector3(0, 0, 0);  // �c�����̑��x���Z�b�g
        //rb.AddForce(kickDirection * 220, ForceMode.Impulse);

    }

    IEnumerator ChangeServiceSpeed(float speed, float duration, float jump) {
        // servicespeed��ݒ�
        servicespeed = speed;
        jumpPower = jump;

        // ~�b�ҋ@
        yield return new WaitForSeconds(duration);
        
        // servicespeed��1�ɖ߂�
        servicespeed = 1f;
        jumpPower = 0.9f;
    }


    public void UpdateCursorLock() {
        if (photonView == null || !photonView.IsMine) {
            return;
        }


    }

    //�p�x�����֐��̍쐬
    public Quaternion ClampRotation(Quaternion q) {
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

        Vector3 footPosition = foot.transform.position;
        return Physics.BoxCast(footPosition, boxSize / 2, Vector3.down, Quaternion.identity, distanceToGround, GroundLayer);
        
    }

    public bool CanWalk(Vector3 movedirection)
    {

        Vector3 wallcheckposition = WallCheck.transform.position;
        //if (animator.GetBool("crouching")) {
      //      return !Physics.Raycast(wallcheckposition, movedirection, wallDetectionDistance + 0.5f, hitMask);
       // }
        return !Physics.Raycast(wallcheckposition, movedirection, wallDetectionDistance, GroundLayer);
    }



    public bool CanWalkAnime (Vector3 movedirection)
    {

        Vector3 wallcheckposition = WallCheck.transform.position;

        return !Physics.Raycast(wallcheckposition, movedirection, wallDetectionDistance + 0.6f, GroundLayer);
    }

    public bool CheckCrouch(Vector3 movedirection)
    {

        Vector3 wallcheckposition = WallCheck.transform.position;
        return !Physics.Raycast(wallcheckposition, movedirection, wallDetectionDistance + 0.5f, GroundLayer);
        
        
    }




}
