using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2Controller : BulletController
{
    [SerializeField]
    private ParticleSystem damageParticle;

    static public float shotSpeed = 3500;
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

    public void OnCollisionEnter(Collision collision)
    {
        // 爆発エフェクト取得
        //damageParticle.transform.position = transform.position;
        //damageParticle.Play();

        if (collision.gameObject.CompareTag("armo"))
        {
            return;
        }



        var obj = Instantiate(damageParticle, transform.position, transform.rotation);
        ParticleSystem p = obj.GetComponent<ParticleSystem>();
        p.Play();


        Destroy(this.gameObject, 0.01f);
    }
}
