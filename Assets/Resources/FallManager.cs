using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallManager : MonoBehaviourPun
{
    public bool HasKill = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (HasKill)
        {
            return;
        }
        if(transform.position.y <= -30)
        {
            HasKill = true;
            if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
            {
                ResultSynchronizer.rs.SendResult(1);
            }
            else
            {
                ResultSynchronizer.rs.SendResult(2);
            }
                Camera.main.transform.parent.GetComponent<CameraController>().Dead();
        }
    }
}
