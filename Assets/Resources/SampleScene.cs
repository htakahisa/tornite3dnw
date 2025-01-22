using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using ExitGames.Client.Photon;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class SampleScene : MonoBehaviourPunCallbacks {

    public int playerNo = 1;

    GameObject rmo;

    RoundManager rm;

    private void Start() {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
        Invoke("InstanceAvatar", 2f);

    }
    private void Awake() {
        
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster() {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom() {


        //InstanceAvatar();


    }

    [PunRPC]

    public void InstanceAvatar() {
        var position = new Vector3(0, 0, 0);

        rmo = GameObject.Find("Roundmanager");
        if (rmo == null)
        {
            return;
        }
        rm = rmo.GetComponent<RoundManager>();

        if (MapManager.mapmanager.GetMapName() == "Secondhouse")
        {
            if (rm.round <= 12)
            {
                // 自身のアバター（ネットワークオブジェクト）を生成する
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    Debug.Log("You are Player 1");
                    // Player 1の初期化処理
                    position = new Vector3(0f, 0f, 13);
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    Debug.Log("You are Player 2");
                    // Player 2の初期化処理
                    position = new Vector3(7f, 0f, -12f);
                }
                else
                {
                    Debug.Log("You are Player " + PhotonNetwork.LocalPlayer.ActorNumber);
                }
            }
            else
            {
                // 自身のアバター（ネットワークオブジェクト）を生成する
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    Debug.Log("You are Player 1");
                    // Player 1の初期化処理
                    position = new Vector3(7f, 0f, -12);
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    Debug.Log("You are Player 2");
                    // Player 2の初期化処理
                    position = new Vector3(0f, 0f, 13);
                }
                else
                {
                    Debug.Log("You are Player " + PhotonNetwork.LocalPlayer.ActorNumber);
                }
            }
        } else if (MapManager.mapmanager.GetMapName() == "Needless")
        {
            if (rm.round <= 12)
            {
                // 自身のアバター（ネットワークオブジェクト）を生成する
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    Debug.Log("You are Player 1");
                    // Player 1の初期化処理
                    position = new Vector3(8f, 0f, -1);
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    Debug.Log("You are Player 2");
                    // Player 2の初期化処理
                    position = new Vector3(-8f, 0f, 9.5f);
                }
                else
                {
                    Debug.Log("You are Player " + PhotonNetwork.LocalPlayer.ActorNumber);
                }
            }
            else
            {
                // 自身のアバター（ネットワークオブジェクト）を生成する
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    Debug.Log("You are Player 1");
                    // Player 1の初期化処理
                    position = new Vector3(-8f, 0f, 9.5f);
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    Debug.Log("You are Player 2");
                    // Player 2の初期化処理
                    position = new Vector3(8f, 0f, -1);
                }
                else
                {
                    Debug.Log("You are Player " + PhotonNetwork.LocalPlayer.ActorNumber);
                }
            }
        }
        else if (MapManager.mapmanager.GetMapName() == "DuelLand")
        {
            if (rm.round <= 12)
            {
                // 自身のアバター（ネットワークオブジェクト）を生成する
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    Debug.Log("You are Player 1");
                    // Player 1の初期化処理
                    position = new Vector3(0f, 0f, 5);
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    Debug.Log("You are Player 2");
                    // Player 2の初期化処理
                    position = new Vector3(0f, 0f, -5f);
                }
                else
                {
                    Debug.Log("You are Player " + PhotonNetwork.LocalPlayer.ActorNumber);
                }
            }
            else
            {
                // 自身のアバター（ネットワークオブジェクト）を生成する
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    Debug.Log("You are Player 1");
                    // Player 1の初期化処理
                    position = new Vector3(0f, 0f, -5f);
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    Debug.Log("You are Player 2");
                    // Player 2の初期化処理
                    position = new Vector3(0f, 0f, 5);
                }
                else
                {
                    Debug.Log("You are Player " + PhotonNetwork.LocalPlayer.ActorNumber);
                }
            }
        }


        GameObject avatar = PhotonNetwork.Instantiate("CowPlayer", position, Quaternion.identity);
        Camera camera = GetComponentInChildren<Camera>();
        var cameraPosition = avatar.transform.position;
        camera.transform.parent = avatar.transform;
        camera.transform.position = new Vector3(cameraPosition.x, cameraPosition.y + 1.582f, cameraPosition.z);
    }




    public int GetPlayerNo() {
        return playerNo;
    }


   


}