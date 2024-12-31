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

    public float range = 100f; // Raycastの射程距離
    private float distanceToGround = 3f;
    private GameObject gc;
    private Classic classic;
    private Misstake sheriff;
    private Stella stella;
    private Duelist duelist;
    private ReineBlanche reine;
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
    private bool CanShoot = true;

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
        classic = gc.GetComponent<Classic>();
        sheriff = gc.GetComponent<Misstake>();
        stella = gc.GetComponent<Stella>();
        duelist = gc.GetComponent<Duelist>();
        reine = gc.GetComponent<ReineBlanche>();
        noel = gc.GetComponent<Noel>();
        silver = gc.GetComponent<Silver>();
        pegasus = gc.GetComponent<Pegasus>();
        yor = gc.GetComponent<Yor>();
        blackbell = gc.GetComponent<BlackBell>();
    }

    private void Awake() {

        rc = this;
        Invoke("DelayGet", 3.1f);
    }

    private void DelayGet() {
        sm = gameObject.GetComponentInParent<SoundManager>();
    }

    // Update is called once per frame
    void Update() {



        if (Input.GetKeyDown(KeyCode.R) && !UseWepon.Equals("")) {
            StartCoroutine(Reload());


        }



        if (ZoomAble) {
            if (Input.GetMouseButtonDown(1)) {
                cam.fieldOfView -= ZoomRatio;
                IsZooming = true;
            }
            if (Input.GetMouseButtonUp(1)) {
                cam.fieldOfView = 60;
                IsZooming = false;
            }
        }








        deltaTimeSum += Time.deltaTime;

        RaycastHit hit;
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward, Color.red);

        if ((Auto && HullAuto()) || (!Auto && SemiAuto())) {
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, range)) {


                if (Shootable()) {
                    //SoundManager.sm.PlaySound("shoot");
                    deltaTimeSum = 0;
                    Magazinesize -= 1;

                    if (mzt != null) {
                        mzt.text = "残弾数 " + Magazinesize;
                    }
                   

                        if (Hit()) {
                       


                       

                            shoot(hit.collider.gameObject, hit.collider.tag);
                        


                    }
                }
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



    public bool GetIsZooming() {
        return IsZooming;

    }


    IEnumerator Reload() {

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
        
        RaycastHit hit;
        if (RateDeltaTime <= deltaTimeSum) {
            if (CanShoot) {
                if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, range)) {
                    if (Magazinesize >= 1) {
                        if ((Auto && HullAuto()) || (!Auto && SemiAuto())) {
                            return true;
                        }
                    } else {
                        sm.PlaySound("noarmo");
                    }

                }
            }
        }
        return false;
    }

    public bool Hit() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) {
            if (IsGrounded())
            {
              


                if (UnZoomAccuracy || IsZooming)
                {
                    sm.PlaySound("shoot");

                    if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, range, hitMask))
                    {
                        if (!UnZoomAccuracy)
                        {
                            cam.fieldOfView = 80;
                            IsZooming = false;
                        }

                        //if ((hit.collider.tag == "Body" || hit.collider.tag == "Head") && hit.transform.gameObject != transform.parent.gameObject) {


                        
                        if ((hit.collider.tag == "Body" || hit.collider.tag == "Head"))
                        {
                            PhotonNetwork.Instantiate("DamageBlood", hit.point, Quaternion.identity);
                        }
                        return true;
                        //}

                    }
                    else if(Physics.Raycast(ray, out hit)) {
                        PhotonNetwork.Instantiate("WallHit", hit.point, Quaternion.identity);
                    }
                }
                else
                {
                    SoundManager.sm.PlaySound("beep");
                    return false;
                }
            }
           


        } 
        return false;
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

        Auto = classic.GetAuto();
        ZoomAble = false;
        UnZoomAccuracy = classic.GetUnZoomAccuracy();


        this.UseWepon = "classic";
        if (mzt != null) {
            mzt.text = "残弾数 " + Magazinesize;
        }


    }


    public void Misstake() {

        currentWeaponIndex = 1;
        SwitchWeapon(currentWeaponIndex);

        Damage = sheriff.GetDamage();
        HeadDamage = sheriff.GetHeadDamage();
        RateDeltaTime = sheriff.GetRate();
        Magazinesize = sheriff.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = sheriff.GetReloadTime();


        Auto = sheriff.GetAuto();
        ZoomAble = false;
        UnZoomAccuracy = sheriff.GetUnZoomAccuracy();

        this.UseWepon = "sheriff";
        if (mzt != null) {
            mzt.text = "残弾数 " + Magazinesize;
        }


    }

    public void Silver() {

        currentWeaponIndex = 2;
        SwitchWeapon(currentWeaponIndex);

        Damage = silver.GetDamage();
        HeadDamage = silver.GetHeadDamage();
        RateDeltaTime = silver.GetRate();
        Magazinesize = silver.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = silver.GetReloadTime();


        Auto = silver.GetAuto();
        ZoomAble = false;
        UnZoomAccuracy = silver.GetUnZoomAccuracy();

        this.UseWepon = "silver";
        if (mzt != null) {
            mzt.text = "残弾数 " + Magazinesize;
        }


    }

    public void Pegasus() {


        currentWeaponIndex = 3;
        SwitchWeapon(currentWeaponIndex);

        Damage = pegasus.GetDamage();
        HeadDamage = pegasus.GetHeadDamage();
        RateDeltaTime = pegasus.GetRate();
        Magazinesize = pegasus.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = pegasus.GetReloadTime();

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


        currentWeaponIndex = 4;
        SwitchWeapon(currentWeaponIndex);

        Damage = stella.GetDamage();
        HeadDamage = stella.GetHeadDamage();
        RateDeltaTime = stella.GetRate();
        Magazinesize = stella.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = stella.GetReloadTime();

        Auto = stella.GetAuto();
        ZoomAble = stella.GetZoomAble();
        ZoomRatio = stella.GetZoomRatio();
        UnZoomAccuracy = stella.GetUnZoomAccuracy();

        this.UseWepon = "stella";
        if (mzt != null) {
            mzt.text = "残弾数 " + Magazinesize;
        }


    }

    public void Noel() {
        currentWeaponIndex = 5;
        SwitchWeapon(currentWeaponIndex);

        Damage = noel.GetDamage();
        HeadDamage = noel.GetHeadDamage();
        RateDeltaTime = noel.GetRate();
        Magazinesize = noel.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = noel.GetReloadTime();

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
        currentWeaponIndex = 6;
        SwitchWeapon(currentWeaponIndex);

        Damage = reine.GetDamage();
        HeadDamage = reine.GetHeadDamage();
        RateDeltaTime = reine.GetRate();
        Magazinesize = reine.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = reine.GetReloadTime();

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
        currentWeaponIndex = 7;
        SwitchWeapon(currentWeaponIndex);

        Damage = duelist.GetDamage();
        HeadDamage = duelist.GetHeadDamage();
        RateDeltaTime = duelist.GetRate();
        Magazinesize = duelist.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = duelist.GetReloadTime();

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
        currentWeaponIndex = 8;
        SwitchWeapon(currentWeaponIndex);

        Damage = yor.GetDamage();
        HeadDamage = yor.GetHeadDamage();
        RateDeltaTime = yor.GetRate();
        Magazinesize = yor.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = yor.GetReloadTime();

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
        currentWeaponIndex = 9;
        SwitchWeapon(currentWeaponIndex);

        Damage = blackbell.GetDamage();
        HeadDamage = blackbell.GetHeadDamage();
        RateDeltaTime = blackbell.GetRate();
        Magazinesize = blackbell.GetMagazine();
        MaxMagazine = Magazinesize;
        ReloadTime = blackbell.GetReloadTime();

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
        Debug.Log(tag); // ヒットしたオブジェクトの名前をログに表示
        if (SceneManager.GetActiveScene().Equals("Practice")) {
            

        } else {
            DamageManager dm = new DamageManager();
            int damage = 0;
            if (tag.Equals("Head")) {
                damage = HeadDamage;
            } else {
                damage = Damage;
            }
            dm.causeDamage(enemy, damage);
        }
    }

    private bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround);
    }

    private bool HullAuto() {
        return Input.GetKey(KeyCode.Mouse0) && Cursor.lockState == CursorLockMode.Locked;

    }
    private bool SemiAuto() {
        return Input.GetKeyDown(KeyCode.Mouse0) && Cursor.lockState == CursorLockMode.Locked;

    }

    void SwitchWeapon(int index) {
        for (int i = 0; i < weapons.Length; i++) {
            weapons[i].SetActive(i == index);
        }
    }





}
