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
    private float textColorVisualRatio = 0; // �e�L�X�g�̓����x 0 �͓���

   

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
        //    // �t�F�[�h�A�E�g
        //    GetComponentInChildren<Text>().color = new Color(255, 0, 0, textColorVisualRatio);
        //    textColorVisualRatio -= Time.deltaTime;
        //    //�����x��0�ɂȂ�����I������B
        //    if (textColorVisualRatio < 0) {
        //        textColorVisualRatio = 0;
        //    }
        //}


        if (slider != null) {
            slider.value = hmc.GetHp(PhotonNetwork.LocalPlayer.ActorNumber);
        }

        //// Damage text �͏�ɃJ����������
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
        // �����x�� 100%�ɂ���
        textColorVisualRatio = 1;


    }




    //// ���̃N���C�A���g�Ɠ������邽�߂�OnPhotonSerializeView���g�p
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
    //    if (stream.IsWriting) {
    //        // ���̃N���C�A���g���̗͏��𑗐M
    //        stream.SendNext(hp);
    //    } else {
    //        // ���̃N���C�A���g����̗͏�����M
    //        hp = (float)stream.ReceiveNext();
    //    }
    //}










}
