using UnityEngine;

public class Flash : MonoBehaviour
{
    private Renderer _renderer;

    private GameObject flashEffect; // �t���b�V���G�t�F�N�g�̃v���n�u
    public float flashDuration = 2f; // �t���b�V���̎�������

    private float throwForce = 4f; // �������
    private float upwardForce = 1.5f; // ������̗�

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
        flashEffect = GameObject.FindWithTag("Flash");
        _renderer = GetComponent<Renderer>();


        Invoke("Blast", 1f);
    }

    // Update is called once per frame
    void Update()
    {




    }


    bool IsVisibleFrom(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    private void Blast()
    {

        sm.PlaySound("flash");
        if (IsVisibleFrom(_renderer, Camera.main))
        {
            // ���C�L���X�g�ŏ�Q�����`�F�b�N
            Vector3 directionToPlayer = Camera.main.transform.position - transform.position;
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, directionToPlayer, out hit, directionToPlayer.magnitude)
                || hit.collider.gameObject.name == "head.x")
            {

                flashEffect.GetComponent<PlayerFlashEffect>().ApplyFlash(flashDuration);
                Debug.Log("�t���b�V��");
            }
        }




        Destroy(gameObject);



    }




}
