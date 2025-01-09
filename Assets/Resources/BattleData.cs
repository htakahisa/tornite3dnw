using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleData : MonoBehaviourPun
{
    Text battletext;
    public static BattleData bd;


    // Start is called before the first frame update
    void Start()
    {
        bd = this;
        battletext = gameObject.GetComponent<Text>();
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Detect(string text)
    {
        battletext.text = text;
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            photonView.RPC("Detected", RpcTarget.Others, 2);
        }
        else
        {
            photonView.RPC("Detected", RpcTarget.Others, 1);
        }
    }

    public void DetectedEnd()
    {
        photonView.RPC("DetectEnd", RpcTarget.All);
    }

    [PunRPC]
    public void DetectEnd()
    {
        battletext.text = "";

    }

    [PunRPC]
    public void Detected(int detectedPlayer)
    {
        if(detectedPlayer == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            battletext.text = "detected\nby the\nenemy.";
        }
    }
}
