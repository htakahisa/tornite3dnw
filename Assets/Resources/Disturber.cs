using UnityEngine;
using Photon.Pun;

public class Disturber : MonoBehaviourPun
{
    public float explosionTime = 30.0f;

    private float timer = 0f;
    private bool isPlanted = false;
    private bool hasExploded = false;


    private void Start()
    {
         
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (isPlanted && !hasExploded)
        {
            timer += Time.deltaTime;
            //Physics.Simulate(0.02f);

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
        if (RoundManager.rm.GetSide().Equals("Valkyrie"))
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
        if (RoundManager.rm.GetSide().Equals("Leviathan"))
        {

            RoundManager.rm.RoundEnd(PhotonNetwork.LocalPlayer.ActorNumber == 2);

        }
    }

  
}
