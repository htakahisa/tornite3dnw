using UnityEngine;

public class SlingShotJump : MonoBehaviour
{
    private Transform cameraTransform; // カメラのTransform
    public LineRenderer lineRenderer; // スリングのライン
    public float maxPullForce = 20f;  // 最大チャージ量
    public float chargeSpeed = 15f;   // チャージ速度
    private float gravity = 16f;      // 重力
    public LayerMask hitLayer;        // 地面や壁のレイヤー

    private CharacterController controller;
    private Vector3 slingStartPoint;  // スリングの開始点
    private Vector3 slingEndPoint;    // スリングの終点
    private bool isCharging = false;  // チャージ中かどうか
    private float currentCharge = 0f; // 現在のチャージ量
    private bool isLaunching = false; // 発射中かどうか
    private Vector3 launchVelocity;   // 発射時の速度

    void Start()
    {
        controller = GetComponent<CharacterController>();
        lineRenderer.enabled = false; // 初期状態で非表示
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (isLaunching)
        {
            ApplySlingShotMovement();
        }

        if (cameraTransform.GetComponent<RayController>().GetWeaponNumber() == 17)
        {
            HandleInput();
        }

        UpdateSlingLine(); // スリングラインを更新
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリック押し始め → 始点を設定
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 50f, hitLayer))
            {
                slingStartPoint = hit.point;
                isCharging = true;
                currentCharge = 0f;
                lineRenderer.enabled = true; // ワイヤーを表示
            }
        }

        if (isCharging) // チャージ中の処理
        {
            if (cameraTransform.GetComponentInParent<CameraController>().SlingPower >= currentCharge)
            {
                currentCharge += chargeSpeed * Time.deltaTime;
                currentCharge = Mathf.Clamp(currentCharge, 0, maxPullForce);
            }

                // 終点をカメラの正面方向に動的更新
                slingEndPoint = cameraTransform.position + cameraTransform.forward * 10f;
        }

        if (Input.GetMouseButtonUp(0) && isCharging) // 左クリックを離したら終点を確定＆発射
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 50f, hitLayer))
            {
                slingEndPoint = hit.point; // 終点を確定
            }

            Launch();
        }
    }

    void Launch()
    {
        cameraTransform.GetComponentInParent<CameraController>().SlingPower -= (int)currentCharge;
        Vector3 launchDirection = (slingStartPoint + slingEndPoint) / 2 - transform.position;
        launchDirection.Normalize();

        launchVelocity = launchDirection * currentCharge;
        isLaunching = true;
        isCharging = false;
        lineRenderer.enabled = false; // 発射後にラインを非表示
    }

    void ApplySlingShotMovement()
    {
        if (controller.isGrounded && launchVelocity.y < 0)
        {
            isLaunching = false;
            return;
        }

        
        launchVelocity.y -= gravity * Time.deltaTime;
        controller.Move(launchVelocity * Time.deltaTime);
    }

    void UpdateSlingLine()
    {
        if (!lineRenderer.enabled) return;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, slingStartPoint);

        if (isCharging)
        {
            // 長押し中は終点をカメラの正面に更新
            lineRenderer.SetPosition(1, slingEndPoint);
        }
    }
}
