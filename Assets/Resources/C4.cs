using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4 : MonoBehaviourPunCallbacks
{

    private GameObject instantiatedPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            gameObject.layer = LayerMask.NameToLayer("Me");
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
