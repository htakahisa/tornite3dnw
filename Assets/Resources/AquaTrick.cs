using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaTrick : MonoBehaviourPunCallbacks
{

    private bool instantiatedScan; // 生成したプレファブを保持する変数
    private GameObject instantiatedPrefab; // 生成したプレファブを保持する変数
                    
    public float scanRadius = 3f;            // スキャン範囲
    public LayerMask hitMask;                 // ヒット判定用のレイヤーマスク
    private Ability ability;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private System.Collections.IEnumerator Scan()
    {
        // スキャンを行う
        yield return new WaitForSeconds(1f); // スキャン前の待機時間

        // スキャン範囲内の敵を検出
        Collider[] targets = Physics.OverlapSphere(transform.position, scanRadius, hitMask);
        foreach (Collider target in targets)
        {
            Debug.Log("敵を検出: " + target.name);
            BattleData.bd.Detect("enemy\ndetected");

            target.GetComponentInParent<CameraController>().Stuned(1.5f, 0.1f);

        }

        // スキャン後の処理（オブジェクトを破壊するなど）
        Invoke("End", 2f); // スキャン機能を破壊
    }
    private void End()
    {
        BattleData.bd.DetectEnd();
    }

     private void OnActive() {
        ability = Camera.main.transform.parent.GetComponent<Ability>();

        if (ability.number2 >= 1) { 
        

            // 生成したプレファブが存在しない場合に生成
            if (instantiatedPrefab == null)
            {
                ability.Spend(2,1);
                Debug.Log($"{gameObject.name} が左クリックされました");
                instantiatedPrefab = PhotonNetwork.Instantiate("AquaSmoke", transform.position, Quaternion.identity);

            }
            else if (!instantiatedScan)
            {


                ability.Spend(2, 1);
                instantiatedScan = true;
                StartCoroutine(Scan());


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
       
        DestroyPrefab();
        ability.Collect(2, 1);

        } else { 
               
            Debug.Log("Prefab already destroyed or not instantiated");

        }

    }

    public void DestroyPrefab()
    {
        PhotonNetwork.Destroy(gameObject);
        instantiatedPrefab.GetComponent<SmokeManager>().PhotonDestroy();
            Debug.Log("Prefab destroyed");
        
    }


        void OnMouseOver() {

        // 右クリックにつき回収
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnActive();
        }

        // 右クリックにつき回収
        if (Input.GetMouseButtonDown(1))
        {
            OnCancel();
        }
    }
}
