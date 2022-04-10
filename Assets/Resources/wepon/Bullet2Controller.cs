using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2Controller : BulletController
{

    static public float shotSpeed = 2500;
    static public int magazineSize = 18;
    static float interval = 0.1f;

    static int damage = 7;

    public Bullet2Controller() : base(shotSpeed,
         magazineSize,
         interval,
         damage)
    {
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
}
