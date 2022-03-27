using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletController : MonoBehaviour
{

    protected float shotSpeed = 0;
    protected int magazineSize = 0;
    protected int currentMagazineSize = 0;

    protected float interval = 0f;

    protected int damage = 7;

    public BulletController(float shotSpeed,
        int magazineSize,
        float interval,
        int damage
        )
    {
        this.shotSpeed = shotSpeed;
        this.magazineSize = magazineSize;
        this.currentMagazineSize = magazineSize;

        this.interval = interval;
        this.damage = damage;
    }





    public int getDamage()
    {
        return this.damage;
    }

    public float getInterval()
    {
        return this.interval;
    }

    public float getShotSpeed()
    {
        return shotSpeed;
    }

    public int getMagazineSize()
    {
        return magazineSize;
    }

    public int getCurrentMagazineSize()
    {
        return currentMagazineSize;
    }

    public void reload()
    {
        this.currentMagazineSize = magazineSize;
    }

    public void shoot()
    {
        this.currentMagazineSize -= 1;
    }

    public bool canShoot()
    {
        return this.currentMagazineSize > 0;

    }
}
