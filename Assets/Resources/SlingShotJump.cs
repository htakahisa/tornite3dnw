using UnityEngine;

public class SlingShotJump : MonoBehaviour
{
    private Transform cameraTransform; // �J������Transform
    public LineRenderer lineRenderer; // �X�����O�̃��C��
    public float maxPullForce = 30f;  // �ő�`���[�W��
    public float chargeSpeed = 20f;   // �`���[�W���x
    private float gravity = 16f;      // �d��
    public LayerMask hitLayer;        // �n�ʂ�ǂ̃��C���[

    private CharacterController controller;
    private Vector3 slingStartPoint;  // �X�����O�̊J�n�_
    private Vector3 slingEndPoint;    // �X�����O�̏I�_
    private bool isCharging = false;  // �`���[�W�����ǂ���
    private float currentCharge = 0f; // ���݂̃`���[�W��
    private bool isLaunching = false; // ���˒����ǂ���
    private Vector3 launchVelocity;   // ���ˎ��̑��x

    private CameraController cc;
    private RayController rc;

    public float SlingRatio = 0.5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        lineRenderer.enabled = false; // ������ԂŔ�\��
        cameraTransform = Camera.main.transform;
        cc = GetComponent<CameraController>();
        rc = GetComponent<RayController>();
    }

    void Update()
    {
        if (isLaunching)
        {
            ApplySlingShotMovement();
        }

        if (cameraTransform.GetComponent<RayController>().GetWeaponNumber() == 17 && cc.IsGrounded())
        {
            HandleInput();
        }

        if (!cc.IsGrounded())
        {
            rc.DestroyAbilityCheck();
        }

        UpdateSlingLine(); // �X�����O���C�����X�V
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0)) // ���N���b�N�����n�� �� �n�_��ݒ�
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 50f, hitLayer))
            {
                slingStartPoint = hit.point;
                isCharging = true;
                currentCharge = 0f;
                lineRenderer.enabled = true; // ���C���[��\��
            }
        }

        if (isCharging) // �`���[�W���̏���
        {
            if (cc.SlingPower >= currentCharge)
            {
                currentCharge += chargeSpeed * Time.deltaTime;
                currentCharge = Mathf.Clamp(currentCharge, 0, maxPullForce);
            }

                // �I�_���J�����̐��ʕ����ɓ��I�X�V
                slingEndPoint = cameraTransform.position + cameraTransform.forward * 10f;
        }

        if (Input.GetMouseButtonUp(0) && isCharging) // ���N���b�N�𗣂�����I�_���m�聕����
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 50f, hitLayer))
            {
                slingEndPoint = hit.point; // �I�_���m��
            }

            Launch();
        }
    }

    void Launch()
    {
        GetComponent<Ability>().Spend(1, (int)currentCharge / 10);
        Vector3 launchDirection = (slingStartPoint + slingEndPoint) / 2 - transform.position;
        launchDirection.Normalize();

        launchVelocity = launchDirection * currentCharge;
        isLaunching = true;
        isCharging = false;
        lineRenderer.enabled = false; // ���ˌ�Ƀ��C�����\��
    }

    void ApplySlingShotMovement()
    {
        if (controller.isGrounded && launchVelocity.y < 0)
        {
            isLaunching = false;
            return;
        }

        
        launchVelocity.y -= gravity * Time.deltaTime;
        launchVelocity.x *= SlingRatio;
        launchVelocity.z *= SlingRatio;
        controller.Move(launchVelocity * Time.deltaTime);
    }

    void UpdateSlingLine()
    {
        if (!lineRenderer.enabled) return;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, slingStartPoint);

        if (isCharging)
        {
            // ���������͏I�_���J�����̐��ʂɍX�V
            lineRenderer.SetPosition(1, slingEndPoint);
        }
    }
}
