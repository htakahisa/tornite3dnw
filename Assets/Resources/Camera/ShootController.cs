using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShootController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject bulletPrefab;
    public float shotSpeed;
    public int shotCount = 30;


    private float shotInterval = 0.1f;
    private float shotDeltatime = 999f;

    void Update()
    {

        if (photonView == null || !photonView.IsMine)
        {
            return;
        }
        shotDeltatime += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (shotDeltatime < shotInterval)
            {
                return;
            }
            shotDeltatime = 0;

            if (shotCount <= 0)
            {
                return;
            }
            shotCount -= 1;

            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            bulletRb.transform.position += bulletRb.transform.forward * 1.01f;
            bulletRb.AddForce(transform.forward * shotSpeed);

            //éÀåÇÇ≥ÇÍÇƒÇ©ÇÁ3ïbå„Ç…èeíeÇÃÉIÉuÉWÉFÉNÉgÇîjâÛÇ∑ÇÈ.

            Destroy(bullet, 3.0f);
            

        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            shotCount = 30;

        }

    }
}
