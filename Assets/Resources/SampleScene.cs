using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class SampleScene : MonoBehaviourPunCallbacks
{

    private int playerNo = 1;

    private void Start()
    {
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        // "Room"�Ƃ������O�̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���ĎQ������j
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {

        var position = new Vector3(0, 0, 0);

        // ���g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
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