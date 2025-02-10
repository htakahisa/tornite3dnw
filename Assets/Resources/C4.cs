using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4 : MonoBehaviourPunCallbacks
{
    private GameObject enemy;
    private GameObject instantiatedPrefab;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Player");
        //enemy = GameObject.FindGameObjectWithTag("Me");
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            gameObject.layer = LayerMask.NameToLayer("Ability");
            transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Ability");
        }
        float distance = Vector3.Distance(transform.position, enemy.transform.position);
        //Debug.Log(distance);
        if(distance <= 2)
        {
            OnActive();
        }
    }

    private void OnActive() {
        if (!photonView.IsMine)
        {
            return;
        }
        // 生成したプレファブが存在しない場合に生成
        if (instantiatedPrefab == null)
        {
        
            Debug.Log($"{gameObject.name} が左クリックされました");
            instantiatedPrefab = PhotonNetwork.Instantiate("RedReboot", transform.position, Quaternion.identity);
            PhotonNetwork.Destroy(gameObject);

        }

        
    }


    private void OnDestroy()
    {
        if (photonView.IsMine)
        {
            Camera.main.GetComponentInParent<SoundManager>().PlayMySound("collect");
        }
    }


    void OnMouseOver()
    {

        // Fで発動
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("発動");
            OnActive();
        }

   
    }
}
