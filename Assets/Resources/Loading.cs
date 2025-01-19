using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class Loading : MonoBehaviourPun
{
    private string MyMap = "";
    private string OpponentMap = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                MyMap = MapManager.mapmanager.GetMapName();
                photonView.RPC("GetOpponentMapName", RpcTarget.Others, MyMap);

                if (MyMap == OpponentMap)
                {
                    SceneManager.LoadScene(MapManager.mapmanager.GetMapName());
                }

            }
        }

            
    }

    [PunRPC]
    private void GetOpponentMapName(string name)
    {
        OpponentMap = name;
    }

}
