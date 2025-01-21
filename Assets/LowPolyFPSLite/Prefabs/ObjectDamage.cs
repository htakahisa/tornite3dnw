using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDamage : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    private int Hp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageCount(int damage)
    {
        if(PhaseManager.pm.GetPhase() == "Buy")
        {
            return;
        }

        Hp -= damage;
        photonView.RPC("SynchronizeHp", RpcTarget.All, Hp);

        if(gameObject.name == "wood")
        {
            gameObject.transform.localScale += new Vector3(damage / 1000.00f, damage / 1000.00f, damage / 1000.00f);
        }
    }

    [PunRPC]
    public void SynchronizeHp(int publicHp)
    {
        this.Hp = publicHp;
        if(this.Hp <= 0)
        {
            Destroy(gameObject);
        }

    }



    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // データを送信
            stream.SendNext(Hp);
       
        }
        else
        {
            // データを受信
            Hp = (int)stream.ReceiveNext();
     
        }
    }

}
