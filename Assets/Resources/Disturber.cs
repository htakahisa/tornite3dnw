using UnityEngine;
using Photon.Pun;

public class Disturber : MonoBehaviourPun
{
    public float explosionTime = 30.0f;

    public double plantedTime = 0f;
    private bool isPlanted = false;
    private bool hasExploded = false;

    private TimerInRound timerinround;

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (isPlanted && !hasExploded)
        {
            double currentTime = PhotonNetwork.Time;

            if (currentTime >= plantedTime + explosionTime)
            {
                Explode();
            }
        }
    }

    public void Plant()
    {
        if (isPlanted) return;

        photonView.RPC(nameof(StartPlanting), RpcTarget.AllBuffered, PhotonNetwork.Time);
    }

    [PunRPC]
    private void StartPlanting(double networkTime)
    {
        isPlanted = true;
        plantedTime = networkTime;
        timerinround = GameObject.FindGameObjectWithTag("TimerText").GetComponent<TimerInRound>();
        timerinround.Planted(this);
    }

    private void Explode()
    {
        if (RoundManager.rm.RoundProcessing)
        {
            return;
        }

        hasExploded = true;
        photonView.RPC(nameof(OnExplode), RpcTarget.All);
    }

    public void Defuse()
    {
        photonView.RPC(nameof(OnDefuse), RpcTarget.All);
    }

    [PunRPC]
    public void OnDefuse()
    {
        if (RoundManager.rm.RoundProcessing)
        {
            return;
        }

        if (RoundManager.rm.GetSide().Equals("Leviathan"))
        {
            RoundManager.rm.RoundEnd(PhotonNetwork.LocalPlayer.ActorNumber == 1);
        }
        else if (RoundManager.rm.GetSide().Equals("Valkyrie"))
        {
            RoundManager.rm.RoundEnd(PhotonNetwork.LocalPlayer.ActorNumber == 2);
        }

        Destroy(gameObject);
    }

    [PunRPC]
    private void OnExplode()
    {
        Debug.Log("Bomb exploded!");

        if (RoundManager.rm.GetSide().Equals("Valkyrie"))
        {
            RoundManager.rm.RoundEnd(PhotonNetwork.LocalPlayer.ActorNumber == 1);
        }
        else if (RoundManager.rm.GetSide().Equals("Leviathan"))
        {
            RoundManager.rm.RoundEnd(PhotonNetwork.LocalPlayer.ActorNumber == 2);
        }
    }
}
