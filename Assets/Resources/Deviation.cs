using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deviation : MonoBehaviour
{
    private bool HadHit = false;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator CallDiable(Vector3 position, Quaternion direction)
    {
        yield return new WaitForSeconds(1f);
        PhotonNetwork.Instantiate("Diable", position, direction);
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (HadHit)
        {
            return;
        }

        PhotonView targetPhotonView = other.gameObject.GetComponentInParent<PhotonView>();

        if (targetPhotonView == null || !targetPhotonView.IsMine)
        {
            return;
        }


        if (other.CompareTag("Body") || other.CompareTag("Head"))
        {
            DamageManager dm = new DamageManager();
            dm.causeDamage(other.gameObject, 150);
            HadHit = true;
        }


    }
}
