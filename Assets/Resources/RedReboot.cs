using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedReboot : MonoBehaviourPun
{


    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject != photonView.IsMine)
        {
            return;
        }


        if (other.CompareTag("Body"))
        {
            
            time += Time.deltaTime;
            if (time >= 0.5)
            {
                Damage(other.gameObject);
                time = 0;
            }
        }
    }

    public void Damage(GameObject target)
    {

        DamageManager dm = new DamageManager();
        dm.causeDamage(target.transform.gameObject, 5);

    }
}
