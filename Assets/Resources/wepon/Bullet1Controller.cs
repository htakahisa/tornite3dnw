using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet1Controller : BulletController
{
    [SerializeField]
    private ParticleSystem damageParticle;

    static float shotSpeed = 2000;
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
