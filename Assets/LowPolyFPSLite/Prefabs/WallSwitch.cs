using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSwitch : MonoBehaviourPun
{
    [SerializeField]
    // �A�N�e�B�u���܂��͔�A�N�e�B�u���������I�u�W�F�N�g�̃��X�g
    private List<GameObject> objects = new List<GameObject>();

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

        // �Ŕ���
        if (Input.GetMouseButtonDown(3))
        {
            photonView.RPC("Close", RpcTarget.All);

        }


    }
    [PunRPC]
    private void Close()
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null) // �I�u�W�F�N�g��null�łȂ����Ƃ��m�F
            {
                obj.SetActive(true);
            }
        }
        audioSource.Play();
    }

   
}
