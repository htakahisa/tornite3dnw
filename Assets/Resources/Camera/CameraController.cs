using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CameraController : MonoBehaviourPunCallbacks {
    float x, z;
    float speed = 0.05f;

    private float stepTimer = 0f;
    private float wallDetectionDistance = 0.05f;

    public GameObject WallCheck;

    private Vector3 lastMoveInput;

    private float servicespeed = 1;


    private int hasAppliedAirMove = 0;

    private GameObject cam;
    Quaternion cameraRot, characterRot;
    static float NormalSensityvity = 1f;
    float Xsensityvity = 1f, Ysensityvity = 1f;

    private float zoomSensitivityMultiplier = 1f;


    private float cowardRange = 10f;

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
    [SerializeField]
    private float jumpPower = 20000;
    private Rigidbody rb;
    private float distanceToGround = 0.03f;

    RayController rc;
    private SoundManager sm;
    private bool katarina = false;

    private float katarinaInterval = 0;
    public GameObject foot;

    // Start is called before the first frame update
    void Start() {

        rb = GetComponent<Rigidbody>();
        // ゲーム開始時にマウスを非表示にし、中央にロックする
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        ability = Camera.main.transform.parent.GetComponent<Ability>();
        sm = Camera.main.transform.parent.GetComponent<SoundManager>();
        coward = (GameObject)Resources.Load("SurveillanceCamera");

    }

    // Update is called once per frame
    void Update() {


        if (Input.GetKeyDown(KeyCode.L)) {
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
                }
            }
        }


            // Escapeキーが押されたときの処理
            if (Input.GetKeyDown(KeyCode.Escape)) {
            // マウスが表示されていない場合は表示し、自由に動かせるようにする
            if (!Cursor.visible) {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            // マウスが表示されている場合は非表示にし、中央にロックする
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

                //// 修正後の移動方向でキャラクターを移動
                //rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
                x = Input.GetAxisRaw("Horizontal") * speed;
                z = Input.GetAxisRaw("Vertical") * speed;
                float ratio = 1f;
                bool issnake = false;
                if (z != 0 && x != 0) {
                    ratio /= 2;
                }
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftControl)) {
                    ratio /= 1.5f;
                    issnake = true;
                }
                if (Input.GetMouseButton(1))
                {
                    ratio /= 1.5f;
                }
                if (z != 0 || x != 0) {
                    if (!issnake) {
                        stepTimer += Time.deltaTime;
                        // 足音を0.5秒毎に再生
                        if (stepTimer >= 0.5f && sm != null) {
                            sm.PlaySound("walk");
                            stepTimer = 0f; // タイマーをリセット
                        }
                    }
                    //rb.velocity = new Vector3(0, rb.velocity.y, 0);  // 速度リセット
                    this.speed = 0.04f * ratio * servicespeed;
                    hasAppliedAirMove = 0;
                    lastMoveInput = Vector3.zero;

                }
            } else if (hasAppliedAirMove <= 1 && moveInput != Vector3.zero) {
                // 空中での移動処理：移動入力が変更されたときのみ実行
                if (moveInput != lastMoveInput) {
                    x = Input.GetAxisRaw("Horizontal") * speed;
                    z = Input.GetAxisRaw("Vertical") * speed;
                    float ratio = 0.5f;
                    bool issnake = false;
                    //if (z != 0 && x != 0) {
                    //    ratio /= 2;
                    //}
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftControl)) {
                        ratio /= 1.5f;
                        issnake = true;
                    }
                    if (z != 0 || x != 0) {
                        if (!issnake) {
                            stepTimer += Time.deltaTime;
                            // 足音を0.5秒毎に再生
                            if (stepTimer >= 0.5f) {
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

            

          
            if (cam != null) {

                // カメラのY軸回転を基準に回転を計算
                Quaternion cameraRotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);

                // 入力ベクトルを定義（x: 左右, z: 前後）
                Vector3 inputDirection = new Vector3(x, 0, z).normalized;

                // 入力をカメラの回転に基づいて回転
                Vector3 moveDirection = cameraRotation * inputDirection;

                if (CanWalk(moveDirection))
                {
                    // 移動処理
                    transform.position += moveDirection * speed;

                }
                
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


  


    public void Stuned(float duration, float magnitude) {

        StartCoroutine(Stun(duration,magnitude));

    }

    private void recoil(float yRot, float xRot) {

     

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

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
            ////　走って移動している時はジャンプ力を上げる
            //if (runFlag && velocity.magnitude > 0f) {
            //    velocity.y += dashJumpPower;
            //} else {
            //    velocity.y += jumpPower;
            //}
            //velocity.y += jumpPower;
            rb.AddForce(new Vector3(0, jumpPower, 0));
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
        rb.velocity = new Vector3(0, 0, 0);  // 縦方向の速度リセット
        rb.AddForce(new Vector3(0, jumpPower * 2, 0));
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
        if (Physics.Raycast(ray, out hit)) {
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
        if (Physics.Raycast(ray, out hit))
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
        if (Physics.Raycast(ray, out hit))
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
       
        if (Physics.Raycast(ray, out hit, cowardRange)) {
          
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

        if (photonView == null || !photonView.IsMine)
        {
            return;
        }
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

        // カメラの位置を取得
        Vector3 cameraPosition = Camera.main.transform.position;

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

        // カメラの位置を取得
        Vector3 cameraPosition = Camera.main.transform.position;

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
            rb.velocity = new Vector3(0, 0, 0);  // 速度リセット
            IsJump = true;
            sm.PlaySound("boostio");
            // グレネードに力を加える
            Vector3 throwDirection = transform.forward*1.5f + transform.up;
            rb.AddForce(throwDirection * 4, ForceMode.VelocityChange);
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
        Vector3 kickDirection = -transform.forward*1.5f + Vector3.up*1.2f;
        rb.velocity = new Vector3(0, 0, 0);  // 縦方向の速度リセット
        rb.AddForce(kickDirection * 220, ForceMode.Impulse);

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
        return Physics.Raycast(footPosition, Vector3.down, distanceToGround);
        
    }

    public bool CanWalk(Vector3 movedirection)
    {

        Vector3 wallcheckposition = WallCheck.transform.position;

        return !Physics.Raycast(wallcheckposition, movedirection, wallDetectionDistance);
    }





 

}
