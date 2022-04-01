using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShootController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject bullet1;

    [SerializeField]
    private GameObject bullet2;


    private int weponId = 1;
    private BulletController bc;

    private float shotSpeed = 0.0f;
    private float shotDeltatime = 999f;


    public AudioClip noArmo;
    public AudioClip weponSe1;
    public AudioClip weponSe2;
    private AudioSource audioSource;

    void Start()
    {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (photonView == null || !photonView.IsMine)
        {
            return;
        }
        shotDeltatime += Time.deltaTime;

        if (weponId == 1)
        {
            bc = bullet1.GetComponent<BulletController>();
        } else if (weponId == 2)
        {
            bc = bullet2.GetComponent<BulletController>();
        }
        if (bc == null)
        {
            return;
        }


        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (shotDeltatime < bc.getInterval())
            {
                return;
            }
            shotDeltatime = 0;

            if (!bc.canShoot())
            {
                audioSource.PlayOneShot(noArmo);
                return;
            }

            if (weponId == 1)
            {

                
                shotSpeed = bc.getShotSpeed();

                for (int i = 0; i < 8; i++)
                {

                    GameObject bullet = PhotonNetwork.Instantiate("bullet1", transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    //GameObject bullet = (GameObject)Instantiate(bullet1, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    
                    Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

                    float spreadRatio = 0.2f;
                    bulletRb.transform.position += bulletRb.transform.forward * 1.01f;
                    bulletRb.transform.position += bulletRb.transform.right * Random.Range(-spreadRatio, spreadRatio);
                    bulletRb.transform.position += bulletRb.transform.up * Random.Range(-spreadRatio, spreadRatio);
                    bulletRb.AddForce(transform.forward * shotSpeed);

                    //射撃されてから3秒後に銃弾のオブジェクトを破壊する.

                    Destroy(bullet, 3.0f);
                }
                audioSource.PlayOneShot(weponSe1);
            }
            else if (weponId == 2)
            {
                shotSpeed = bc.getShotSpeed();

                GameObject bullet = PhotonNetwork.Instantiate("bullet2", transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                //GameObject bullet = (GameObject)Instantiate(bullet2, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

                bulletRb.transform.position += bulletRb.transform.forward * 1.01f;
                bulletRb.AddForce(transform.forward * shotSpeed);

                //射撃されてから3秒後に銃弾のオブジェクトを破壊する.

                Destroy(bullet, 3.0f);
                audioSource.PlayOneShot(weponSe2);
            }

            // 弾発射カウントを1減らす
            bc.shoot();

        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            bc.reload();

        } else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weponId = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weponId = 2;
        }

    }
}
