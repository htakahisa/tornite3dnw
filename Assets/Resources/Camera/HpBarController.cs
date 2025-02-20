using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class HpBarController : MonoBehaviourPun {
    private Slider slider;

    

    //[SerializeField]
    private GameObject hmo;
    private HpMaster hmc;


    GameObject camera;

    private float showDamageDelta = 999.0f;
    private float showDamageTime = 5.0f;
    private int damageSum = 0;

   

    private void Awake() {

        PhotonNetwork.IsMessageQueueRunning = true;

    }


    // Start is called before the first frame update
    void Start() {

        hmc = HpMaster.hpmaster;

        slider = GetComponent<Slider>();

        camera = Camera.main.gameObject;

    }

    // Update is called once per frame
    void Update() {


        if (slider != null) {
            slider.value = hmc.GetHp(PhotonNetwork.LocalPlayer.ActorNumber);
        }


    }





    [PunRPC]
    void DisplayHP(string damage) {
        // receive the synced value if needed 

        if (showDamageDelta < showDamageTime / 2) {
            damageSum += Int32.Parse(damage);
        } else {
            damageSum = Int32.Parse(damage);
        }

        GetComponentInChildren<Text>().text = damageSum.ToString();
        showDamageDelta = 0;
        // “§–¾“x‚ð 100%‚É‚·‚é


    }












}
