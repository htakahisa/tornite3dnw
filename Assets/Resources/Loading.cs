using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Loading : MonoBehaviourPunCallbacks
{

    private bool hasload = false;

    private const byte MyMapEventCode = 1; // 任意のイベントコード

    // Start is called before the first frame update






    // Update is called once per frame
    void Update()
    {
        if (hasload)
        {
            return;
        }
        if (MapManager.mapmanager.GetMapName() == "DuelLand")
        {
            SceneManager.LoadScene(MapManager.mapmanager.GetMapName());
            hasload = true;
        }



        if (PhotonNetwork.InRoom)
        {
            if (ResourceManager.resourcemanager.HasLoadedAll)
            {
                if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
                {
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







}
