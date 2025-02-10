using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class Loading : MonoBehaviourPun
{
    private string MyMap = "Needless";
    private string OpponentMap = "";
    private bool hasload = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasload)
        {
            return;
        }


        if (PhotonNetwork.InRoom)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                MyMap = MapManager.mapmanager.GetMapName();
                photonView.RPC("GetOpponentMapName", RpcTarget.Others, MyMap);

                if (MyMap == OpponentMap)
                {
                    if (!ResourceManager.resourcemanager.HasLoadedAll)
                    {
                        return;
                    }
                    SceneManager.LoadScene(MapManager.mapmanager.GetMapName());
                    hasload = true;
                }

            }
        }
#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(MapManager.mapmanager.GetMapName());
        }
#endif


    }


    [PunRPC]
    private void GetOpponentMapName(string name)
    {
        OpponentMap = name;
    }

}
