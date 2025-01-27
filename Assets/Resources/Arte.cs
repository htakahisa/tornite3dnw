using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

public class Arte : MonoBehaviourPun
{

    private float throwForce = 15f; // �������

    Rigidbody rb;

    private SoundManager sm;

    private bool HasExplode = false;


    private GameObject arteBomb;

    [SerializeField]
    private int fragments = 20;

    public float randomForceXRange = 1f; // �����_���ȗ͈͂̔�
    public float randomForceYRange = 1f; // �����_���ȗ͈͂̔�

    private void OnCollisionEnter(Collision collision)
    {
      

        HasExplode = true;
        Invoke("StartExplosionAttack",0.5f);
    }


    private void Awake()
    {

        rb = gameObject.GetComponent<Rigidbody>();
        sm = Camera.main.transform.parent.GetComponent<SoundManager>();
        if (photonView.IsMine)
        {
            // �O���l�[�h�ɗ͂�������
            Vector3 throwDirection = transform.forward * 2;
            rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
        }

     



    }

 

    // �����U�����J�n���郁�\�b�h
    public void StartExplosionAttack()
    {

        for (int i = 0; i < fragments; i++)
        {
            GameObject instance = Instantiate(ResourceManager.resourcemanager.GetObject("ArteBomb"), transform.position, transform.rotation);
            Rigidbody rb = instance.GetComponent<Rigidbody>();
            // �����_���ȕ����̗͂��v�Z
            Vector3 randomForce = new Vector3(
                Random.Range(-randomForceXRange, randomForceXRange),
                Random.Range(0, randomForceYRange),
                Random.Range(-randomForceXRange, randomForceXRange));
            rb.AddForce(randomForce, ForceMode.Impulse);
        }

        PhotonNetwork.Destroy(gameObject);

    }





    }
