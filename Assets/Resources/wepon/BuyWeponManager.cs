using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyWeponManager : MonoBehaviourPunCallbacks {

    public int nowweponcost = 0;
    private bool having = false;


    RayController rc;

    GameObject rmo;
    RoundManager rmc;


    // Start is called before the first frame update
    void Awake() {

  
        rmo = GameObject.Find("Roundmanager");
        rmc = rmo.GetComponent<RoundManager>();
        
    }

    // Update is called once per frame
    void Update() {

    }


 


    public void Buy(int cost, string weapon)
    {
        if(rc == null)
        {
            rc = Camera.main.GetComponent<RayController>();
        }
        if (CanBuy(cost)){
        Debug.Log(weapon);    
        Pay(cost);
        rc.Invoke(weapon, 0);
        nowweponcost = cost;

        }
    }







    public void Pay(int cost) {

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


 
}
