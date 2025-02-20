using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArteBomb : MonoBehaviourPun
{

    private Material sphereMaterial;
    private GameObject materialmanager;

    private HashSet<GameObject> damagedParents = new HashSet<GameObject>(); // ダメージを与えた親オブジェクトを追跡

    public float radii = 1f; // 爆発の半径
    public int damages = 10; // 爆発のダメージ
    private bool drawingradii = false;

    private bool HasExplode = false;



    // Start is called before the first frame update
    void Awake()
    {
     

        if (materialmanager == null)
        {
            materialmanager = MaterialManager.mm.gameObject;
        }
        // マテリアルのセットアップ (カスタムダブルサイドシェーダーを使用)
        sphereMaterial = new Material(materialmanager.GetComponent<MeshRenderer>().materials[1]);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (HasExplode)
        {
            return;
        }
        if (collision.gameObject.tag == "Arte")
        {
            return;
        }
        if (HasExplode)
        {
            return;
        }

        HasExplode = true;
        StartCoroutine(PerformExplosion(radii, damages));
    }


    // 爆発を処理するメソッド
    private IEnumerator PerformExplosion(float radius, int damage)
    {

        yield return new WaitForSeconds(0.5f);
        drawingradii = true;
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
        
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (drawingradii)
        {
            DrawExplosionRange(radii);
        }

    }

        private void DrawExplosionRange(float radius)
    {


        // スケールを反映した行列を作成
        Matrix4x4 matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, Vector3.one * radius * 2);
        Graphics.DrawMesh(CreateSphereMesh(), matrix, sphereMaterial, 0);
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
      
            Gizmos.DrawWireSphere(transform.position, radii);
        
    }
}
