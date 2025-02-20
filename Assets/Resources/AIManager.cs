using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    public AIState currentState = AIState.Chase;
    private Transform target; // �v���C���[
    private CameraController cc;

    public Transform point;
    private float DelayShoot = 0.1f;
    private float FirstDelayShoot = 0;

    public LayerMask WallHitMask;
    public LayerMask HitMask;

    private Vector3 lastPosition;
    private float stuckTime;
    private const float stuckThreshold = 1.0f; // 1�b�ȏ㓮���Ȃ��ƃX�^�b�N����

    PlayTransformSound pts;
    private float RunInterval = 0.4f;

    private float delayreflect = 0.3f;

    private int HeadDamage = 0;
    private int BodyDamage = 0;

    private float HeadPunch = 0;

    private Object weapon;

    public float duration = 0.1f; // ��]�ɂ����鎞��


    // Start is called before the first frame update
    void Start()
    {

        int range = Random.Range(1, 5);
        if (range == 1)
        {
            weapon = new Silver();
            // Silver�^�ɃL���X�g
            Silver silverWeapon = weapon as Silver;
            FirstDelayShoot = silverWeapon.GetRate();
            HeadDamage = silverWeapon.GetHeadDamage();
            BodyDamage = silverWeapon.GetDamage();
            HeadPunch = 0;
            HpMaster.hpmaster.SetAIShield(0);
        }
        else if (range == 2)
        {
            weapon = new Stella();

            Stella stellaWeapon = weapon as Stella;
            FirstDelayShoot = stellaWeapon.GetRate();
            HeadDamage = stellaWeapon.GetHeadDamage();
            BodyDamage = stellaWeapon.GetDamage();
            HeadPunch = stellaWeapon.GetPunch();
            HpMaster.hpmaster.SetAIShield(0.67f);
        }
        else if (range == 3)
        {
            weapon = new mischief();

            mischief mischiefWeapon = weapon as mischief;
            FirstDelayShoot = mischiefWeapon.GetRate();
            HeadDamage = mischiefWeapon.GetHeadDamage();
            BodyDamage = mischiefWeapon.GetDamage();
            HeadPunch = mischiefWeapon.GetPunch();
            HpMaster.hpmaster.SetAIShield(0.8f);
        }
        else if (range == 4)
        {
            weapon = new Noel();

            Noel noelWeapon = weapon as Noel;
            FirstDelayShoot = noelWeapon.GetRate();
            HeadDamage = noelWeapon.GetHeadDamage();
            BodyDamage = noelWeapon.GetDamage();
            HeadPunch = 0;
            HpMaster.hpmaster.SetAIShield(1);
        }
        cc = GetComponent<CameraController>();
        pts = GetComponent<PlayTransformSound>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case AIState.Aim:
                Aim();
                break;
            case AIState.Chase:
                Chase();
                break;

            case AIState.Attack:
                DelayShoot -= Time.deltaTime;
                Attack();
                break;
        }
        Debug.DrawRay(point.position, point.forward * 100, Color.red);

        RunInterval -= Time.deltaTime;

    }

    public enum AIState
    {
        Chase,   // �ǐՁi�v���C���[�ɋ߂Â��j
        Aim,     // �G�C���i�Ə��ړ��j
        Attack   // �U���i�ˌ��j
        
    }

    public void Chase()
    {
        Debug.Log("Chase");
        target = MyTag.mytag.transform;
        Vector3 direction = target.position - transform.position;

        RaycastHit hit;
        Vector3 moveDirection = GetAdaptiveDirection(transform, target);
        if (RunInterval <= 0)
        {
           pts.PlayTransSound("walk");
           RunInterval = 0.4f;
        }
        float distance = Vector3.Distance(point.position, target.position);

        if (Physics.Raycast(point.position, direction, distance, WallHitMask))
        {
            if (moveDirection != Vector3.zero)
            {
                cc.AIWalk(moveDirection * 0.5f);
                
            }
            
        }
        else
        {
            DelayReflect();
        }

    }

    private void DelayReflect()
    {
        delayreflect -= Time.deltaTime;
        if (delayreflect <= 0)
        {
            currentState = AIState.Aim;
            delayreflect = 0.05f;
        }
        else
        {
            DelayingWalk();
        }
    }

    private void DelayingWalk()
    {
        Vector3 moveDirection = GetAdaptiveDirection(transform, target);

        if (moveDirection != Vector3.zero)
        {
            cc.AIWalk(moveDirection * 0.2f);

        }
    }


    public static Vector3 GetBestDirection(Transform self, Transform target)
    {
        // ��������^�[�Q�b�g�ւ̕����x�N�g��
        Vector3 toTarget = (target.position - self.position).normalized;

        // �����̊��
        Vector3 forward = self.forward;
        Vector3 right = self.right;

        // �e�����Ƃ̓��ς��v�Z
        float forwardDot = Vector3.Dot(forward, toTarget);
        float backwardDot = Vector3.Dot(-forward, toTarget);
        float rightDot = Vector3.Dot(right, toTarget);
        float leftDot = Vector3.Dot(-right, toTarget);

        // ��Ԓl���傫��������I��
        float[] dotValues = { forwardDot, backwardDot, rightDot, leftDot };
        Vector3[] directions = { forward, -forward, right, -right };

        // �\�[�g���čœK�ȏ��Ԃ�����
        for (int i = 0; i < dotValues.Length - 1; i++)
        {
            for (int j = i + 1; j < dotValues.Length; j++)
            {
                if (dotValues[j] > dotValues[i])
                {
                    float tempValue = dotValues[i];
                    dotValues[i] = dotValues[j];
                    dotValues[j] = tempValue;

                    Vector3 tempDir = directions[i];
                    directions[i] = directions[j];
                    directions[j] = tempDir;
                }
            }
        }

        return directions[0];
    }

    public Vector3 GetAdaptiveDirection(Transform self, Transform target)
    {
        Vector3 bestDirection = GetBestDirection(self, target);
        Vector3 secondBestDirection = Vector3.zero;

        // 1�b�ȏ㓮���Ȃ������ꍇ�A�ʂ̕���������
        if (Vector3.Distance(self.position, lastPosition) < 0.01f)
        {
            stuckTime += Time.deltaTime;
            if (stuckTime >= stuckThreshold)
            {
                secondBestDirection = GetBestDirection(self, target);
                if (secondBestDirection != bestDirection)
                {
                    bestDirection = secondBestDirection;
                }
                else
                {
                    bestDirection = Vector3.zero; // ����ł������Ȃ��Ȃ��~
                }
                stuckTime = 0f; // �X�^�b�N��������Z�b�g
            }
        }
        else
        {
            stuckTime = 0f;
        }

        lastPosition = self.position;
        return bestDirection;
    }

    public void Aim()
    {
        
      

        Debug.Log("Aim");

        target = MyTag.mytag.transform;
        Vector3 direction = target.position - transform.position;
        direction.y = 0; // Y���̉�]���Œ�

        StartCoroutine(RotateSmoothly(direction));

        RaycastHit hit;
        Physics.Raycast(point.position, point.forward, out hit, 200, HitMask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Body" || hit.collider.gameObject.tag == "Head")
            {
                currentState = AIState.Attack;
            }
            Physics.Raycast(point.position, direction, out hit, 200, HitMask);
            if (hit.collider.gameObject.tag != "Body" && hit.collider.gameObject.tag != "Head")
            {
                currentState = AIState.Chase;
            }
        }
     


    }

    private IEnumerator RotateSmoothly(Vector3 targetDirection)
    {
        if (targetDirection == Vector3.zero) yield break; // �[�������Ȃ牽�����Ȃ�

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation; // �ŏI�I�Ƀs�b�^���ڕW����������
        Vector3 targethead = new Vector3(target.position.x, target.position.y + Random.Range(1.1f, 2f), target.position.z);
        point.transform.LookAt(targethead);
    }

    public void Attack()
    {


        if (DelayShoot >= FirstDelayShoot)
        {
            return;
        }

        Debug.Log("Attack");

        bool IsAiming = false;
        RaycastHit hit;
        if (Physics.Raycast(point.transform.position, point.transform.forward, out hit, 200, HitMask))
        {
           

            
                DamageManager dm = new DamageManager();
                int damage = 0;
                if (hit.collider.gameObject.tag.Equals("Head"))
                {
                    damage = HeadDamage;
                    IsAiming = true;
                    GetTopmostParent(hit.collider.gameObject).GetComponent<CameraController>().Stuned(HeadPunch, HeadPunch);

                }
                if (hit.collider.gameObject.tag.Equals("Body"))
                {
                    damage = BodyDamage;
                    IsAiming = true;
                }
                if (!tag.Equals("Head") && !tag.Equals("Body"))
                {
                    IsAiming = false;
                }
                dm.causeDamage(hit.collider.gameObject, damage);
            

        }

        DelayShoot = FirstDelayShoot;
        Physics.Raycast(point.position, point.forward, out hit, 200, HitMask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag != "Body" && hit.collider.gameObject.tag != "Head")
            {
                currentState = AIState.Aim;
            }
        }
        target = MyTag.mytag.transform;
        Vector3 direction = target.position - transform.position;
        direction.y = 0; // Y���̉�]���Œ�
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
        Physics.Raycast(point.position, direction, out hit, 200, HitMask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag != "Body" && hit.collider.gameObject.tag != "Head")
            {
                currentState = AIState.Chase;
            }
        }





        }
    GameObject GetTopmostParent(GameObject obj)
    {
        Transform current = obj.transform;

        // ���[�g�I�u�W�F�N�g�ɓ��B����܂Őe�����ǂ�
        while (current.parent != null)
        {
            current = current.parent;
        }

        return current.gameObject;
    }

}
