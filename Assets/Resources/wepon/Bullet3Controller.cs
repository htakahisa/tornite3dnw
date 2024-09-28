using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet3Controller : BulletController {
   

    static public float shotSpeed = 1000000;
    static public int magazineSize = 33;
    static float interval = 0.13f;
    
    static int damage = 3;

    public Bullet3Controller() : base(shotSpeed,
         magazineSize,
         interval,
         damage) {
    }


    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("armo"))
        {
            return;
        }


   


        Destroy(this.gameObject, 0.01f);
    }
}
