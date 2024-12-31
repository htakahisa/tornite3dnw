//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Photon.Pun;
//using Photon.Pun.Demo.Asteroids;

//public class ShootController : MonoBehaviourPunCallbacks {



//    private BulletController bc;

//    private float shotDeltatime = 999f;


//    public AudioClip noArmo;
//    public AudioClip weponSe1;
//    public AudioClip weponSe2;
//    public AudioClip reload;
//    public AudioClip changeWepon;
//    private AudioSource audioSource;

//    void Start() {
//        //Component‚ðŽæ“¾
//        audioSource = GetComponent<AudioSource>();
//    }

//    void Update() {

     
//        shotDeltatime += Time.deltaTime;


      


//        if (Input.GetKey(KeyCode.Mouse0) && Cursor.lockState == CursorLockMode.Locked) {
//            if (shotDeltatime < bc.getInterval()) {
//                return;
//            } else {

//                audioSource.PlayOneShot(weponSe2);
//                float xrecoil;
//                xrecoil = Random.Range(-1, 1);
//                GetComponentInChildren<Camera>().transform.Rotate(new Vector3(-1, 0, 0), Space.Self);
//                transform.RotateAround(transform.position, Vector3.up, xrecoil);




//                // ’e”­ŽËƒJƒEƒ“ƒg‚ð1Œ¸‚ç‚·
//                bc.shoot();
//            }
//            shotDeltatime = 0;

//            if (!bc.canShoot()) {
//                audioSource.PlayOneShot(noArmo);
//                return;
//            }









//        } else if (Input.GetKeyDown(KeyCode.R)) {
//            audioSource.PlayOneShot(reload);
//            bc.reload();



//        }
//    }
//}
