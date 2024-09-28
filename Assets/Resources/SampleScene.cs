using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class SampleScene : MonoBehaviourPunCallbacks
{

    private int playerNo = 1;

    private void Start()
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {

        var position = new Vector3(0, 0, 0);

        // 自身のアバター（ネットワークオブジェクト）を生成する
        if (playerNo == 1) {
           position = new Vector3(0, 0.8f, 12);
        } else {
           position = new Vector3(0, 0.8f, -12);
        }
        GameObject avatar = PhotonNetwork.Instantiate("Avatar", position, Quaternion.identity);


        AvatorController avatorController = avatar.GetComponent<AvatorController>();
        avatorController.setPlayerNo(playerNo);
        playerNo = playerNo + 1;



        Camera camera = GetComponentInChildren<Camera>();
        var cameraPosition = avatar.transform.position;
        camera.transform.parent = avatar.transform;       
        camera.transform.position = new Vector3(cameraPosition.x, cameraPosition.y + 1, cameraPosition.z - 0.3f);

    }
}