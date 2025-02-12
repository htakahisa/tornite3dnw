using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedReboot : MonoBehaviourPun
{


    private float time = 0;

    // Start is called before the first frame update
    void Awake()
    {
        Invoke("DestroyPun", 10f);
        if (!photonView.IsMine)
        {
            gameObject.layer = LayerMask.NameToLayer("Ability");
            transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Ability");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestroyPun()
    {
        PhotonNetwork.Destroy(gameObject);
    }

 

    void OnTriggerStay(Collider other)
    {


        if (other.CompareTag("Body") || other.CompareTag("Feet"))
        {
            
      
            GameObject HitCharacter = GetTopmostParent(other.gameObject);
     

            PhotonView hitphotonview = HitCharacter.GetComponent<PhotonView>();

            if (!hitphotonview.IsMine)
            {
                return;
            }
            HitCharacter.GetComponent<CameraController>().Stuned(0.05f, 0.2f);
            time += Time.deltaTime;
            if (time >= 0.5f)
            {
                
                Damage(other.gameObject);
                time = 0;
            }
        }

        

        
    }

    GameObject GetTopmostParent(GameObject obj)
    {
        Transform current = obj.transform;

        // ルートオブジェクトに到達するまで親をたどる
        while (current.parent != null)
        {
            current = current.parent;
        }

        return current.gameObject;
    }

    public void Damage(GameObject target)
    {

        DamageManager dm = new DamageManager();
        dm.causeDamage(target.transform.gameObject, 5);

    }
}
