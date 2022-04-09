using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet1Controller : BulletController
{


    static float shotSpeed = 4000;
    static int magazineSize = 8;
    static float interval = 0.5f;

    static int damage = 5;

    public Bullet1Controller() : base( shotSpeed,
         magazineSize,
         interval,
         damage)
    {
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 2.0f);
    }



}
