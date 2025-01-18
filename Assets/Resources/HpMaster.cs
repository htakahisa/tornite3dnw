using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpMaster : MonoBehaviourPun, IPunObservable {

  

    private float hp1 = 100;
    private float hp2 = 100;

    public static HpMaster hpmaster = null;

    private float shield = 1f;
    
    GameObject rmo;

    RoundManager rm;

    private bool HasKill = false;

    void Awake() {
        hpmaster = this;
 
        Invoke("SetArmer", 0.01f);
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
    
    }

    private void SetArmer() {
        rmo = GameObject.Find("Roundmanager");
        rm = rmo.GetComponent<RoundManager>();
        hp1 = 100;
        hp2 = 100;
        photonView.RPC("SynchronizeHp", RpcTarget.All, hp1, hp2);
        if (rm.sideRound < 2) {
            this.shield = 1;
            armer.armermanager.ArmerNo(1);
        } else if (rm.sideRound < 4) {
            this.shield = 0.8f;
            armer.armermanager.ArmerNo(2);
        } else if (rm.sideRound > 3)
        {
            this.shield = 0.67f;
            armer.armermanager.ArmerNo(3);
        }
        if (rm.streak == 1) {
             if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
             {
                this.shield = 0.67f;
                armer.armermanager.ArmerNo(4);
                rm.streak = 0;
            }
        }
        if (rm.streak == 2)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            {
                this.shield = 0.67f;
                armer.armermanager.ArmerNo(4);
                rm.streak = 0;
            }
        }
        
    }


    
    public void SetHp(float damage, int player) {

        damage *= shield;

        if (player == 1) {
            hp1 -= damage;
        } else if (player == 2) {
            hp2 -= damage;
        }



        photonView.RPC("SynchronizeHp", RpcTarget.All, hp1, hp2);





        //Debug.Log(damage);
        //rmo = GameObject.Find("Roundmanager");

        //rm = rmo.GetComponent<RoundManager>();



    }


    [PunRPC]
    public void SynchronizeHp(float hp1, float hp2) {

        if (rm == null) {
            return;
        }

        this.hp1 = hp1;
        this.hp2 = hp2;

        if (HasKill)
        {
            return;
        }
        if (rm.RoundProcessing)
        {
            return;
        }
            if (this.hp1 <= 0)
            {
                HasKill = true;
                rm.RoundEnd(false);
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    Camera.main.transform.parent.GetComponent<CameraController>().Dead();
                }
                
            }
            else if (this.hp2 <= 0)
            {
                HasKill = true;
                rm.RoundEnd(true);
                if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    Camera.main.transform.parent.GetComponent<CameraController>().Dead();
                }
                
            }
        


    }




        public float GetHp(int player) {
        if (player == 1) {
            return hp1;
        } else if (player == 2) {
            return hp2;
        }

        return 0;
    }

    public bool Dead() {

        if (hp1 <= 0 || hp2 <= 0) {
            return true;
        }
        return false;
    }




    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
    //    if (PhotonNetwork.LocalPlayer.ActorNumber == 1) {
    //        // データを送信
    //        hp2 = (int)stream.ReceiveNext();
    //        stream.SendNext(hp1);
    //    } else {
    //        // データを受信
    //        hp1 = (int)stream.ReceiveNext();
    //        stream.SendNext(hp2);
    //    }
    //}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            // データを送信
            stream.SendNext(hp1);
            stream.SendNext(hp2);
            stream.SendNext(HasKill);
        } else {
            // データを受信
            hp1 = (float)stream.ReceiveNext();
            hp2 = (float)stream.ReceiveNext();
            HasKill = (bool)stream.ReceiveNext();
        }
    }
}
