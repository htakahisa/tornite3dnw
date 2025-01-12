using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CameraController : MonoBehaviourPunCallbacks {
    float x, z;
    float speed = 0.05f;

    private float stepTimer = 0f;
    private float wallDetectionDistance = 0.17f;

    public GameObject WallCheck;

    private Vector3 lastMoveInput;

    private float servicespeed = 1;
    public LayerMask hitMask;

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

    //変数の宣言(角度の制限用)
    float minX = -90f, maxX = 90f;

    private bool IsJump = false;

    public GameObject flash; // グレネードのプレハブ


    GameObject coward;
    private Ability ability;

    // エフェクトが生成される位置のオフセット
    public Vector3 effectOffset = new Vector3(0, 1, 2);

    // エフェクトが生成される位置のオフセット
    public Vector3 diable = new Vector3(0, 1, 2);

    private bool IsKamaitachi = false;  

    //　通常のジャンプ力

    private float jumpPower = 0.9f;
    // private Rigidbody rb;
    private float distanceToGround = 0.07f;

    RayController rc;
    private SoundManager sm;
    private bool katarina = false;

    private float katarinaInterval = 0;
    public GameObject foot;
    // public GameObject stepClimbCheck;

    private Animator animator;

    private float gravity = -16f;    // 重力の強さ

    private CharacterController controller;  // CharacterControllerコンポーネント
    private Vector3 velocity;

    public bool CanPlant = false;
    private float PlantTime = 4f;
    private float DefuseTime = 6f;
    private bool IsHalf = false;

    Disturber disturber = null;

    // Start is called before the first frame update
    void Start() {

        //rb = GetComponent<Rigidbody>();
        // ゲーム開始時にマウスを非表示にし、中央にロックする
         Cursor.visible = true;
         Cursor.lockState = CursorLockMode.None;

        ability = Camera.main.transform.parent.GetComponent<Ability>();
        sm = Camera.main.transform.parent.GetComponent<SoundManager>();
        coward = (GameObject)Resources.Load("SurveillanceCamera");

        animator = GetComponent<Animator>();
        // CharacterControllerを取得
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.L))
        {
            // ability.number2 ++;
        }

        if (katarina)
        {
            if (PhaseManager.pm.GetPhase().Equals("Battle"))
            {
                katarinaInterval += Time.deltaTime;
                if (katarinaInterval >= 0.5f)
                {
                    ability.Collect(2, 1);
                    katarinaInterval = 0f;
                }
                if (Input.GetKeyDown(KeyCode.Q) && ability.number2 >= 10)
                {
                    KatarinaSmoke();
                    ability.Spend(2, 10);
                }

                if (Input.GetKeyDown(KeyCode.F) && ability.number2 >= 30)
                {
                    C4();
                    ability.Spend(2, 30);
                }
                if (Input.GetKeyDown(KeyCode.X) && ability.number2 >= 50)
                {
                    Debug.Log("敵を検出");
                    BattleData.bd.Detect("enemy\ndetected");
                    // 敵に対する処理をここに追加
                    ScanCamera.sc.ActiveScan();
                    ability.Spend(2, 50);
                    Invoke("DestroyPun", 5f);
                }
            }
        }
        if (PhaseManager.pm != null)
        {
            if (PhaseManager.pm.GetPhase().Equals("Battle"))
            {

                    // Escapeキーが押されたときの処理
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        // マウスが表示されていない場合は表示し、自由に動かせるようにする
                        if (!Cursor.visible)
                        {
                            Cursor.visible = true;
                            Cursor.lockState = CursorLockMode.None;
                        }
                        // マウスが表示されている場合は非表示にし、中央にロックする
                        else
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                        }
                    }
                }
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
                this.rc = cam.GetComponent<RayController>();
            }

            move();
            jump();

            UpdateCursorLock();
        
    }

        private void DestroyPun()
    {
        BattleData.bd.DetectEnd();
        ScanCamera.sc.InActiveScan();
    }

    public void Dead() {
        PhotonNetwork.Destroy(gameObject);
      
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
    private IEnumerator Stun(float duration, float magnitude) {
        float elapsed = 0.0f;

        while (elapsed < duration) {

            recoil(Random.Range(-1, 2) * magnitude, magnitude);

            elapsed += Time.deltaTime;

            yield return null;
        }

        
    }



    private void FixedUpdate() {

        if (photonView == null || !photonView.IsMine) {
            return;
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            Debug.Log(RoundManager.rm.GetSide());
            if (RoundManager.rm.GetSide().Equals("Leviathan") && disturber != null)
            {
                DefuseTime -= Time.deltaTime;
                if (DefuseTime <= 3)
                {
                    IsHalf = true;
                }
            }
            else if(RoundManager.rm.GetSide().Equals("Valkyrie") && CanPlant && disturber == null)
            {
                
                PlantTime -= Time.deltaTime;
                

            }
        }
        
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            if (IsHalf)
            {
                DefuseTime = 3;
            }
            else if(!IsHalf){
                DefuseTime = 6;
            }
           
            PlantTime = 5;
        }

        if (PlantTime <= 0)
        {
            disturber = PhotonNetwork.Instantiate("Disturber", transform.position, transform.rotation).GetComponent<Disturber>();
        }

        // 地面にいる場合、垂直速度をリセット
        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2f; // 少し負の値にして地面に密着させる
            velocity.x = 0f;  // 水平方向の速度をリセット
            velocity.z = 0f;
        }

        // 重力を適用
        velocity.y += gravity * Time.deltaTime;

        // 垂直方向の移動
        controller.Move(velocity * Time.deltaTime);

        if (false)
        {
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
            crouchInterval = 0;
            }
        
            if (Input.GetKey(KeyCode.LeftShift) && (crouchInterval >= 0.5f))
            {

                animator.SetBool("crouching", true);
                // GameObjectの現在のpositionを取得
                Vector3 newPosition = Camera.main.transform.position;

                // y座標を1に変更
                newPosition.y = transform.position.y + 1;

                // 新しいpositionを適用
                Camera.main.transform.position = newPosition;



            }

            else
            {
                animator.SetBool("crouching", false);
                // GameObjectの現在のpositionを取得
                Vector3 newPosition = Camera.main.transform.position;

                // y座標を1.7に変更
                newPosition.y = transform.position.y + 1.7f;

                // 新しいpositionを適用
                Camera.main.transform.position = newPosition;

                crouchInterval += Time.deltaTime;

            }
        }

        
        RaycastHit hit;

     


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
           
            if (IsGrounded())
            {

                //// 修正後の移動方向でキャラクターを移動
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
                    ratio /= 1.5f;
                    issnake = true;
                }
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    ratio /= 3f;
                    issnake = true;
                  
                }
              
                if (Input.GetMouseButton(1))
                {
                    ratio /= 1.5f;
                }
                if (z != 0 || x != 0)
                {
                    if (!issnake)
                    {
                        stepTimer += Time.deltaTime;
                        // 足音を0.5秒毎に再生
                        if (stepTimer >= 0.5f && sm != null)
                        {
                            sm.PlaySound("walk");
                            stepTimer = 0f; // タイマーをリセット
                        }
                    }
                    //rb.velocity = new Vector3(0, rb.velocity.y, 0);  // 速度リセット
                    this.speed = 0.04f * ratio * servicespeed;
                    hasAppliedAirMove = 0;
                    lastMoveInput = Vector3.zero;

                }
            }
            else if (hasAppliedAirMove <= 1 && moveInput != Vector3.zero)
            {
                // 空中での移動処理：移動入力が変更されたときのみ実行
                if (moveInput != lastMoveInput)
                {
                    x = Input.GetAxisRaw("Horizontal") * speed;
                    z = Input.GetAxisRaw("Vertical") * speed;
                    float ratio = 0.5f;
                    bool issnake = false;
                  
                    //if (z != 0 && x != 0) {
                    //    ratio /= 2;
                    //}
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        ratio /= 1.5f;
                        issnake = true;
                    }
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        ratio /= 3f;
                        issnake = true;

                    }
                   
                   
                    if (z != 0 || x != 0)
                    {
                        if (!issnake)
                        {
                            stepTimer += Time.deltaTime;
                            // 足音を0.5秒毎に再生
                            if (stepTimer >= 0.5f)
                            {
                                sm.PlaySound("walk");
                                stepTimer = 0f; // タイマーをリセット
                            }
                        }
                        //rb.velocity = new Vector3(0, rb.velocity.y, 0);  // 速度リセット
                        this.speed = 0.04f * ratio * servicespeed;
                        lastMoveInput = moveInput;
                    }
                    //// 修正後の移動方向でキャラクターを移動
                    //rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
                    // 空中での移動を一度だけ設定
                    hasAppliedAirMove++;
                }
            }




            if (cam != null)
            {

                // カメラのY軸回転を基準に回転を計算
                Quaternion cameraRotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);

                // 入力ベクトルを定義（x: 左右, z: 前後）
                Vector3 inputDirection = new Vector3(x, 0, z).normalized;

                // 入力をカメラの回転に基づいて回転
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
                    // 移動処理
                    //transform.position += moveDirection * speed;
                    // キャラクターを移動
                    controller.Move(moveDirection * speed);

                }

            }

        }
        else
        {
            animator.SetBool("walking", false);
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


  

    [PunRPC]
    public void Stuned(float duration, float magnitude) {

        StartCoroutine(Stun(duration,magnitude));

    }

    public void recoil(float yRot, float xRot) {

        float xRandomRot = Random.Range(-xRot, xRot);

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRandomRot, 0);

        //Updateの中で作成した関数を呼ぶ
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

            //velocity.y += jumpPower;

            //rb.AddForce(new Vector3(0, jumpPower, 0));

            velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity); // ジャンプの初速度

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
        velocity.y = Mathf.Sqrt(jumpPower * -8f * gravity); // ジャンプの初速度
        //rb.velocity = new Vector3(0, 0, 0);  // 縦方向の速度リセット
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
        if (Physics.Raycast(ray, out hit, 100, AbilityHitMask))
        {
            PhotonNetwork.Instantiate("C4", hit.point, Quaternion.identity);
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
       
        if (Physics.Raycast(ray, out hit, cowardRange, AbilityHitMask)) {
          
            Instantiate(coward, hit.point, rota);
            ability.Spend(2, 1);
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

    public void Katarina()
    {

        katarina = true;

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


        // カメラの向いている方向を取得
        Vector3 spawnDirection = Camera.main.transform.forward;

        // オブジェクトを生成
        PhotonNetwork.Instantiate("Stradarts", spawnPosition, Quaternion.LookRotation(spawnDirection));
    }
    public void Eagle() {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.2f;
        if (photonView == null || !photonView.IsMine) {
            return;
        }



        // カメラの向いている方向を取得
        Vector3 spawnDirection = Camera.main.transform.forward;

        // オブジェクトを生成
        PhotonNetwork.Instantiate("Eagle_Elite", spawnPosition, Quaternion.LookRotation(spawnDirection));
    }

    public void Wolf() {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.2f;
        if (photonView == null || !photonView.IsMine) {
            return;
        }

        // カメラの位置を取得
        Vector3 cameraPosition = Camera.main.transform.position;

        // カメラの向いている方向を取得
        Vector3 spawnDirection = Camera.main.transform.forward;

        // オブジェクトを生成
        PhotonNetwork.Instantiate("Wolf", spawnPosition, Quaternion.LookRotation(spawnDirection));
    }

    public void Boostio() {
        if (IsGrounded() && !IsJump) {
            velocity = new Vector3(0, 0, 0);
            IsJump = true;
            sm.PlaySound("boostio");
            // グレネードに力を加える
            // 垂直方向のジャンプ速度
            velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity);

            // 水平方向のブースト速度（前方向に移動）
            Vector3 boostDirection = transform.forward.normalized; // 前方向
            velocity += boostDirection * 8f;
            IsJump = false;
            ability.Spend(1, 1);
            
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

        // プレイヤーの反対方向に壁キックの力を加える
        Vector3 kickDirection = -transform.forward * 5f;
        // 垂直方向のジャンプ速度
        velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity);

        velocity += kickDirection;
        //修正
        //rb.velocity = new Vector3(0, 0, 0);  // 縦方向の速度リセット
        //rb.AddForce(kickDirection * 220, ForceMode.Impulse);

    }

    IEnumerator ChangeServiceSpeed() {
        // servicespeedを1.5に設定
        servicespeed = 1.5f;
        IsKamaitachi = true;
        // 10秒待機
        yield return new WaitForSeconds(10f);

        // servicespeedを1に戻す
        servicespeed = 1.0f;
        IsKamaitachi = false;
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
        //if (photonView == null || !photonView.IsMine) {
        //    return;
        //}
        NormalSensityvity = sensi;


    }

    public bool IsGrounded() {

        Vector3 footPosition = foot.transform.position;
        return Physics.Raycast(footPosition, Vector3.down, distanceToGround, GroundLayer);
        
    }

    public bool CanWalk(Vector3 movedirection)
    {

        Vector3 wallcheckposition = WallCheck.transform.position;
        if (animator.GetBool("crouching")) {
      //      return !Physics.Raycast(wallcheckposition, movedirection, wallDetectionDistance + 0.5f, hitMask);
        }
        return !Physics.Raycast(wallcheckposition, movedirection, wallDetectionDistance, hitMask);
    }



    public bool CanWalkAnime (Vector3 movedirection)
    {

        Vector3 wallcheckposition = WallCheck.transform.position;

        return !Physics.Raycast(wallcheckposition, movedirection, wallDetectionDistance + 0.6f, hitMask);
    }

    public bool CheckCrouch(Vector3 movedirection)
    {

        Vector3 wallcheckposition = WallCheck.transform.position;
        return !Physics.Raycast(wallcheckposition, movedirection, wallDetectionDistance + 0.5f, hitMask);
        
        
    }




}
