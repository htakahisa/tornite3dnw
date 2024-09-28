using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2Controller : BulletController
{
    //[SerializeField]
    //private ParticleSystem damageParticle;

    static public float shotSpeed = 9999990;
    static public int magazineSize = 25;
    static float interval = 0.07f;

    static int damage = 2;

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

        if (collision.gameObject.CompareTag("armo"))
        {
            return;
        }


        //Vector3 effectPos = transform.position - transform.forward * 3f;

        //var obj = Instantiate(damageParticle, effectPos, transform.rotation);
        //ParticleSystem p = obj.GetComponent<ParticleSystem>();
        //p.Play();


        Destroy(this.gameObject, 0.01f);
    }
}
