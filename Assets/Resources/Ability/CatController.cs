using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    private float stepTimer = 1.1f;

    public float attackDistance = 0.3f; // 攻撃を行う距離（ここでは0に近い距離）
    private bool antirugcheck = false;
    //private CatEffect catEffect;

    private float flashDuration = 2.5f; // フラッシュの持続時間

    private SoundManager sm;
    private int randomValue = 0;

    private bool HasDetected = false;

    private Vector3 targetpos;

    private GameObject enemy;

    void Start()
    {
        enemy = EnemyTag.enemytag.gameObject;
        sm = Camera.main.transform.parent.GetComponent<SoundManager>();

        
        if (enemy != null)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(targetpos);
        }

        Invoke("Destroy", 20f);
    }

    void Update()
    {
        
        // 鳴き声を再生
        if (stepTimer >= 1.0f + randomValue * 2.5f)
        {
            randomValue = Random.Range(1, 5);
            sm.PlayMySound("cat" + randomValue);
            stepTimer = 0f;

            agent.speed = randomValue * 2;
        }

        if (target != null && agent != null)
        {
            stepTimer += Time.deltaTime;

            target = enemy.transform;

            // ターゲットとの距離を計算
            float distance = Vector3.Distance(transform.position, target.position);
            Debug.Log(distance);
            if (distance <= 3)
            {
                HasDetected = true;
            }

            if (HasDetected)
            {
                agent.SetDestination(target.position);
            }

            // 攻撃距離以内の場合
            if (distance <= attackDistance)
            {
                Hug();
            }
        }

    }

    public void SetTarget(Vector3 position)
    {
        targetpos = position;
    }

    private void Hug()
    {

        if (antirugcheck)
        {
            return;
        }

        antirugcheck = true;

        CatEffect targetCatEffect = target.GetComponentInChildren<CatEffect>();

        if (targetCatEffect != null)
        {
            // 相手の CatEffect に対してエフェクトを有効化
            targetCatEffect.ApplyFlash(flashDuration);
            Destroy();
        }
    }


    private void Destroy()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnDestroy()
    {
        PhotonNetwork.Instantiate("CatCollect", transform.position, Quaternion.identity);
    }

}
