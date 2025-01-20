using Photon.Pun;
using UnityEngine;

public class Stradarts : MonoBehaviourPun
{
    private float speed = 12f;                // ダートの移動速度        
    private float scanRadius = 8f;           // スキャン範囲
    public LayerMask hitMask;                // ヒット判定用のレイヤーマスク

    private Vector3 launchDirection;         // 発射方向
    private bool isScanning = false;         // スキャン中かどうか
    private bool showSphere = false;         // 球の表示フラグ
    private Material sphereMaterial;         // 球のマテリアル
    private GameObject materialmanager;

    void Start()
    {
        if (photonView.IsMine)
        {
            if(materialmanager == null)
            {
                materialmanager = GameObject.Find("materialmanager");
            }
            // 発射方向を設定
            launchDirection = transform.forward;

            // マテリアルのセットアップ (カスタムダブルサイドシェーダーを使用)
            sphereMaterial = new Material(materialmanager.GetComponent<MeshRenderer>().material);

            
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // ダートを移動させる
            if (!isScanning)
            {
                float distanceToMove = speed * Time.deltaTime;
                RaycastHit hit;

                // レイを飛ばして壁に当たるか確認
                if (Physics.Raycast(transform.position, launchDirection, out hit, distanceToMove))
                {
                    // 壁に当たった場合、スキャンを開始
                    isScanning = true;
                    transform.position = hit.point; // 壁の位置で止まる
                    showSphere = true; // 球を表示
                    // スキャンを開始
                    StartCoroutine(Scan());
                }
                else
                {
                    // 壁に当たらない場合は移動する
                    transform.position += launchDirection * distanceToMove;
                }
            }
        }
    }

    private System.Collections.IEnumerator Scan()
    {
        // スキャンを行う
        yield return new WaitForSeconds(2f); // スキャン前の待機時間
                                             // 球の非表示フラグをセット
        showSphere = false;
        // スキャン範囲内の敵を検出
        Collider[] targets = Physics.OverlapSphere(transform.position, scanRadius, hitMask);
        foreach (Collider target in targets)
        {
            Debug.Log("敵を検出: " + target.name);
            BattleData.bd.Detect("enemy\ndetected");
            // 敵に対する処理をここに追加
            ScanCamera.sc.ActiveScan();
        }

       

        // スキャン後の処理（オブジェクトを破壊するなど）
        Invoke("DestroyPun", 2f); // ダートを破壊
    }

    private void DestroyPun()
    {
      
        BattleData.bd.DetectEnd();
        ScanCamera.sc.InActiveScan();
        PhotonNetwork.Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        // デバッグ用のスキャン範囲を可視化
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }

    void OnRenderObject()
    {
        // ゲーム中に球を描画
        if (showSphere && sphereMaterial != null)
        {
            Graphics.DrawMesh(
                MeshBuilder.CreateSphere(20, 20), // 球メッシュの作成
                Matrix4x4.TRS(transform.position, Quaternion.identity, Vector3.one * scanRadius), // 正確なスケール
                sphereMaterial,
                0
            );
        }
    }
}

// ヘルパークラス：球メッシュを作成
public static class MeshBuilder
{
    public static Mesh CreateSphere(int longitude, int latitude)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(longitude + 1) * (latitude + 1)];
        int[] triangles = new int[longitude * latitude * 6];
        float pi = Mathf.PI;
        float twoPi = pi * 2;

        for (int lat = 0; lat <= latitude; lat++)
        {
            float a1 = pi * lat / latitude;
            float sin1 = Mathf.Sin(a1);
            float cos1 = Mathf.Cos(a1);

            for (int lon = 0; lon <= longitude; lon++)
            {
                float a2 = twoPi * lon / longitude;
                float sin2 = Mathf.Sin(a2);
                float cos2 = Mathf.Cos(a2);

                vertices[lat * (longitude + 1) + lon] = new Vector3(sin1 * cos2, cos1, sin1 * sin2);
            }
        }

        int index = 0;
        for (int lat = 0; lat < latitude; lat++)
        {
            for (int lon = 0; lon < longitude; lon++)
            {
                int current = lat * (longitude + 1) + lon;
                int next = current + longitude + 1;

                triangles[index++] = current;
                triangles[index++] = next;
                triangles[index++] = current + 1;

                triangles[index++] = current + 1;
                triangles[index++] = next;
                triangles[index++] = next + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }
}
