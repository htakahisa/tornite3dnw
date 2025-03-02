using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;


// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class SampleScene : MonoBehaviourPunCallbacks {

    public int playerNo = 1;


    RoundManager rm;

    public static SampleScene ss;

    private void Start() {

        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "jp"; // ���{���[�W�������Œ�

        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();

    }
    private void Awake()
    {
        ss = this;
        if (!SceneManager.GetActiveScene().name.Equals("Loading"))
        {
            Invoke("InstanceAvatar", 2);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (SceneManager.GetActiveScene().name.Equals("DuelLand"))
        {
            Invoke("InstanceAi", 2);

        }
    }

    public override void OnConnectedToMaster()
    {
        InRoom();
    }
    
        


    public void InRoom()
    {
        RoomOptions options = new RoomOptions { MaxPlayers = 2 }; // �ő�2�l�̃��[��
        // "Room"�Ƃ������O�̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���ĎQ������j
        PhotonNetwork.JoinOrCreateRoom(Custom.password + "," + MapManager.mapmanager.GetMapName(), options, TypedLobby.Default);
    }


    public void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected due to: " + cause);

        if (cause == DisconnectCause.ClientTimeout)
        {
            Debug.Log("Trying to reconnect...");
            PhotonNetwork.Reconnect(); // �����I�ɍĐڑ�
        }
    }

    public void InstanceAi()
    {
        SpawnPossManager spm = SpawnPossManager.spm;
        GameObject Ai = ResourceManager.resourcemanager.GetObject("Ai");
        Instantiate(Ai, spm.GetSpawnPos(), Quaternion.identity);
    }


        public void InstanceAvatar() {
        SpawnPossManager spm = SpawnPossManager.spm;
        var position = new Vector3(0, 0, 0);


        rm = RoundManager.rm;

        if (MapManager.mapmanager.GetMapName() == "Secondhouse")
        {
            if (rm.round <= 12)
            {
                // ���g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    Debug.Log("You are Player 1");
                    // Player 1�̏���������
                    position = new Vector3(0f, 0f, 13);
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    Debug.Log("You are Player 2");
                    // Player 2�̏���������
                    position = new Vector3(7f, 0f, -12f);
                }
                else
                {
                    Debug.Log("You are Player " + PhotonNetwork.LocalPlayer.ActorNumber);
                }
            }
            else
            {
                // ���g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    Debug.Log("You are Player 1");
                    // Player 1�̏���������
                    position = new Vector3(7f, 0f, -12);
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    Debug.Log("You are Player 2");
                    // Player 2�̏���������
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
                // ���g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    Debug.Log("You are Player 1");
                    // Player 1�̏���������
                    position = new Vector3(8f, 0f, -1);
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    Debug.Log("You are Player 2");
                    // Player 2�̏���������
                    position = new Vector3(-8f, 0f, 9.5f);
                }
                else
                {
                    Debug.Log("You are Player " + PhotonNetwork.LocalPlayer.ActorNumber);
                }
            }
            else
            {
                // ���g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    Debug.Log("You are Player 1");
                    // Player 1�̏���������
                    position = new Vector3(-8f, 0f, 9.5f);
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    Debug.Log("You are Player 2");
                    // Player 2�̏���������
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
                // ���g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    Debug.Log("You are Player 1");
                    // Player 1�̏���������
                    position = spm.GetSpawnPos();
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    Debug.Log("You are Player 2");
                    // Player 2�̏���������
                    position = spm.GetSpawnPos();
                }
                else
                {
                    Debug.Log("You are Player " + PhotonNetwork.LocalPlayer.ActorNumber);
                }
            }
            else
            {
                // ���g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    Debug.Log("You are Player 1");
                    // Player 1�̏���������
                    position = spm.GetSpawnPos();
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    Debug.Log("You are Player 2");
                    // Player 2�̏���������
                    position = spm.GetSpawnPos();
                }
                else
                {
                    Debug.Log("You are Player " + PhotonNetwork.LocalPlayer.ActorNumber);
                }
            }
        }
        else if (MapManager.mapmanager.GetMapName() == "Whisper")
        {
            if (rm.round <= 12)
            {
                // ���g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    Debug.Log("You are Player 1");
                    // Player 1�̏���������
                    position = new Vector3(-5.871117f, -3.515372f, -15.65266f);
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    Debug.Log("You are Player 2");
                    // Player 2�̏���������
                    position = new Vector3(-3.7f, -2.428032f, 16f);
                }
                else
                {
                    Debug.Log("You are Player " + PhotonNetwork.LocalPlayer.ActorNumber);
                }
            }
            else
            {
                // ���g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    Debug.Log("You are Player 1");
                    // Player 1�̏���������
                    position = new Vector3(-3.7f, -2.428032f, 16f);
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    Debug.Log("You are Player 2");
                    // Player 2�̏���������
                    position = new Vector3(-5.871117f, -3.515372f, -15.65266f);
                }
                else
                {
                    Debug.Log("You are Player " + PhotonNetwork.LocalPlayer.ActorNumber);
                }
            }
        }
        else if (MapManager.mapmanager.GetMapName() == "Mementomori")
        {
            if (rm.round <= 12)
            {
                // ���g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    Debug.Log("You are Player 1");
                    // Player 1�̏���������
                    position = new Vector3(0.5927622f, -0.8114262f, 13.65365f);
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    Debug.Log("You are Player 2");
                    // Player 2�̏���������
                    position = new Vector3(-3.166074f, -0.03999949f, 48.20417f);
                }
                else
                {
                    Debug.Log("You are Player " + PhotonNetwork.LocalPlayer.ActorNumber);
                }
            }
            else
            {
                // ���g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    Debug.Log("You are Player 1");
                    // Player 1�̏���������
                    position = new Vector3(-3.166074f, -0.03999949f, 48.20417f);
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    Debug.Log("You are Player 2");
                    // Player 2�̏���������
                    position = new Vector3(0.5927622f, -0.8114262f, 13.65365f);
                }
                else
                {
                    Debug.Log("You are Player " + PhotonNetwork.LocalPlayer.ActorNumber);
                }
            }
        }


        GameObject avatar = PhotonNetwork.Instantiate("CowPlayer", position, Quaternion.identity);
        Camera camera = Camera.main;
        var cameraPosition = avatar.transform.position;
        camera.transform.parent = avatar.transform;
        camera.transform.position = new Vector3(cameraPosition.x, cameraPosition.y + 1.582f, cameraPosition.z);
    }




    public int GetPlayerNo() {
        return playerNo;
    }


   


}