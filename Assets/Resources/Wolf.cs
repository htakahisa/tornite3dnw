using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class Wolf : MonoBehaviourPun {
    private Transform target;  // プレイヤーなどのターゲット
    private NavMeshAgent agent;
    public float attackDistance = 0.1f; // 攻撃を行う距離（ここでは0に近い距離）
    private bool antirugcheck = false;
    private float stepTimer = 0f;

    private SoundManager sm;

    void Start() {
        GameObject enemy = GameObject.FindGameObjectWithTag("Player");
        sm = Camera.main.transform.parent.GetComponent<SoundManager>();
        if (enemy != null) {
            // NavMeshAgentコンポーネントの取得
            agent = GetComponent<NavMeshAgent>();
            target = enemy.transform;  // ターゲットを設定
        }

        Invoke("Destroy", 10f);

    }

    void Update() {
        // ターゲットの位置に向かって移動
        if (target != null) {
            stepTimer += Time.deltaTime;
            // 足音を0.5秒毎に再生
            if (stepTimer >= 0.5f) {
                sm.PlayMySound("wolf");
                stepTimer = 0f; // タイマーをリセット
            }
            agent.SetDestination(target.position);
            // ターゲットとの距離を計算
            float distance = Vector3.Distance(transform.position, target.position);

            // 距離が指定した攻撃距離以内になった場合
            if (distance <= attackDistance) {
                Damage(); // Damageメソッドを呼び出す
            }
        }
    }

    private void Destroy() {
        PhotonNetwork.Destroy(gameObject);
    }


    private void Damage() {
        if (!antirugcheck) {
            // 指定した名前の子オブジェクトを取得        
            DamageCounter dc = target.GetComponentInChildren<DamageCounter>();
            GameObject body = dc.gameObject;
            if (dc != null) {
                dc.DamageCount(50, body);
                body.GetComponentInParent<CameraController>().Stuned(1f,1);
                Destroy();
                antirugcheck = true;
            }
        }
    }
}
