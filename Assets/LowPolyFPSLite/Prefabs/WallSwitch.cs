using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSwitch : MonoBehaviourPun
{
    [SerializeField]
    private GameObject Wall;

    [SerializeField]
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        if (PhaseManager.pm.GetPhase().Equals("Buy"))
        {
            return;
        }

        // ‚Å”­“®
        if (Input.GetMouseButtonDown(3))
        {
            photonView.RPC("Close", RpcTarget.All);

        }


    }
    [PunRPC]
    private void Close()
    {
        
        Wall.SetActive(true);
        audioSource.Play();
    }
}
