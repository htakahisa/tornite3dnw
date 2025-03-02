using Photon.Pun;
using UnityEngine;

public class Flash : MonoBehaviourPun
{


    public float flashDuration = 2f; // �t���b�V���̎�������

    private float throwForce = 9f; // �������

    Rigidbody rb;

    private AudioSource audio;

    // Start is called before the first frame update
    void Awake()
    {
        audio = GetComponent<AudioSource>();
        if (photonView.IsMine)
        {
            rb = gameObject.GetComponent<Rigidbody>();
            // �O���l�[�h�ɗ͂�������
            Vector3 throwDirection = transform.forward * 2;
            rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
        }

        Invoke("Blast", 0.7f);
    }

    // Update is called once per frame
    void Update()
    {




    }


    private void Blast()
    {
        audio.Play();

        int layerMask = LayerMask.GetMask("MapObject", "BackHead");

        Vector3 direction = (Camera.main.transform.position - transform.position).normalized;
        RaycastHit hit;
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        if (!Physics.Raycast(transform.position, direction, out hit, distance, layerMask))
        {
            PlayerFlashEffect.pfe.ApplyFlash(flashDuration);
            Debug.Log("�t���b�V��");

        }

        Debug.Log(hit.collider);
        Debug.DrawRay(transform.position, direction * 100, Color.green);




        Destroy(gameObject);



    }




}
