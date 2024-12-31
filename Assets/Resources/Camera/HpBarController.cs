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


    GameObject RoundManagerObject;


    public AudioClip damagedSe;
    private AudioSource audioSource;


    private bool death = false;

    RoundManager rm;
    AvatorController ac;


    GameObject camera;
    RayController rc;

    private float showDamageDelta = 999.0f;
    private float showDamageTime = 5.0f;
    private int damageSum = 0;
    private float textColorVisualRatio = 0; // テキストの透明度 0 は透明

   

    private void Awake() {

        PhotonNetwork.IsMessageQueueRunning = true;

    }


    // Start is called before the first frame update
    void Start() {

        hmo = GameObject.Find("HpMasterO");

        RoundManagerObject = GameObject.Find("Roundmanager");

        hmc = hmo.GetComponent<HpMaster>();

        slider = GetComponentInChildren<Slider>();

        rm = RoundManagerObject.GetComponent<RoundManager>();
        ac = gameObject.GetComponent<AvatorController>();

        camera = Camera.main.gameObject;
        rc = camera.GetComponent<RayController>();
    }

    // Update is called once per frame
    void Update() {
        //showDamageDelta += Time.deltaTime;
        //if (showDamageDelta < showDamageTime) {
        //    // フェードアウト
        //    GetComponentInChildren<Text>().color = new Color(255, 0, 0, textColorVisualRatio);
        //    textColorVisualRatio -= Time.deltaTime;
        //    //透明度が0になったら終了する。
        //    if (textColorVisualRatio < 0) {
        //        textColorVisualRatio = 0;
        //    }
        //}


        if (slider != null) {
            slider.value = hmc.GetHp(PhotonNetwork.LocalPlayer.ActorNumber);
        }

        //// Damage text は常にカメラを向く
        //GetComponentInChildren<Text>().transform.rotation = Camera.main.transform.rotation;

        //if (hmc.Dead()) {
        //    PhotonNetwork.Destroy(transform.parent.gameObject);

        //}


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
        // 透明度を 100%にする
        textColorVisualRatio = 1;


    }




    //// 他のクライアントと同期するためにOnPhotonSerializeViewを使用
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
    //    if (stream.IsWriting) {
    //        // このクライアントが体力情報を送信
    //        stream.SendNext(hp);
    //    } else {
    //        // 他のクライアントから体力情報を受信
    //        hp = (float)stream.ReceiveNext();
    //    }
    //}










}
