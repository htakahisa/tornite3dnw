using UnityEngine;

public class Flash : MonoBehaviour
{
    private Renderer _renderer;

    private GameObject flashEffect; // �t���b�V���G�t�F�N�g�̃v���n�u
    public float flashDuration = 2f; // �t���b�V���̎�������

    private float throwForce = 4f; // �������
    private float upwardForce = 1.5f; // ������̗�

    Rigidbody rb;



    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        // �O���l�[�h�ɗ͂�������
        Vector3 throwDirection = transform.forward + transform.up * upwardForce;
        rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
        flashEffect = GameObject.FindWithTag("Flash");
        _renderer = GetComponent<Renderer>();


        Invoke("Blast", 0.5f);
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

        SoundManager.sm.PlaySound("flash");
        if (IsVisibleFrom(_renderer, Camera.main))
        {
            // ���C�L���X�g�ŏ�Q�����`�F�b�N
            Vector3 directionToPlayer = Camera.main.transform.position - transform.position;
            RaycastHit hit;
            Debug.Log("blast:" + Physics.Raycast(transform.position, directionToPlayer, out hit, directionToPlayer.magnitude));
            Debug.Log("����:" + hit.collider.gameObject.name);
            if (!Physics.Raycast(transform.position, directionToPlayer, out hit, directionToPlayer.magnitude)
                || hit.collider.gameObject.name == "Head")
            {

                flashEffect.GetComponent<PlayerFlashEffect>().ApplyFlash(flashDuration);
                Debug.Log("�t���b�V��");
            }
        }




        Destroy(gameObject);



    }




}