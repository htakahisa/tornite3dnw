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
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            gameObject.layer = LayerMask.NameToLayer("Me");
            transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Me");
        }
        float distance = Vector3.Distance(transform.position, enemy.transform.position);
        if(distance <= 0.5)
        {
            OnActive();
        }
    }

    private void OnActive() {
        // ���������v���t�@�u�����݂��Ȃ��ꍇ�ɐ���
        if (instantiatedPrefab == null)
        {
        
            Debug.Log($"{gameObject.name} �����N���b�N����܂���");
            instantiatedPrefab = PhotonNetwork.Instantiate("RedReboot", transform.position, Quaternion.identity);
            PhotonNetwork.Destroy(gameObject);

        }

        
    }

  



    void OnMouseOver()
    {

        // F�Ŕ���
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("����");
            OnActive();
        }

        // �E�N���b�N�ɂ����
       //   if (Input.GetMouseButtonDown(1))
      //  {
      //      OnCancel();
       // }
    }
}
