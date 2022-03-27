using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    public float shotSpeed;
    public int shotCount = 30;


    private float shotInterval = 0.1f;
    private float shotDeltatime = 999f;

    void Update()
    {

        shotDeltatime += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (shotDeltatime < shotInterval)
            {
                return;
            }
            shotDeltatime = 0;

            shotCount -= 1;

            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(transform.forward * shotSpeed);

            //射撃されてから3秒後に銃弾のオブジェクトを破壊する.

            Destroy(bullet, 3.0f);
            

        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            shotCount = 30;

        }

    }
}
