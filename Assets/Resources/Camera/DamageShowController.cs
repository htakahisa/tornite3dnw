using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class DamageShowController : MonoBehaviour
{

    //�@DamageUI�v���n�u
    [SerializeField]
    private GameObject damageUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Damage(Collision col)
    {
        //�@DamageUI���C���X�^���X���B�o��ʒu�͐ڐG�����R���C�_�̒��S����J�����̕����ɏ����񂹂��ʒu
        var obj = PhotonNetwork.Instantiate("DamageUi", gameObject.transform.up * 10f, Quaternion.identity);
    }
}
