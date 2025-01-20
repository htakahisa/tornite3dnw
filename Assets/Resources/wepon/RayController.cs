using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RayController : MonoBehaviourPun {

    [SerializeField]
    private PinManager pinmanager;

    public LayerMask hitMask;

    private GameObject pm;
    private PhaseManager pmc;
    private RoundManager rmc;

    public float range = 200f; // Raycastの射程距離
    private float distanceToGround = 0.15f;
    private GameObject gc;
    private Classic classic;
    private JawKha jawkha;
    private Mistake mistake;
    private Stella stella;
    private Rapetter rapetter;
    private Duelist duelist;
    private ReineBlanche reine;
    private mischief mischief;
    private Noel noel;
    private Silver silver;
    private Pegasus pegasus;
    private Yor yor;
    private BlackBell blackbell;


    private int Damage = 0;
    private int HeadDamage = 0;
    private float RateDeltaTime = 0f;
    private int Magazinesize = 0;
    private string UseWepon = "";
    private bool Auto = false;
    private int MaxMagazine;
    private float ReloadTime = 0f;
    private Camera cam;
    public bool CanShoot = true;

    private bool ZoomAble = false;
    private float ZoomRatio = 0f;
    private bool IsZooming = false;

    private int duelistcounter = 0;
    private bool IsDuelist = false;

    private double deltaTimeSum = 0f;

    [SerializeField] private Text mz;
    private Text mzt;

    bool UnZoomAccuracy = false;

    public static RayController rc = null;

    private SoundManager sm = null;

    [SerializeField] private GameObject[] weapons; // 武器のプロファブを格納する配列
    private int currentWeaponIndex = 0;
    private GameObject avatar;
    private CameraController camcon;

    private float yRecoil = 0;
    private float xRecoil = 0;

    private float punch = 0f;
    private int burst = 0;
    private float burstrate = 0;

    private float RecoilService = 1;

    private float PeekingSpeed = 0;




    // Start is called before the first frame update
    void Start() {

        cam = GetComponent<Camera>();
        pm = GameObject.Find("PhaseManager");
        pmc = pm.GetComponent<PhaseManager>();
        rmc = GameObject.Find("Roundmanager").GetComponent<RoundManager>();

        // 最初の武器をアクティブにする
        SwitchWeapon(currentWeaponIndex);


        mzt = mz.GetComponent<Text>();
        gc = GameObject.Find("GunController");
        classic = new Classic();
        jawkha = new JawKha();
        mistake = new Mistake();
        silver = new Silver();
        pegasus = new Pegasus();
        stella = new Stella();
        rapetter = new Rapetter();
        mischief = new mischief();
        noel = new Noel();
        reine = new ReineBlanche();
        duelist = new Duelist();
        yor = new Yor();
        blackbell = new BlackBell();
        
    }

    private void Awake() {

        rc = this;
        Invoke("DelayGet", 3.1f);
    }

    private void DelayGet() {

        avatar = transform.parent.gameObject;
        sm = gameObject.GetComponentInParent<SoundManager>();
        camcon = avatar.GetComponent<CameraController>();
       
    }

    // Update is called once per frame
    void Update() {

      

        if (Input.GetKeyDown(KeyCode.R) && !UseWepon.Equals("")) {
            StartCoroutine(Reload());


        }



        if (ZoomAble)
        {
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(Zoom());
            }
            if (Input.GetMouseButtonUp(1))
            {
                cam.fieldOfView = 80;
                IsZooming = false;
            }
        }








        deltaTimeSum += Time.deltaTime;

        Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward, Color.red);

        if ((Auto && HullAuto()) || (!Auto && SemiAuto())) {


            Fire();

        }
        if (Input.GetMouseButtonDown(0))
        {
            if (deltaTimeSum >= burstrate)
            {
                if (burst != 0)
                {
                    StartCoroutine(Burst());
                }
            }
        }



    }


    private IEnumerator Zoom()
    {
        for (float zoom = 80 - ZoomRatio; ZoomRatio < cam.fieldOfView;)
        {
            if (!Input.GetMouseButton(1))
            {
                cam.fieldOfView = 80;
                yield break;
            }
            cam.fieldOfView -= 1;
            yield return new WaitForSeconds(PeekingSpeed / zoom);
        }
            IsZooming = true;
    }

        private void Fire()
        {
        if (Shootable())
        {
            //SoundManager.sm.PlaySound("shoot");
            deltaTimeSum = 0;
            Magazinesize -= 1;



            if (mzt != null)
            {
                mzt.text = "残弾数 " + Magazinesize;
            }

            if(UseWepon.Equals("yor"))
            {
                camcon.Boost(0.5f, -5);
            }

            GameObject target = Hit();


            if (target.CompareTag("Body") || target.CompareTag("Head"))
            {



              
                shoot(target, target.tag);
               


            }

            if (target.CompareTag("Head"))
            {
                photonView.RPC("Stuned", RpcTarget.Others, punch, punch);
            }

            if (target.CompareTag("Destructible"))
            {
                shoot(target, target.tag);
            }



                if (IsZooming)
            {
                RecoilService += 1.5f;
            }
           

            camcon.recoil(yRecoil / RecoilService, xRecoil / RecoilService);

            RecoilService = 1;

            if (ZoomRatio <= 40)
            {
                cam.fieldOfView = 80;
                IsZooming = false;
            }

        }
    }






    //↓謎のウォールチェックとかいうメソッド。いるのかわからんけど、なくても動くで。

    //protected bool WallCheck(Vector3 targetPosition, Vector3 desiredPosition, LayerMask wallLayers, out Vector3 wallHitPosition) {
    //    if (Physics.Raycast(targetPosition, desiredPosition - targetPosition, out RaycastHit wallHit, Vector3.Distance(targetPosition, desiredPosition), wallLayers, QueryTriggerInteraction.Ignore)) {
    //        wallHitPosition = wallHit.point;
    //        return true;
    //    } else {
    //        wallHitPosition = desiredPosition;
    //        return false;
    //    }
    //}
    GameObject GetTopmostParent(GameObject obj)
    {
        Transform current = obj.transform;

        // ルートオブジェクトに到達するまで親をたどる
        while (current.parent != null)
        {
            current = current.parent;
        }

        return current.gameObject;
    }


        public bool GetIsZooming() {
        return IsZooming;

    }


    IEnumerator Reload() {
        if (!CanShoot)
        {
            yield break;
        }
        Debug.Log("Reloading...");

        CanShoot = false;

        yield return new WaitForSeconds(ReloadTime);

        Magazinesize = MaxMagazine;

        if (mzt != null) {
            mzt.text = "残弾数 " + Magazinesize;
        }

        CanShoot = true;

    }

    public bool Shootable() {
        

        
            if (CanShoot) {

                    if (Magazinesize >= 1) {
                sm.PlaySound("shoot");
                return true;
                        
                    } else {
                        sm.PlaySound("noarmo");
                    }

                
            }
        
        return false;
    }

    public GameObject Hit() {
        RaycastHit hit; 
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) {
            if (camcon.IsGrounded())
            {



                if (UnZoomAccuracy || IsZooming)
                {
                   






                    if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, range, hitMask))
                    {
                        if (hit.collider.gameObject.tag == "Body" || hit.collider.gameObject.tag == "Head")
                        {
                            PhotonNetwork.Instantiate("DamageBlood", hit.point, Quaternion.identity);
                        } else
                        {
                            PhotonNetwork.Instantiate("WallHit", hit.point, Quaternion.identity);
                        }
                            return hit.collider.gameObject;

                    }





                    

                      

                    
                  
                }
            }



        }
        return gameObject;

    }

    public void Classic() {


        currentWeaponIndex = 0;
        SwitchWeapon(currentWeaponIndex);

        Damage = classic.GetDamage();
        HeadDamage = classic.GetHeadDamage();
        RateDeltaTime = classic.GetRate();
        Magazinesize = classic.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = classic.GetReloadTime();
        yRecoil = classic.GetYRecoil();
        xRecoil = 0;
        punch = 0;
        burst = 0;
        burstrate = 0;

        Auto = classic.GetAuto();
        ZoomAble = false;
        UnZoomAccuracy = classic.GetUnZoomAccuracy();


        this.UseWepon = "classic";
        if (mzt != null) {
            mzt.text = "残弾数 " + Magazinesize;
        }


    }
    public void JawKha()
    {


        currentWeaponIndex = 1;
        SwitchWeapon(currentWeaponIndex);

        Damage = jawkha.GetDamage();
        HeadDamage = jawkha.GetHeadDamage();
        RateDeltaTime = jawkha.GetRate();
        Magazinesize = jawkha.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = jawkha.GetReloadTime();
        yRecoil = jawkha.GetYRecoil();
        xRecoil = jawkha.GetXRecoil();
        punch = jawkha.GetPunch();
        burst = 0;
        burstrate = 0;
        PeekingSpeed = 0;

        Auto = jawkha.GetAuto();
        ZoomAble = jawkha.GetZoomAble();
        ZoomRatio = jawkha.GetZoomRatio();
        UnZoomAccuracy = jawkha.GetUnZoomAccuracy();

        this.UseWepon = "jawkha";
        if (mzt != null)
        {
            mzt.text = "残弾数 " + Magazinesize;
        }


    }


    public void Mistake() {

        currentWeaponIndex = 2;
        SwitchWeapon(currentWeaponIndex);

        Damage = mistake.GetDamage();
        HeadDamage = mistake.GetHeadDamage();
        RateDeltaTime = mistake.GetRate();
        Magazinesize = mistake.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = mistake.GetReloadTime();
        yRecoil = mistake.GetYRecoil();
        xRecoil = 0;
        punch = mistake.GetPunch();
        burst = 0;
        burstrate = 0;
        PeekingSpeed = 0;

        Auto = mistake.GetAuto();
        ZoomAble = false;
        UnZoomAccuracy = mistake.GetUnZoomAccuracy();

        this.UseWepon = "mistake";
        if (mzt != null) {
            mzt.text = "残弾数 " + Magazinesize;
        }


    }

    public void Silver() {

        currentWeaponIndex = 3;
        SwitchWeapon(currentWeaponIndex);

        Damage = silver.GetDamage();
        HeadDamage = silver.GetHeadDamage();
        RateDeltaTime = silver.GetRate();
        Magazinesize = silver.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = silver.GetReloadTime();
        yRecoil = silver.GetYRecoil();
        xRecoil = 0;
        punch = 0;
        burst = 0;
        burstrate = 0;
        PeekingSpeed = 0;

        Auto = silver.GetAuto();
        ZoomAble = false;
        UnZoomAccuracy = silver.GetUnZoomAccuracy();

        this.UseWepon = "silver";
        if (mzt != null) {
            mzt.text = "残弾数 " + Magazinesize;
        }


    }

    public void Pegasus() {


        currentWeaponIndex = 4;
        SwitchWeapon(currentWeaponIndex);

        Damage = pegasus.GetDamage();
        HeadDamage = pegasus.GetHeadDamage();
        RateDeltaTime = pegasus.GetRate();
        Magazinesize = pegasus.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = pegasus.GetReloadTime();
        yRecoil = pegasus.GetYRecoil();
        xRecoil = pegasus.GetXRecoil();
        punch = 0;
        burst = 0;
        burstrate = 0;
        PeekingSpeed = pegasus.GetPeekingSpeed();

        Auto = pegasus.GetAuto();
        ZoomAble = pegasus.GetZoomAble();
        ZoomRatio = pegasus.GetZoomRatio();
        UnZoomAccuracy = pegasus.GetUnZoomAccuracy();

        this.UseWepon = "pegasus";
        if (mzt != null) {
            mzt.text = "残弾数 " + Magazinesize;
        }


    }

    public void Stella() {


        currentWeaponIndex = 5;
        SwitchWeapon(currentWeaponIndex);

        Damage = stella.GetDamage();
        HeadDamage = stella.GetHeadDamage();
        RateDeltaTime = stella.GetRate();
        Magazinesize = stella.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = stella.GetReloadTime();
        yRecoil = stella.GetYRecoil();
        xRecoil = stella.GetXRecoil();
        punch = stella.GetPunch();
        burst = stella.GetBurst();
        burstrate = stella.GetBurstRate();
        PeekingSpeed = stella.GetPeekingSpeed();

        Auto = stella.GetAuto();
        ZoomAble = stella.GetZoomAble();
        ZoomRatio = stella.GetZoomRatio();
        UnZoomAccuracy = stella.GetUnZoomAccuracy();

        this.UseWepon = "stella";
        if (mzt != null) {
            mzt.text = "残弾数 " + Magazinesize;
        }


    }

    public void Rapetter()
    {


        currentWeaponIndex = 6;
        SwitchWeapon(currentWeaponIndex);

        Damage = rapetter.GetDamage();
        HeadDamage = rapetter.GetHeadDamage();
        RateDeltaTime = rapetter.GetRate();
        Magazinesize = rapetter.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = rapetter.GetReloadTime();
        yRecoil = rapetter.GetYRecoil();
        xRecoil = rapetter.GetXRecoil();
        punch = 0;
        burst = 0;
        burstrate = 0;
        PeekingSpeed = rapetter.GetPeekingSpeed();

        Auto = rapetter.GetAuto();
        ZoomAble = rapetter.GetZoomAble();
        ZoomRatio = rapetter.GetZoomRatio();
        UnZoomAccuracy = rapetter.GetUnZoomAccuracy();

        this.UseWepon = "rapetter";
        if (mzt != null)
        {
            mzt.text = "残弾数 " + Magazinesize;
        }


    }

    public void Mischief()
    {
        currentWeaponIndex = 7;
        SwitchWeapon(currentWeaponIndex);

        Damage = mischief.GetDamage();
        HeadDamage = mischief.GetHeadDamage();
        RateDeltaTime = mischief.GetRate();
        Magazinesize = mischief.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = mischief.GetReloadTime();
        yRecoil = mischief.GetYRecoil();
        xRecoil = mischief.GetXRecoil();
        punch = mischief.GetPunch();
        burst = 0;
        burstrate = 0;
        PeekingSpeed = mischief.GetPeekingSpeed();


        Auto = mischief.GetAuto();
        ZoomAble = mischief.GetZoomAble();
        ZoomRatio = mischief.GetZoomRatio();
        UnZoomAccuracy = mischief.GetUnZoomAccuracy();

        this.UseWepon = "mischief";
        if (mzt != null)
        {
            mzt.text = "残弾数 " + Magazinesize;
        }


    }

    public void Noel() {
        currentWeaponIndex = 8;
        SwitchWeapon(currentWeaponIndex);

        Damage = noel.GetDamage();
        HeadDamage = noel.GetHeadDamage();
        RateDeltaTime = noel.GetRate();
        Magazinesize = noel.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = noel.GetReloadTime();
        yRecoil = noel.GetYRecoil();
        xRecoil = noel.GetXRecoil();
        punch = 0;
        burst = 0;
        burstrate = 0;
        PeekingSpeed = noel.GetPeekingSpeed();

        Auto = noel.GetAuto();
        ZoomAble = noel.GetZoomAble();
        ZoomRatio = noel.GetZoomRatio();
        UnZoomAccuracy = noel.GetUnZoomAccuracy();

        this.UseWepon = "noel";
        if (mzt != null) {
            mzt.text = "残弾数 " + Magazinesize;
        }


    }

    public void Reine() {
        currentWeaponIndex = 9;
        SwitchWeapon(currentWeaponIndex);

        Damage = reine.GetDamage();
        HeadDamage = reine.GetHeadDamage();
        RateDeltaTime = reine.GetRate();
        Magazinesize = reine.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = reine.GetReloadTime();
        yRecoil = 0;
        xRecoil = 0;
        punch = 0;
        burst = 0;
        burstrate = 0;
        PeekingSpeed = reine.GetPeekingSpeed();

        Auto = reine.GetAuto();
        ZoomAble = reine.GetZoomAble();
        ZoomRatio = reine.GetZoomRatio();
        UnZoomAccuracy = reine.GetUnZoomAccuracy();

        this.UseWepon = "reineblanche";
        if (mzt != null) {
            mzt.text = "残弾数 " + Magazinesize;
        }


    }

    public void Duelist() {
        currentWeaponIndex = 10;
        SwitchWeapon(currentWeaponIndex);

        Damage = duelist.GetDamage();
        HeadDamage = duelist.GetHeadDamage();
        RateDeltaTime = duelist.GetRate();
        Magazinesize = duelist.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = duelist.GetReloadTime();
        yRecoil = 0;
        xRecoil = 0;
        punch = 0;
        burst = 0;
        burstrate = 0;
        PeekingSpeed = duelist.GetPeekingSpeed();

        Auto = duelist.GetAuto();
        ZoomAble = duelist.GetZoomAble();
        ZoomRatio = duelist.GetZoomRatio();
        UnZoomAccuracy = duelist.GetUnZoomAccuracy();

        this.UseWepon = "duelist";
        if (mzt != null) {
            mzt.text = "残弾数 " + Magazinesize;
        }
        if (!IsDuelist) {
            photonView.RPC("Duelistcount", RpcTarget.All);
        }
    }
    public void Yor() {
        currentWeaponIndex = 11;
        SwitchWeapon(currentWeaponIndex);

        Damage = yor.GetDamage();
        HeadDamage = yor.GetHeadDamage();
        RateDeltaTime = yor.GetRate();
        Magazinesize = yor.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = yor.GetReloadTime();
        yRecoil = yor.GetYRecoil();
        xRecoil = 0;
        punch = 0;
        burst = 0;
        burstrate = 0;
        PeekingSpeed = 0;

        Auto = yor.GetAuto();
        ZoomAble = yor.GetZoomAble();
        ZoomRatio = yor.GetZoomRatio();
        UnZoomAccuracy = yor.GetUnZoomAccuracy();

        this.UseWepon = "yor";
        if (mzt != null) {
            mzt.text = "残弾数 " + Magazinesize;
        }
      
    }
    public void BlackBell() {
        currentWeaponIndex = 12;
        SwitchWeapon(currentWeaponIndex);

        Damage = blackbell.GetDamage();
        HeadDamage = blackbell.GetHeadDamage();
        RateDeltaTime = blackbell.GetRate();
        Magazinesize = blackbell.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = blackbell.GetReloadTime();
        yRecoil = 0;
        xRecoil = 0;
        punch = 0;
        burst = 0;
        burstrate = 0;
        PeekingSpeed = blackbell.GetPeekingSpeed();

        Auto = blackbell.GetAuto();
        ZoomAble = blackbell.GetZoomAble();
        ZoomRatio = blackbell.GetZoomRatio();
        UnZoomAccuracy = blackbell.GetUnZoomAccuracy();

        this.UseWepon = "blackbell";
        if (mzt != null) {
            mzt.text = "残弾数 " + Magazinesize;
        }

    }


    [PunRPC]
    void Duelistcount() {

        duelistcounter++;
        Debug.Log("There are " + duelistcounter + "duelist.");
        if (duelistcounter >= 2) {
            rmc.IsOlive();
        }
    }







    public void shoot(GameObject enemy, string tag) {
        Debug.Log(enemy.name); // ヒットしたオブジェクトの名前をログに表示
        if (SceneManager.GetActiveScene().Equals("Practice")) {
            

        } else {
            if (tag.Equals("Destructible"))
            {
                DamageManager dm = new DamageManager();
                int damage = 0;
                damage = Damage;
                dm.damageToObj(enemy, damage);
            }
            else
            {
                DamageManager dm = new DamageManager();
                int damage = 0;
                if (tag.Equals("Head"))
                {
                    damage = HeadDamage;
                }
                if (tag.Equals("Body"))
                {
                    damage = Damage;
                }
                if (!tag.Equals("Head") && !tag.Equals("Body"))
                {
                    return;
                }
                dm.causeDamage(enemy, damage);
            }
        }
    }

    private bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround);
    }

    private bool HullAuto() {
        if (RateDeltaTime >= deltaTimeSum)
        {
            return false;
        }

        if (burst != 0 && IsZooming)
        {
           return false;
        }
        return Input.GetKey(KeyCode.Mouse0) && Cursor.lockState == CursorLockMode.Locked;

    }
    private bool SemiAuto() {
        if (RateDeltaTime >= deltaTimeSum)
        {
            return false;
        }
        return Input.GetKeyDown(KeyCode.Mouse0) && Cursor.lockState == CursorLockMode.Locked;

    }

    private IEnumerator Burst()
    {

       
            for (int i = 0; i < burst; i++)
            {
            Fire();
            yield return new WaitForSeconds(RateDeltaTime);
            }
            

    }

        void SwitchWeapon(int index) {
        for (int i = 0; i < weapons.Length; i++) {
            weapons[i].SetActive(i == index);
        }
    }


    public string getSkinName()
    {
        // 使っている武器の取得
        GameObject usedWepon = null;
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].active)
            {
                usedWepon = weapons[i];
                break;
            }
        }

        SkinManager skin = usedWepon.GetComponent<SkinManager>();
        if (skin == null)
        {
            return null;
        }
        else
        {
            return skin.getSkinName();
        }

    }



}
