using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bullet4Controller : BulletController {
   

    static public float shotSpeed = 1000000;
    static public int magazineSize = 7;
    static float interval = 0.3f;
    
    static int damage = 45;

    public bullet4Controller() : base(shotSpeed,
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
