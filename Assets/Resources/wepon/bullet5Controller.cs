using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bullet5Controller : BulletController {
   

    static public float shotSpeed = 1000000;
    static public int magazineSize = 1;
    static float interval = 2f;
    
    static int damage = 200;

    public bullet5Controller() : base(shotSpeed,
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
