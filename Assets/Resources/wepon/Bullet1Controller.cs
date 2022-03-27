using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1Controller : BulletController
{


    static float shotSpeed = 1000;
    static int magazineSize = 8;
    static float interval = 0.5f;


    public Bullet1Controller() : base( shotSpeed,
         magazineSize,
         interval)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
