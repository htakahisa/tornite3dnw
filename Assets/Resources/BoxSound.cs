using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSound : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject != photonView.IsMine)
        {
            return;
        }
        SoundManager sm = Camera.main.transform.parent.gameObject.GetComponent<SoundManager>();
        sm.PlaySound("clapping");
        
    }

}
