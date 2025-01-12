using UnityEngine;
using Photon.Pun;

public class Disturber : MonoBehaviourPun, IPunObservable
{
    public float explosionTime = 30.0f;

    private float timer = 0f;
    private bool isPlanted = false;
    private bool isDefusing = false;
    private bool hasExploded = false;

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        if (isPlanted && !hasExploded)
        {
            timer += Time.deltaTime;

            if (timer >= explosionTime)
            {
                Explode();
            }
        }
    }

    public void Plant()
    {
        if (isPlanted) return;

        photonView.RPC(nameof(StartPlanting), RpcTarget.All);
    }

    [PunRPC]
    private void StartPlanting()
    {
        isPlanted = true;
        timer = 0f;
    }

    public void Defuse()
    {
        if (!isPlanted || hasExploded) return;

        photonView.RPC(nameof(StartDefusing), RpcTarget.All);
    }

    [PunRPC]
    private void StartDefusing()
    {
        isDefusing = true;
    }

    private void Explode()
    {
        hasExploded = true;
        photonView.RPC(nameof(OnExplode), RpcTarget.All);
    }

    [PunRPC]
    private void OnExplode()
    {
        Debug.Log("Bomb exploded!");
        //�T�C�h���`�F���W���Ă��Ȃ��ăA�^�b�J�[���̏ꍇ�APlayer1�̏����ł���APlayer1������Player1�̏������j������B�T�C�h���`�F���W���Ă��ăf�B�t�F���_�[���̏ꍇ�APlayer1�̏����ł���APlayer2������Player1�̏������j������B
        if (RoundManager.rm.round <= 12 == RoundManager.rm.GetSide().Equals("Valkyrie"))
        {
            RoundManager.rm.RoundEnd(true);
        } else
        {
            RoundManager.rm.RoundEnd(false);
        }
    }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isPlanted);
            stream.SendNext(isDefusing);
            stream.SendNext(timer);
        }
        else
        {
            isPlanted = (bool)stream.ReceiveNext();
            isDefusing = (bool)stream.ReceiveNext();
            timer = (float)stream.ReceiveNext();
        }
    }
}
