using UnityEngine;
using Photon.Pun;

public class Disturber : MonoBehaviourPun
{
    public float explosionTime = 40.0f;

    public double plantedTime = 0f;
    private bool isPlanted = false;
    private bool hasExploded = false;

    private TimerInRound timerinround;

    [SerializeField]
    private GameObject explodedPref;

    [SerializeField]
    private AudioClip startSe;
    [SerializeField]
    private AudioClip explodeSe;

    private AudioSource audioSource;

    public static Disturber disturber;

    private void Awake()
    {
        disturber = this;
    }


    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = startSe;
        audioSource.volume = 0.3f;
        audioSource.Play();
    }

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
        timerinround = TimerInRound.TIR;
        timerinround.Planted(this);
    }


    private void Explode()
    {


        hasExploded = true;
        if (RoundManager.rm.RoundProcessing)
        {
            return;
        }
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
            ResultSynchronizer.rs.SendResult(PhotonNetwork.LocalPlayer.ActorNumber);
        }
      

        Destroy(gameObject);
    }

    [PunRPC]
    private void OnExplode()
    {
        Debug.Log("Bomb exploded!");


        // 爆発エフェクト
        Instantiate(explodedPref, transform.position, transform.rotation);
        audioSource.Stop();
        audioSource.clip = explodeSe;
        audioSource.Play();

        if (RoundManager.rm.GetSide().Equals("Valkyrie"))
        {
            ResultSynchronizer.rs.SendResult(PhotonNetwork.LocalPlayer.ActorNumber);
        }
       
    }
}
