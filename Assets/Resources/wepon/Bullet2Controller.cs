using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2Controller : BulletController
{

    static public float shotSpeed = 1200;
    static public int magazineSize = 30;
    static float interval = 0.1f;

    public Bullet2Controller() : base(shotSpeed,
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
