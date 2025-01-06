using Photon.Pun;
using UnityEngine;

public class Stradarts : MonoBehaviourPun {
    public float speed = 150f;                // ダートの移動速度        
    public float scanRadius = 7f;            // スキャン範囲
    public LayerMask hitMask;                 // ヒット判定用のレイヤーマスク

    private Vector3 launchDirection;          // 発射方向
    private bool isScanning = false;          // スキャン中かどうか

    void Start() {

        if (photonView.IsMine){
            // 発射方向を設定
            launchDirection = transform.forward;
            // スキャンを開始
            StartCoroutine(Scan());
        }
    }

    void Update() {
        if (photonView.IsMine){
            // ダートを移動させる
            if (!isScanning) {
                float distanceToMove = speed * Time.deltaTime;
                RaycastHit hit;

                // レイを飛ばして壁に当たるか確認
                if (Physics.Raycast(transform.position, launchDirection, out hit, distanceToMove)) {

                    // 壁に当たった場合、スキャンを開始
                    isScanning = true;
                    transform.position = hit.point; // 壁の位置で止まる
                } else {
                    // 壁に当たらない場合は移動する
                    transform.position += launchDirection * distanceToMove;
                }
            }
        }
    }

    private System.Collections.IEnumerator Scan() {
        // スキャンを行う
        yield return new WaitForSeconds(2f); // スキャン前の待機時間

        // スキャン範囲内の敵を検出
        Collider[] targets = Physics.OverlapSphere(transform.position, scanRadius, hitMask);
        foreach (Collider target in targets) {
            Debug.Log("敵を検出: " + target.name);
            BattleData.bd.Detect("enemy\ndetected");
            // 敵に対する処理をここに追加
            ScanCamera.sc.ActiveScan();


        }

        // スキャン後の処理（オブジェクトを破壊するなど）
        
        Invoke("DestroyPun", 2f); // ダートを破壊
    }

    private void DestroyPun() {
        BattleData.bd.DetectEnd();
        ScanCamera.sc.InActiveScan();
        PhotonNetwork.Destroy(gameObject);
    }

    void OnDrawGizmos() {
        // スキャン範囲を可視化
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}
