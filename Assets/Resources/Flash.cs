using UnityEngine;

public class Flash : MonoBehaviour
{
    private Renderer _renderer;

    private GameObject flashEffect; // フラッシュエフェクトのプレハブ
    public float flashDuration = 2f; // フラッシュの持続時間

    private float throwForce = 4f; // 投げる力
    private float upwardForce = 1.5f; // 上向きの力

    Rigidbody rb;

    private SoundManager sm;

    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        sm = Camera.main.transform.parent.GetComponent<SoundManager>();
        // グレネードに力を加える
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
            // レイキャストで障害物をチェック
            Vector3 directionToPlayer = Camera.main.transform.position - transform.position;
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, directionToPlayer, out hit, directionToPlayer.magnitude)
                || hit.collider.gameObject.name == "head.x")
            {

                flashEffect.GetComponent<PlayerFlashEffect>().ApplyFlash(flashDuration);
                Debug.Log("フラッシュ");
            }
        }




        Destroy(gameObject);



    }




}
