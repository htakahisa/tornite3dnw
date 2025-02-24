using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    public AIState currentState = AIState.Chase;
    private Transform target; // プレイヤー
    private CameraController cc;

    public Transform point;
    private float DelayShoot = 0.1f;
    private float FirstDelayShoot = 0;

    public LayerMask WallHitMask;
    public LayerMask HitMask;

    private Vector3 lastPosition;
    private float stuckTime;
    private const float stuckThreshold = 1f; // 1秒以上動かないとスタック判定

    PlayTransformSound pts;
    private float RunInterval = 0.4f;

    private float delayreflect = 0f;

    private int HeadDamage = 0;
    private int BodyDamage = 0;

    private float HeadPunch = 0;

    private Object weapon;

    private float duration = 0.1f; // 回転にかける時間

    public float rotationSpeed = 5f;
    public float obstacleDetectionDistance = 2f;
    public float sideDetectionDistance = 1.5f;
    public float avoidanceForce = 3f;
    public float characterWidth = 1.0f;

    private float rayOffset = 0.1f; //  レイの発射位置を少し前にずらす

    private float gravity = -16f;    // 重力の強さ
    private CharacterController controller;  // CharacterControllerコンポーネント
    private Vector3 velocity;

    public Vector3 boxSize = new Vector3(0.3f, 0.1f, 0.3f); // 判定用のBoxサイズ

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        delayreflect = GetRandomDelay();
        int range = Random.Range(1, 5);
        if (range == 1)
        {
            weapon = new Silver();
            // Silver型にキャスト
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
        Debug.Log(weapon);
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
            case AIState.Avoid:
                DelayShoot -= Time.deltaTime;
                Avoid();
                break;
        }
   

    }

    private void FixedUpdate()
    {
        // 地面にいる場合、垂直速度をリセット
        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2f; // 少し負の値にして地面に密着させる
            velocity.x = 0f;  // 水平方向の速度をリセット
            velocity.z = 0f;
        }
        velocity.y += gravity * Time.fixedDeltaTime;

        // 垂直方向の移動
        controller.Move(velocity * Time.fixedDeltaTime);
        RunInterval -= Time.deltaTime;
    }

    public enum AIState
    {
        Chase,   // 追跡（プレイヤーに近づく）
        Aim,     // エイム（照準移動）
        Attack,   // 攻撃（射撃）
        Avoid
    }
    void OnDrawGizmos()
    {
        Vector3 Target = new Vector3(target.position.x, target.position.y + 1.3f, target.position.z);
        Vector3 TargetDirection = (Target - point.position);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(point.position, TargetDirection);
        Gizmos.color = Color.red;
        Vector3 basePos = transform.position + transform.forward * rayOffset;
        Vector3 rightStart = basePos + transform.right * (characterWidth / 2);
        Vector3 leftStart = basePos - transform.right * (characterWidth / 2);

        Gizmos.DrawRay(basePos, transform.forward * obstacleDetectionDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(rightStart, transform.forward * obstacleDetectionDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(leftStart, transform.forward * obstacleDetectionDistance);
    }


    Vector3 AvoidObstacle()
    {
        Vector3 avoidance = Vector3.zero;

        Vector3 basePos = transform.position + transform.forward * rayOffset;
        Vector3 rightPos = basePos + transform.right * (characterWidth / 2);
        Vector3 leftPos = basePos - transform.right * (characterWidth / 2);

        bool frontBlocked = Physics.Raycast(basePos, transform.forward, obstacleDetectionDistance);
        bool rightBlocked = Physics.Raycast(rightPos, transform.forward, obstacleDetectionDistance);
        bool leftBlocked = Physics.Raycast(leftPos, transform.forward, obstacleDetectionDistance);


        if (frontBlocked || rightBlocked || leftBlocked)
        {
            Vector3 rightDir = transform.right;
            Vector3 leftDir = -transform.right;

            bool rightClear = !Physics.Raycast(transform.position, rightDir, sideDetectionDistance);
            bool leftClear = !Physics.Raycast(transform.position, leftDir, sideDetectionDistance);

            if (rightBlocked && !leftBlocked)
                avoidance = leftDir;
            else if (leftBlocked && !rightBlocked)
                avoidance = rightDir;
            else if (rightClear && !leftClear)
                avoidance = rightDir;
            else if (!rightClear && leftClear)
                avoidance = leftDir;


            avoidance *= avoidanceForce;
        }

        return avoidance;
    }

    public void Chase()
    {
        


        target = MyTag.mytag.transform;
        // ターゲットへの基本的な追跡方向
        Vector3 direction = (target.position - point.position).normalized;
        Vector3 avoidance = AvoidObstacle();

        // 回避方向とターゲット方向を合成
        Vector3 finalDirection;

        if (avoidance == Vector3.zero)
        {
            finalDirection = (direction + avoidance).normalized;
        }
        else
        {
            // 回避方向とターゲット方向を合成
            finalDirection = avoidance;
        }
            finalDirection.y = 0;

        // 最終的な進行方向が十分であれば、回転と移動を行う
        if (finalDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(finalDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            Vector3 walkdirection = finalDirection;
            

            cc.AIWalk(transform.forward);
            if (RunInterval <= 0)
            {
                    pts.PlayTransSound("walk");
                    RunInterval = 0.4f;
            }
                
        }

        RaycastHit hit;

        Vector3 Target = new Vector3(target.position.x, target.position.y + 1.3f, target.position.z);

        Vector3 TargetDirection = (Target - point.position);

        if (Physics.Raycast(point.position, TargetDirection, out hit, 100, HitMask))
        {
            
            if(hit.collider != null)
            {

                if(hit.collider.gameObject.tag == "Body" || hit.collider.gameObject.tag == "Head")
                {
                    currentState = AIState.Aim;
                }
            }    
              
            
            
        }



    }

    public void Avoid()
    {


        Debug.Log("Avoid");
        target = MyTag.mytag.transform;

        Vector3 moveDirection;

        if (AvoidObstacle() == Vector3.zero) {
            moveDirection = -transform.right;
        }
        else
        {
            moveDirection = AvoidObstacle();
        }

        Vector3 Target = new Vector3(target.position.x, target.position.y + 1.3f, target.position.z);

        Vector3 TargetDirection = (Target - point.position);


        cc.AIWalk(moveDirection);
        if (RunInterval <= 0)
        {
            pts.PlayTransSound("walk");
            RunInterval = 0.4f;
        }

        RaycastHit hit;

        if (Physics.Raycast(point.position, TargetDirection, out hit, 100, HitMask))
        {
            if (hit.collider != null)
            {

                if (hit.collider.gameObject.tag == "Body" || hit.collider.gameObject.tag == "Head")
                {
                    DelayReflect();
                }
                else
                {
                    currentState = AIState.Chase;
                }
            }
            else
            {
                currentState = AIState.Chase;
            }

        }
        else
        {
            currentState = AIState.Chase;
        }




    }

    private void DelayReflect()
    {
        delayreflect -= Time.deltaTime;
        if (delayreflect <= 0)
        {
            currentState = AIState.Aim;
            delayreflect = GetRandomDelay();
        }
    }

    private float GetRandomDelay()
    {
        return Random.Range(0.1f,1f);
    }

    private void DelayingWalk()
    {
      
    }


    public static Vector3 GetBestDirection(Transform self, Transform target, int number)
    {
        // 自分からターゲットへの方向ベクトル
        Vector3 toTarget = (target.position - self.position).normalized;

        // 自分の基準軸
        Vector3 forward = self.InverseTransformPoint(self.eulerAngles);
        Vector3 right = self.InverseTransformPoint(self.eulerAngles);        

        // 各方向との内積を計算
        float forwardDot = Vector3.Dot(forward, toTarget);
        float backwardDot = Vector3.Dot(-forward, toTarget);
        float rightDot = Vector3.Dot(right, toTarget);
        float leftDot = Vector3.Dot(-right, toTarget);

        // 一番値が大きい方向を選択
        float[] dotValues = { forwardDot, backwardDot, rightDot, leftDot };
        Vector3[] directions = { forward, -forward, right, -right };

        // ソートして最適な順番を決定
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

        return directions[number];
    }

    public Vector3 GetAdaptiveDirection(Transform self, Transform target)
    {
        Vector3 bestDirection = GetBestDirection(self, target, 0);
        Vector3 secondBestDirection = Vector3.zero;

        // 1秒以上動かなかった場合、別の方向を試す
        if (Vector3.Distance(self.position, lastPosition) < 0.01f)
        {
            stuckTime += Time.deltaTime;
            if (stuckTime >= stuckThreshold)
            {
               secondBestDirection = GetBestDirection(self, target, 1);
                if (secondBestDirection != bestDirection)
                {
                    bestDirection = secondBestDirection;
                }
                else
                {
                    bestDirection = Vector3.zero; // それでも動けないなら停止
                }
                stuckTime = 0f; // スタック判定をリセット
            }
        }
        else
        {
            stuckTime = 0f;
        }

        lastPosition = self.position;
        return bestDirection;
    }

    public bool IsGrounded()
    {

        Vector3 footPosition = transform.position;
        return Physics.BoxCast(footPosition, boxSize / 2, Vector3.down, Quaternion.identity, 0.3f, WallHitMask);

    }

    public void Aim()
    {
        
      

        Debug.Log("Aim");

        target = MyTag.mytag.transform;
        Vector3 direction = target.position - transform.position;
        direction.y = 0; // Y軸の回転を固定

        // 現在のオブジェクトの前方向（ワールド空間）
        Vector3 currentDirection = transform.forward;

        // ベクトル間の角度のズレを計算
        float angleDifference = Vector3.Angle(currentDirection, direction);
        float duration = angleDifference * 0.001f;

        StartCoroutine(RotateSmoothly(direction, duration));

        RaycastHit hit;
        Physics.Raycast(point.position, point.forward, out hit, 200, HitMask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Body" || hit.collider.gameObject.tag == "Head")
            {
                if (Random.Range(0, 2) == 0)
                {
                    currentState = AIState.Attack;
                }
                else 
                {
                    currentState = AIState.Avoid;
                }
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



    }

    private IEnumerator RotateSmoothly(Vector3 targetDirection, float duration)
    {
        if (targetDirection == Vector3.zero) yield break; // ゼロ方向なら何もしない

        float delayflick = Random.Range(0.09f,0.13f);

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Vector3 targethead = new Vector3(target.position.x, target.position.y + Random.Range(1f, 2f), target.position.z);
        transform.rotation = targetRotation; // 最終的にピッタリ目標方向を向く
        yield return new WaitForSeconds(delayflick);
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
                    GetTopmostParent(hit.collider.gameObject).GetComponent<CameraController>().Recoiled(HeadPunch, HeadPunch, 0.1f);

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


        AIState choosestate = AIState.Attack;

        DelayShoot = FirstDelayShoot;
        Physics.Raycast(point.position, point.forward, out hit, 200, HitMask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag != "Body" && hit.collider.gameObject.tag != "Head")
            {
                choosestate = AIState.Aim;
            }
        }
        target = MyTag.mytag.transform;
        Vector3 direction = target.position - transform.position;
        direction.y = 0; // Y軸の回転を固定
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
        Physics.Raycast(point.position, direction, out hit, 200, HitMask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag != "Body" && hit.collider.gameObject.tag != "Head")
            {
                choosestate = AIState.Chase;
            }
        }
        if(choosestate == AIState.Attack)
        {
            choosestate = AIState.Avoid;
        }

        currentState = choosestate;





    }
    GameObject GetTopmostParent(GameObject obj)
    {
        Transform current = obj.transform;

        // ルートオブジェクトに到達するまで親をたどる
        while (current.parent != null)
        {
            current = current.parent;
        }

        return current.gameObject;
    }

}
