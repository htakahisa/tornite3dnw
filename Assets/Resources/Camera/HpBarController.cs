using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class HpBarController : MonoBehaviour
{
    private Slider slider;

    private float hp = 100;


    public AudioClip damagedSe;
    private AudioSource audioSource;


    private float showDamageDelta = 999.0f;
    private float showDamageTime = 5.0f;
    private int damageSum = 0;
    private float textColorVisualRatio = 0; // �e�L�X�g�̓����x 0 �͓���

    [SerializeField]
    private ParticleSystem damageParticle;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        showDamageDelta += Time.deltaTime;
        if (showDamageDelta < showDamageTime)
        {
            // �t�F�[�h�A�E�g
            GetComponentInChildren<Text>().color = new Color(255, 0, 0, textColorVisualRatio);
            textColorVisualRatio -= Time.deltaTime;
            //�����x��0�ɂȂ�����I������B
            if (textColorVisualRatio < 0)
            {
                textColorVisualRatio = 0;
            }
        }

        // Damage text �͏�ɃJ����������
        GetComponentInChildren<Text>().transform.rotation = Camera.main.transform.rotation;

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "armo")
        {
            slider = GetComponentInChildren<Slider>();
            if (slider != null) {
                //Component���擾
                audioSource = gameObject.GetComponentInChildren<AudioSource>();
                audioSource.PlayOneShot(damagedSe);

                BulletController bc = collision.gameObject.GetComponent<BulletController>();
                hp = hp - bc.getDamage();
                slider.value = hp;

                //GetComponentInChildren<Text>().text = hp.ToString();

                GetComponent<PhotonView>().RPC(nameof(DisplayHP), RpcTarget.Others, bc.getDamage().ToString());

                damageParticle.transform.position = transform.position;
                damageParticle.Play();
            }
        }
    }

    [PunRPC]
    void DisplayHP(string damage)
    {
        // receive the synced value if needed 

        if (showDamageDelta < showDamageTime / 2)
        {
            damageSum += Int32.Parse(damage);
        } else
        {
            damageSum = Int32.Parse(damage);
        }

        GetComponentInChildren<Text>().text = damageSum.ToString();
        showDamageDelta = 0;
        // �����x�� 100%�ɂ���
        textColorVisualRatio = 1;
    }
}
