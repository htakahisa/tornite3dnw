using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaTrick : MonoBehaviourPunCallbacks
{
    private bool instantiatedScan; // スキャン処理のフラグ
    private GameObject instantiatedPrefab; // 生成したプレファブを保持する変数

    public float scanRadius = 3f;           // スキャン範囲
    public LayerMask hitMask;               // ヒット判定用のレイヤーマスク
    private Ability ability;

    // スキャン範囲の視覚効果
    private GameObject scanVisual;          // スキャン範囲を描画するオブジェクト
    [SerializeField] private Material scanMaterial; // 半透明の赤いマテリアル

    void Start()
    {
    }

    void Update()
    {
     
    }

    private IEnumerator Scan()
    {
        // スキャンを行う
        yield return new WaitForSeconds(1f); // スキャン前の待機時間

        // スキャン範囲内の敵を検出
        Collider[] targets = Physics.OverlapSphere(transform.position, scanRadius, hitMask);
        foreach (Collider target in targets)
        {
            Debug.Log("敵を検出: " + target.name);
            BattleData.bd.Detect("enemy\ndetected");

            if (target.GetComponentInParent<CameraController>() != null)
            {
                target.GetComponentInParent<CameraController>().Stuned(1.5f, 0.1f);
            }
        }

        // スキャン後の処理（オブジェクトを破壊するなど）
        Invoke("End", 2f);
    }

    private void End()
    {
        BattleData.bd.DetectEnd();
        DestroyScanVisual(); // スキャン範囲の視覚効果を削除
        DestroyThis();
    }

    private void OnActive()
    {
        ability = Camera.main.transform.parent.GetComponent<Ability>();

        if (ability.number2 >= 1)
        {
            // 生成したプレファブが存在しない場合に生成
            if (instantiatedPrefab == null)
            {
                ability.Spend(2, 1);
                Debug.Log($"{gameObject.name} が左クリックされました");
                instantiatedPrefab = PhotonNetwork.Instantiate("AquaSmoke", transform.position, Quaternion.identity);
            }
            else
            {
                ability.Spend(2, 1);
                PhotonNetwork.Instantiate("AquaArmer", transform.position, Quaternion.identity);
                
            }
        }
    }

    // 右クリックされたときの処理
    private void OnCancel()
    {
        // 生成したプレファブが存在する場合に削除
        if (instantiatedPrefab != null)
        {
            Debug.Log($"{gameObject.name} が右クリックされました");
            DestroySmoke();
            ability.Collect(2, 1);
        }
        else
        {
            instantiatedScan = true;
            CreateScanVisual(); // スキャン範囲の視覚効果を作成
            StartCoroutine(Scan());
        }
    }

    public void DestroySmoke()
    {
        instantiatedPrefab.GetComponent<SmokeManager>().PhotonDestroy();
        Debug.Log("Prefab destroyed");
    }

    public void DestroyThis()
    {
        PhotonNetwork.Destroy(gameObject);
        Debug.Log("Prefab destroyed");
    }

    void OnMouseOver()
    {

        if (!photonView.IsMine)
        {
            return;
        }

        // 左クリックされたとき
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnActive();
        }

        // 右クリックされたとき
        if (Input.GetMouseButtonDown(1))
        {
            OnCancel();
        }
    }

    // スキャン範囲の視覚効果を作成
    private void CreateScanVisual()
    {
        if (scanVisual != null) return; // 既に存在する場合は作成しない

        scanVisual = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        scanVisual.transform.position = transform.position;
        scanVisual.transform.localScale = new Vector3(scanRadius * 2, scanRadius * 2, scanRadius * 2); // 半径に合わせて拡大
        scanVisual.GetComponent<Renderer>().material = scanMaterial; // 半透明の赤いマテリアルを設定
        scanVisual.GetComponent<Collider>().enabled = false;         // コライダーを無効化
    }

    // スキャン範囲の視覚効果を削除
    private void DestroyScanVisual()
    {
        if (scanVisual != null)
        {
            Destroy(scanVisual);
            scanVisual = null;
        }
    }
}
