using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

public class Arte : MonoBehaviourPun
{

    private float throwForce = 15f; // 投げる力

    Rigidbody rb;

    private SoundManager sm;

    private bool HasExplode = false;


    private GameObject arteBomb;

    [SerializeField]
    private int fragments = 20;

    public float randomForceXRange = 1f; // ランダムな力の範囲
    public float randomForceYRange = 1f; // ランダムな力の範囲

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
            // グレネードに力を加える
            Vector3 throwDirection = transform.forward * 2;
            rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
        }

     



    }

 

    // 爆発攻撃を開始するメソッド
    public void StartExplosionAttack()
    {

        for (int i = 0; i < fragments; i++)
        {
            GameObject instance = Instantiate(ResourceManager.resourcemanager.GetObject("ArteBomb"), transform.position, transform.rotation);
            Rigidbody rb = instance.GetComponent<Rigidbody>();
            // ランダムな方向の力を計算
            Vector3 randomForce = new Vector3(
                Random.Range(-randomForceXRange, randomForceXRange),
                Random.Range(0, randomForceYRange),
                Random.Range(-randomForceXRange, randomForceXRange));
            rb.AddForce(randomForce, ForceMode.Impulse);
        }

        PhotonNetwork.Destroy(gameObject);

    }





    }
