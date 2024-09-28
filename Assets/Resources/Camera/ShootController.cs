using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.Asteroids;

public class ShootController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject bullet1;

    [SerializeField]
    private GameObject bullet2;

    [SerializeField]
    private GameObject bullet3;

    [SerializeField]
    private GameObject bullet4;

    [SerializeField]
    private GameObject bullet5;

    private int weponId = 1;
    private BulletController bc;

    private float shotSpeed = 0.0f;
    private float shotDeltatime = 999f;


    public AudioClip noArmo;
    public AudioClip weponSe1;
    public AudioClip weponSe2;
    public AudioClip reload;
    public AudioClip changeWepon;
    private AudioSource audioSource;

    void Start()
    {
        //ComponentÇéÊìæ
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
        } else if (weponId == 3) {
            bc = bullet3.GetComponent<BulletController>();
        } else if (weponId == 4) {
            bc = bullet4.GetComponent<BulletController>();
        } else if (weponId == 5) {
            bc = bullet5.GetComponent<BulletController>();
        }
        if (bc == null)
        {
            return;
        }


        if (Input.GetKey(KeyCode.Mouse0) && Cursor.lockState == CursorLockMode.Locked) {
            if (shotDeltatime < bc.getInterval()) {
                return;
            }
            shotDeltatime = 0;

            if (!bc.canShoot()) {
                audioSource.PlayOneShot(noArmo);
                return;
            }

            if (weponId == 1) {


                shotSpeed = bc.getShotSpeed();

                for (int i = 0; i < 8; i++) {

                    GameObject bullet = PhotonNetwork.Instantiate("bullet1", transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    //GameObject bullet = (GameObject)Instantiate(bullet1, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));

                    Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

                    float spreadRatio = 0.2f;
                    bulletRb.transform.position += bulletRb.transform.forward * 1.01f;
                    bulletRb.transform.position += bulletRb.transform.right * Random.Range(-spreadRatio, spreadRatio);
                    bulletRb.transform.position += bulletRb.transform.up * Random.Range(-spreadRatio, spreadRatio);
                    bulletRb.AddForce(transform.forward * shotSpeed);

                    //éÀåÇÇ≥ÇÍÇƒÇ©ÇÁ3ïbå„Ç…èeíeÇÃÉIÉuÉWÉFÉNÉgÇîjâÛÇ∑ÇÈ.

                    Destroy(bullet, 2.0f);
                }
                audioSource.PlayOneShot(weponSe1);
                // recoil
                GetComponentInChildren<Camera>().transform.Rotate(new Vector3(-4, 0, 0), Space.Self);

            } else if (weponId == 2) {
                shotSpeed = bc.getShotSpeed();

                GameObject bullet = PhotonNetwork.Instantiate("bullet2", transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                //GameObject bullet = (GameObject)Instantiate(bullet2, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

                bulletRb.transform.position += bulletRb.transform.forward * 1.01f;
                bulletRb.AddForce(transform.forward * shotSpeed);

                //éÀåÇÇ≥ÇÍÇƒÇ©ÇÁ3ïbå„Ç…èeíeÇÃÉIÉuÉWÉFÉNÉgÇîjâÛÇ∑ÇÈ.

                Destroy(bullet, 2.0f);
                audioSource.PlayOneShot(weponSe2);
                float xrecoil;
                xrecoil = Random.Range(-1, 1);
                GetComponentInChildren<Camera>().transform.Rotate(new Vector3(-1, 0, 0), Space.Self);
                transform.RotateAround(transform.position, Vector3.up, xrecoil);

            } else if (weponId == 3) {
              shotSpeed = bc.getShotSpeed();
                GameObject bullet = PhotonNetwork.Instantiate("bullet3", transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                //GameObject bullet = (GameObject)Instantiate(bullet2, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

                bulletRb.transform.position += bulletRb.transform.forward * 1.01f;
                bulletRb.AddForce(transform.forward * shotSpeed);

                //éÀåÇÇ≥ÇÍÇƒÇ©ÇÁ3ïbå„Ç…èeíeÇÃÉIÉuÉWÉFÉNÉgÇîjâÛÇ∑ÇÈ.

                Destroy(bullet, 2.0f);
                audioSource.PlayOneShot(weponSe2);
                audioSource.PlayOneShot(weponSe1);
                float xrecoil;
                xrecoil = Random.Range(-0.7f, 0.3f);
                GetComponentInChildren<Camera>().transform.Rotate(new Vector3(-0.5f, 0, 0), Space.Self);
                transform.RotateAround(transform.position, Vector3.up, xrecoil);
            } else if (weponId == 4) {
                shotSpeed = bc.getShotSpeed();
                GameObject bullet = PhotonNetwork.Instantiate("bullet4", transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                //GameObject bullet = (GameObject)Instantiate(bullet2, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

                bulletRb.transform.position += bulletRb.transform.forward * 1.01f;
                bulletRb.AddForce(transform.forward * shotSpeed);

                //éÀåÇÇ≥ÇÍÇƒÇ©ÇÁ3ïbå„Ç…èeíeÇÃÉIÉuÉWÉFÉNÉgÇîjâÛÇ∑ÇÈ.

                Destroy(bullet, 2.0f);
                audioSource.PlayOneShot(weponSe2);
                audioSource.PlayOneShot(weponSe1);
                float xrecoil;
                GetComponentInChildren<Camera>().transform.Rotate(new Vector3(-2, 0, 0), Space.Self);
            } else if (weponId == 5) {
                shotSpeed = bc.getShotSpeed();
                GameObject bullet = PhotonNetwork.Instantiate("bullet5", transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                //GameObject bullet = (GameObject)Instantiate(bullet2, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

                bulletRb.transform.position += bulletRb.transform.forward * 1.01f;
                bulletRb.AddForce(transform.forward * shotSpeed);

                //éÀåÇÇ≥ÇÍÇƒÇ©ÇÁ3ïbå„Ç…èeíeÇÃÉIÉuÉWÉFÉNÉgÇîjâÛÇ∑ÇÈ.

                Destroy(bullet, 2.0f);
                audioSource.PlayOneShot(weponSe2);
                audioSource.PlayOneShot(weponSe1);
                float xrecoil;
                GetComponentInChildren<Camera>().transform.Rotate(new Vector3(-5, 0, 0), Space.Self);
            }

            // íeî≠éÀÉJÉEÉìÉgÇ1å∏ÇÁÇ∑
            bc.shoot();


        } else if (Input.GetKeyDown(KeyCode.R)) {
            audioSource.PlayOneShot(reload);
            bc.reload();

        } else if (Input.GetKeyDown(KeyCode.Alpha1)) {

            audioSource.PlayOneShot(changeWepon);

            weponId = 1;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            audioSource.PlayOneShot(changeWepon);
            weponId = 2;
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            audioSource.PlayOneShot(changeWepon);
            weponId = 3;
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            audioSource.PlayOneShot(changeWepon);
            weponId = 4;
        } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            audioSource.PlayOneShot(changeWepon);
            weponId = 5;
        }

    }
}
