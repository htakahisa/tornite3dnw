using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBuyManager : MonoBehaviourPunCallbacks {

    [SerializeField] private GameObject[] ability;
    private int currentAblityIndex = 0;
    private int currentKindIndex = 1;
    public int able1cost = 0;
    public int able2cost = 0;
    private string nowability = "";


    private Ability able;
  

    GameObject rmo;
    RoundManager rmc;



    // Start is called before the first frame update
    void Awake() {


        // 最初の武器をアクティブにする
        SwitchAbility(currentAblityIndex);

        
        

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
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentAblityIndex > 0 && IsSelected()) {
            currentAblityIndex -= 1;
            SwitchAbility(currentAblityIndex);

        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentAblityIndex < ability.Length - 1 && IsSelected()) {
            currentAblityIndex += 1;
            SwitchAbility(currentAblityIndex);

        }

        if (Input.GetKeyDown(KeyCode.Return) && IsSelected()) {

           
           
            if (ability.Length != 0) {
                able = Camera.main.GetComponentInParent<Ability>();

              
                if (CanBuy(300) && IsSelected() && IsSelectedThis(0) && able.Limit(2, 1, "flash1")) {
                    Buy(300);
                    able.Buy("flash1");
                    able1cost += 300;
                    nowability = "flash";
                }
                if (CanBuy(200) && IsSelected() && IsSelectedThis(1) && able.Limit(3, 2, "whiteout2")) {
                    Buy(200);
                    able.Buy("whiteout2");
                    able2cost += 200;
                    nowability = "whiteout";
                }
                if (CanBuy(300) && IsSelected() && IsSelectedThis(2) && able.Limit(2, 2, "bluelight2")) {
                    Buy(300);
                    able.Buy("bluelight2");
                    able2cost += 300;
                    nowability = "bluelight";
                }
                if (CanBuy(500) && IsSelected() && IsSelectedThis(3) && able.Limit(5, 2, "aqua2"))
                {
                    Buy(500);
                    able.Buy("aqua2");
                    able2cost += 500;
                    nowability = "aqua";
                }
                if (CanBuy(500) && IsSelected() && IsSelectedThis(4) && able.Limit(2, 2, "updraft2")) {
                    Buy(500);
                    able.Buy("updraft2");
                    able2cost += 500;
                    nowability = "updraft";
                }
                if (CanBuy(1000) && IsSelected() && IsSelectedThis(5) && able.Limit(2, 1, "boostio1")) {
                    Buy(1000);
                    able.Buy("boostio1");
                    able1cost += 1000;
                    nowability = "boostio";
                }
                if (CanBuy(700) && IsSelected() && IsSelectedThis(6) && able.Limit(1, 1, "kamaitachi1")) {
                    Buy(700);
                    able.Buy("kamaitachi1");
                    able1cost += 700;
                    nowability = "kamaitachi";
                }
                if (CanBuy(500) && IsSelected() && IsSelectedThis(7) && able.Limit(2, 2, "stradarts2")) {
                    Buy(500);
                    able.Buy("stradarts2");
                    able1cost += 500;
                    nowability = "stradarts";
                }
                if (CanBuy(600) && IsSelected() && IsSelectedThis(8) && able.Limit(1, 2, "coward2")) {
                    Buy(600);
                    able.Buy("coward2");
                    able2cost += 600;
                    nowability = "coward";
                }
                if (CanBuy(3000) && IsSelected() && IsSelectedThis(9) && able.Limit(1, 2, "katarina2"))
                {
                    Buy(3000);
                    able.Buy("katarina2");
                    able1cost += 3000;
                    nowability = "katarina";
                }
                if (CanBuy(1300) && IsSelected() && IsSelectedThis(10) && able.Limit(1, 2, "eagle2")) {
                    Buy(1300);
                    able.Buy("eagle2");
                    able2cost += 1300;
                    nowability = "eagle";
                }
                if (CanBuy(1300) && IsSelected() && IsSelectedThis(11) && able.Limit(1, 1, "wolf1")) {
                    Buy(1300);
                    able.Buy("wolf1");
                    able1cost += 1300;
                    nowability = "wolf";
                }
                if (CanBuy(2000) && IsSelected() && IsSelectedThis(12) && able.Limit(1, 1, "yor1")) {
                    Buy(2000);
                    able.Buy("yor1");
                    able1cost += 2000;
                    nowability = "yor";
                }
                if (CanBuy(5000) && IsSelected() && IsSelectedThis(13) && able.Limit(1, 1, "blackbell1")) {
                    Buy(5000);
                    able.Buy("blackbell1");
                    able1cost += 5000;
                    nowability = "blackbell";
                }
                if (CanBuy(1500) && IsSelected() && IsSelectedThis(14) && able.Limit(1, 1, "diable1")) {
                    Buy(1500);
                    able.Buy("diable1");
                    able1cost += 1500;
                    nowability = "diable";
                }
                if (CanBuy(2000) && IsSelected() && IsSelectedThis(15) && able.Limit(1, 2, "straychild2")) {
                    Buy(2000);
                    able.Buy("straychild2");
                    able2cost += 2000;
                    nowability = "straychild";
                }
           
              








                if (IsSelected() && IsSelectedThis(16)) {
                    
                    able.Buy("cancel");
                    Return(able1cost);
                    Return(able2cost);
                    able1cost = 0;
                    able2cost = 0;
                    nowability = "";
                }


            }




        }
    }


    public bool CanBuy(int cost) {

        
        return (rmc.IsCanBuy(cost, PhotonNetwork.LocalPlayer.ActorNumber));


    }

    public bool IsSelected() {
        return currentKindIndex == 2;
    }
    public bool IsSelectedThis(int number) {
        return currentAblityIndex == number;
    }

    public void Buy(int cost) {

        

        rmc.ChangeCoin(cost * -1, PhotonNetwork.LocalPlayer.ActorNumber);
        rmc.AddOutLoad(cost, PhotonNetwork.LocalPlayer.ActorNumber);
    }

    public void Return(int cost) {

        rmc.ChangeCoin(cost, PhotonNetwork.LocalPlayer.ActorNumber);
        rmc.AddOutLoad(-1 * cost, PhotonNetwork.LocalPlayer.ActorNumber);

    }



    void SwitchAbility(int index) {

        if (ability.Length != 0) {
            for (int i = 0; i < ability.Length; i++) {
                ability[i].SetActive(i == index);
            }
        }
    }

  
}
