using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyWeponManager : MonoBehaviourPunCallbacks {

    [SerializeField] private GameObject[] weapons;
    private int currentWeaponIndex = 0;
    private int currentKindIndex = 1;
    public int nowweponcost = 0;
    private bool having = false;

    

    RayController rc;

    GameObject rmo;
    RoundManager rmc;

    GameObject avatar;
    AvatorController ac = null;

    // Start is called before the first frame update
    void Awake() {


        // 最初の武器をアクティブにする
        SwitchWeapon(currentWeaponIndex);

       


        rmo = GameObject.Find("Roundmanager");
        rmc = rmo.GetComponent<RoundManager>();

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.DownArrow) && currentKindIndex < 2) {
            currentKindIndex += 1;

        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentKindIndex > 1) {
            currentKindIndex -= 1;

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentWeaponIndex > 0 && IsSelected()) {
            currentWeaponIndex -= 1;
            SwitchWeapon(currentWeaponIndex);

        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentWeaponIndex < weapons.Length - 1 && IsSelected()) {
            currentWeaponIndex += 1;
            SwitchWeapon(currentWeaponIndex);

        }

        if (Input.GetKeyDown(KeyCode.Return) && IsSelected()) {

            rc = Camera.main.GetComponent<RayController>();
            avatar = transform.root.gameObject;
            ac = avatar.GetComponent<AvatorController>();
            if (weapons.Length != 0) {

                if (CanBuy(0) && IsSelected() && IsSelectedThis(0)) {
                    Buy(0);
                    rc.Classic();
                    nowweponcost = 0;
                }
                if (CanBuy(300) && IsSelected() && IsSelectedThis(1))
                {
                    Buy(300);
                    rc.JawKha();
                    nowweponcost = 300;

                }

                if (CanBuy(700) && IsSelected() && IsSelectedThis(2)) {
                        Buy(700);
                        rc.Misstake();
                        nowweponcost = 700;

                    }
                    if (CanBuy(1500) && IsSelected() && IsSelectedThis(3)) {
                        Buy(1500);
                        rc.Silver();
                        nowweponcost = 1500;

                    }
                    if (CanBuy(1100) && IsSelected() && IsSelectedThis(4)) {
                        Buy(1100);
                        rc.Pegasus();
                        nowweponcost = 1100;

                    }
                    if (CanBuy(1900) && IsSelected() && IsSelectedThis(5)) {
                        Buy(1900);
                        rc.Stella();
                        nowweponcost = 1900;

                    }
                    if (CanBuy(2400) && IsSelected() && IsSelectedThis(6)) {
                        Buy(2400);
                        rc.Noel();
                        nowweponcost = 2400;

                    }
                    if (CanBuy(2000) && IsSelected() && IsSelectedThis(7)) {
                        Buy(2000);
                        rc.Reine();
                        nowweponcost = 2000;

                    }
                    if (CanBuy(4000) && IsSelected() && IsSelectedThis(8)) {
                        Buy(4000);
                        rc.Duelist();
                        nowweponcost = 4000;

                    }
                
            }




        }
    }

    public void Buy(int cost) {

        if (having) {
            Back(nowweponcost);
        }
        having = true;
            rmc.ChangeCoin(cost * -1, PhotonNetwork.LocalPlayer.ActorNumber);
        rmc.AddOutLoad(cost, PhotonNetwork.LocalPlayer.ActorNumber);
    }

    public void Back(int back) {


        rmc.ChangeCoin(back, PhotonNetwork.LocalPlayer.ActorNumber);
        rmc.AddOutLoad(-1 * back, PhotonNetwork.LocalPlayer.ActorNumber);
    }

    public bool CanBuy(int cost) {

        return (rmc.IsCanBuy(cost - nowweponcost, PhotonNetwork.LocalPlayer.ActorNumber));


    }

    public bool IsSelected() {
        return currentKindIndex == 1;
    }
    public bool IsSelectedThis(int number) {
        return currentWeaponIndex == number;
    }

    void SwitchWeapon(int index) {

        if (weapons.Length != 0) {
            for (int i = 0; i < weapons.Length; i++) {
                weapons[i].SetActive(i == index);
            }
        }
    }
}
