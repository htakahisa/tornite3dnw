using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using ExitGames.Client.Photon;


// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class SampleScene : MonoBehaviourPunCallbacks {

    public int playerNo = 1;

    private void Start() {
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
        Invoke("InstanceAvatar", 2f);

    }
    private void Awake() {
        
    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster() {
        // "Room"�Ƃ������O�̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���ĎQ������j
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom() {


        //InstanceAvatar();


    }

    [PunRPC]

    public void InstanceAvatar() {
        var position = new Vector3(0, 0, 0);

        // ���g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1) {
            Debug.Log("You are Player 1");
            // Player 1�̏���������
            position = new Vector3(0f, 0f, 13);
        } else if (PhotonNetwork.LocalPlayer.ActorNumber == 2) {
            Debug.Log("You are Player 2");
            // Player 2�̏���������
            position = new Vector3(7f, 0.85f, -12f);
        } else {
            Debug.Log("You are Player "+ PhotonNetwork.LocalPlayer.ActorNumber);
        }


        GameObject avatar = PhotonNetwork.Instantiate("CowPlayer", position, Quaternion.identity);
        Camera camera = GetComponentInChildren<Camera>();
        var cameraPosition = avatar.transform.position;
        camera.transform.parent = avatar.transform;
        camera.transform.position = new Vector3(cameraPosition.x, cameraPosition.y + 1.7f, cameraPosition.z);
    }




    public int GetPlayerNo() {
        return playerNo;
    }


   


}