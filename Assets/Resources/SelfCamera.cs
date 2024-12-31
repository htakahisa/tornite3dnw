using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfCamera : MonoBehaviour
{


    private PhotonView photonView;

    // Start is called before the first frame update
    void Start() {
        photonView = GetComponent<PhotonView>();


      
    }

    // Update is called once per frame
    void Update() {
        if (gameObject.layer.Equals("Enemy") || (transform.parent.gameObject.tag.Equals("Me"))) {
            return;
        }

        if (photonView.IsMine) {
            // タグを変更
            gameObject.layer = LayerMask.NameToLayer("Me");
            transform.parent.gameObject.tag = "Me";
        } else {
            // 現在のオブジェクトの親を取得
            Transform parent = transform.parent;
            gameObject.layer = LayerMask.NameToLayer("Enemy");
            if (parent != null) {
                // 親の子オブジェクトをすべて取得
                foreach (Transform sibling in parent) {

                    // "MapIcon"という名前のオブジェクトを探す
                    if (sibling.name == "MapIcon") {
                        sibling.gameObject.layer = LayerMask.NameToLayer("EnemyIcon");
                    }

                    if (sibling.name == "Head") {
                        sibling.gameObject.layer = LayerMask.NameToLayer("Target");
                    }
                    if (sibling.name == "Body") {
                        sibling.gameObject.layer = LayerMask.NameToLayer("Target");
                    }
                }
            }

        }
    }
}
