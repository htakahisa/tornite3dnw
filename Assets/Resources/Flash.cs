using UnityEngine;

public class Flash : MonoBehaviour
{
    private Renderer _renderer;

    public float flashDuration = 2f; // �t���b�V���̎�������

    private float throwForce = 9f; // �������

    Rigidbody rb;

    private SoundManager sm;

    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        sm = Camera.main.transform.parent.GetComponent<SoundManager>();
        // �O���l�[�h�ɗ͂�������
        Vector3 throwDirection = transform.forward * 2;
        rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
        _renderer = GetComponent<Renderer>();

        

        Invoke("Blast", 0.7f);
    }

    // Update is called once per frame
    void Update()
    {




    }


    private void Blast()
    {
        sm.PlayMySound("flash");

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
