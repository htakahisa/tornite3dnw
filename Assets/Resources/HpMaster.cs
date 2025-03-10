using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HpMaster : MonoBehaviourPun, IPunObservable {

  

    private float hp1 = 100;
    private float hp2 = 100;

    public static HpMaster hpmaster = null;
    private float shield = 1f;

    private float aiShield = 1f;

    RoundManager rm;

    private bool HasKill = false;

    private int LastCost = 0;

    void Awake() {
        hpmaster = this;

       
    }

    // Start is called before the first frame update
    void Start() {
        ResetHp();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LastCostChange(int number)
    {
        LastCost = number;
    }

    public int GetLastCost()
    {
        return LastCost;
    }

    private void ResetHp()
    {
        hp1 = 100;
        hp2 = 100;
        rm = RoundManager.rm;
    }


    public void SetShield(float Shield)
    {
        shield = Shield;
    }

    public void SetAIShield(float Shield)
    {
        aiShield = Shield;
    }

    IEnumerator DelayAiSpawn()
    {
        yield return new WaitForSeconds(2f);
        SampleScene.ss.InstanceAi();
    }


    public void SetHp(float damage, int player) {

        
      
        if (SceneManager.GetActiveScene().name.Equals("DuelLand"))
        {
            if (player == 2)
            {
                damage *= aiShield;

                if (aiShield == 0)
                {
                    aiShield = 1;

                }

                hp2 -= damage;
                if (hp2 <= 0)
                {
                    Destroy(EnemyTag.enemytag.gameObject);
                    hp2 = 100;
                    StartCoroutine(DelayAiSpawn());
                }
            }
            else
            {
               
                damage *= shield;

                if (shield == 0)
                {
                    shield = 1;
                    armer.armermanager.ArmerNo(1);
                }

                hp1 -= damage;
                if (hp1 <= 0)
                {
                    Destroy(MyTag.mytag.gameObject);
                    SceneManager.LoadScene("DuelLand");
                }
            }
            




        }
        else
        {
            photonView.RPC("Calculate", RpcTarget.All, damage, player);
        }


    }
        [PunRPC]
    private void Calculate(float damage, int player)
    {
        if(player != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            return;
        }     

        damage *= shield;

        if (player == 1)
        {
            hp1 -= damage;
        }
        else if (player == 2)
        {
            hp2 -= damage;
        }

        if (shield == 0)
        {
            shield = 1;
            armer.armermanager.ArmerNo(1);
        }
        

        photonView.RPC("SynchronizeHp", RpcTarget.All, hp1, hp2);
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
            if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            {
                HasKill = true;
                ResultSynchronizer.rs.SendResult(2);
                Camera.main.transform.parent.GetComponent<CameraController>().Dead();
                PlayerPrefs.SetInt("Shootinglose", PlayerPrefs.GetInt("Shootinglose") + 1);
            }
            else
            {
                PlayerPrefs.SetInt("Shootingwin", PlayerPrefs.GetInt("Shootingwin") + 1);
            }
            PlayerPrefs.Save();

        }
        else if (this.hp2 <= 0)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
            {
                HasKill = true;
                ResultSynchronizer.rs.SendResult(1);
                Camera.main.transform.parent.GetComponent<CameraController>().Dead();
                PlayerPrefs.SetInt("Shootinglose", PlayerPrefs.GetInt("Shootinglose") + 1);
            }
            else
            {
                PlayerPrefs.SetInt("Shootingwin", PlayerPrefs.GetInt("Shootingwin") + 1);
            }
            PlayerPrefs.Save();
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
