using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCounter : MonoBehaviour {


    private GameObject hmo;
    private HpMaster hmc;
    private CameraController cc;
    
    // Start is called before the first frame update
    void Awake() {
        hmo = GameObject.Find("HpMasterO");
        hmc = hmo.GetComponent<HpMaster>();
        cc = GetComponentInParent<CameraController>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void DamageCount(int damage, GameObject enemy) {

        PhotonView photonView = enemy.GetComponent<PhotonView>();
        Photon.Realtime.Player owner = photonView.Owner;
        if (photonView != null && owner != null) {
            // オブジェクトの所有者（Owner）のActorNumberを取得
            int ownerActorNumber = owner.ActorNumber;
            Debug.Log("The ActorNumber of the object owner is: " + ownerActorNumber);
            hmc.SetHp(damage, ownerActorNumber);
        }

    }

  

}
