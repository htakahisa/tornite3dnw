using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet3Controller : MonoBehaviour {
    [SerializeField]
    private ParticleSystem damageParticle;

    static public float shotSpeed = 10000;
    static public int magazineSize = 50;
    static float interval = 0.05f;

    static int damage = 2;

  
    

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


        Vector3 effectPos = transform.position - transform.forward * 3f;

        var obj = Instantiate(damageParticle, effectPos, transform.rotation);
        ParticleSystem p = obj.GetComponent<ParticleSystem>();
        p.Play();


        Destroy(this.gameObject, 0.01f);
    }
}
