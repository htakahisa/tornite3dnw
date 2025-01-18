using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSynchronizer : MonoBehaviourPun
{
    public static ResultSynchronizer rs;

    // Start is called before the first frame update
    void Start()
    {
        rs = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SendResult(int winnerNumber)
    {
        photonView.RPC("SynchronizeResult", RpcTarget.All, winnerNumber);
    }
    [PunRPC]
    private void SynchronizeResult(int winnerNumber)
    {
        RoundManager.rm.RoundEnd(winnerNumber == 1);
    }
}
