using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

public class Arte : MonoBehaviourPun
{
    public float[] radii = { 3f, 4f, 5f }; // 爆発の半径
    public int[] damages = { 30, 20, 10 }; // 爆発のダメージ
    public float explosionDelay = 0.5f; // 爆発間の間隔

    private float throwForce = 8f; // 投げる力

    Rigidbody rb;

    private SoundManager sm;

    private bool HasExplode = false;

    private HashSet<GameObject> damagedParents = new HashSet<GameObject>(); // ダメージを与えた親オブジェクトを追跡

    private bool showExplosionRange = false; // 爆発範囲を表示するフラグ
    private int currentExplosionIndex = -1; // 現在の爆発インデックス

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        sm = Camera.main.transform.parent.GetComponent<SoundManager>();
        if (photonView.IsMine)
        {
            // グレネードに力を加える
            Vector3 throwDirection = transform.forward * 2;
            rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
        }
    }

        private void OnCollisionEnter(Collision collision)
    {
        if (HasExplode)
        {
            return;
        }

        HasExplode = true;
        showExplosionRange = true; // 爆発範囲の表示を開始
        Invoke("StartExplosionAttack", 1f);
    }

    // 爆発攻撃を開始するメソッド
    public void StartExplosionAttack()
    {
        StartCoroutine(ExplosionSequence());
    }


    // 爆発を順番に処理するコルーチン
    private IEnumerator ExplosionSequence()
    {
        for (int i = 0; i < radii.Length; i++)
        {
            currentExplosionIndex = i; // 現在の爆発インデックスを設定
            PerformExplosion(radii[i], damages[i]);
            yield return new WaitForSeconds(explosionDelay);
        }
        showExplosionRange = false; // 爆発範囲の表示を終了
        Destroy(gameObject);
    }

    // 爆発を処理するメソッド
    private void PerformExplosion(float radius, int damage)
    {
        DamageManager damagemanager = new DamageManager();

        // 現在の爆発の情報をデバッグログに表示
        Debug.Log($"Explosion at radius {radius}m with {damage} damage.");

        // 爆発範囲内のコライダーを取得
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            GameObject parentObject = hitCollider.transform.root.gameObject; // 親オブジェクトを取得

            if ((hitCollider.gameObject.tag == "Body" || hitCollider.gameObject.tag == "Head") && !damagedParents.Contains(parentObject))
            {
                damagemanager.causeDamage(hitCollider.gameObject, damage);
                damagedParents.Add(parentObject); // ダメージを与えた親オブジェクトを記録
                Debug.Log(damage);
            }
        }

        // 爆発エフェクトなどを追加可能
        // Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        // 爆発範囲をランタイム中に表示
        if (showExplosionRange && currentExplosionIndex >= 0 && currentExplosionIndex < radii.Length)
        {
            DrawExplosionRange(radii[currentExplosionIndex]);
        }
    }

    private void DrawExplosionRange(float radius)
    {
        // 爆発範囲を球として描画
        Material mat = new Material(Shader.Find("Unlit/Color"));
        mat.color = new Color(1f, 0f, 0f, 0.3f); // 半透明の赤色

        // スケールを反映した行列を作成
        Matrix4x4 matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, Vector3.one * radius * 2);
        Graphics.DrawMesh(CreateSphereMesh(), matrix, mat, 0);
    }

    private Mesh CreateSphereMesh()
    {
        GameObject tempSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Mesh sphereMesh = tempSphere.GetComponent<MeshFilter>().sharedMesh;
        Destroy(tempSphere);
        return sphereMesh;
    }

    private void OnDrawGizmos()
    {
        // シーンビューで爆発の範囲を視覚的に確認
        Gizmos.color = Color.red;
        foreach (float radius in radii)
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
