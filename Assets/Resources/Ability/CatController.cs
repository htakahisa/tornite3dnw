using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    private float stepTimer = 1.1f;

    //private float attackDistance = 0.2f;
    //private CatEffect catEffect;

    private float flashDuration = 2.5f; // フラッシュの持続時間

    private SoundManager sm;
    private int randomValue = 0;

    void Start()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Player");
        sm = Camera.main.transform.parent.GetComponent<SoundManager>();


        if (enemy != null)
        {
            agent = GetComponent<NavMeshAgent>();
            target = enemy.transform;

        }

        Invoke("Destroy", 10f);
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

            // ターゲットに向かって移動
            agent.SetDestination(target.position);

            //// ターゲットとの距離を計算
            //float distance = Vector3.Distance(transform.position, target.position);

            
            //// 攻撃距離以内の場合
            //if (distance <= attackDistance)
            //{
            //    //Hug();
            //}
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.tag.Equals("Me")) {
            return;
        }

        // 衝突相手の GameObject から CatEffect を取得
        Transform mainCamera = collision.gameObject.transform.Find("Main Camera");
        if (mainCamera == null) {
            return;
        }

        CatEffect targetCatEffect = mainCamera.Find("Canvas/Cat").GetComponent<CatEffect>();

        if (targetCatEffect != null)
        {
            // 相手の CatEffect に対してエフェクトを有効化
            targetCatEffect.ApplyFlash(flashDuration);
            PhotonNetwork.Destroy(gameObject);
        }
    }


    private void Destroy()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
