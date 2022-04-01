using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour
{
    private Slider slider;

    private float hp = 100;


    public AudioClip damagedSe;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "armo")
        {
            slider = GetComponentInChildren<Slider>();
            if (slider != null) {
                //Component‚ðŽæ“¾
                audioSource = gameObject.GetComponentInChildren<AudioSource>();
                audioSource.PlayOneShot(damagedSe);

                BulletController bc = collision.gameObject.GetComponent<BulletController>();
                hp = hp - bc.getDamage();
                slider.value = hp;

                GetComponent<DamageShowController>().Damage(collision);
            }
        }
    }


}
