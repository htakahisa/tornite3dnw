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
            // �^�O��ύX
            gameObject.layer = LayerMask.NameToLayer("Me");
            transform.parent.gameObject.tag = "Me";
        } else {
            // ���݂̃I�u�W�F�N�g�̐e���擾
            Transform parent = transform.parent;
            gameObject.layer = LayerMask.NameToLayer("Enemy");
            if (parent != null) {
                // �e�̎q�I�u�W�F�N�g�����ׂĎ擾
                foreach (Transform sibling in parent) {

                    // "MapIcon"�Ƃ������O�̃I�u�W�F�N�g��T��
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
