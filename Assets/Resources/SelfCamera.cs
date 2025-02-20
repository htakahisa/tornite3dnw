using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfCamera : MonoBehaviour
{
    public GameObject MapIcon;
    public GameObject Plane;
    [SerializeField] public List<GameObject> target;
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start() {
        photonView = GetComponent<PhotonView>();


      
    }

    // Update is called once per frame
    void Update() {
        if (LayerMask.LayerToName(gameObject.layer).Equals("Enemy") || (transform.parent.gameObject.tag.Equals("Me"))) {
            return;
        }

        if (photonView.IsMine) {
            // タグを変更
            gameObject.layer = LayerMask.NameToLayer("Me");
            transform.parent.gameObject.tag = "Me";
            transform.parent.gameObject.AddComponent<MyTag>();

        } if(!photonView.IsMine || transform.parent.name == "CowAi(Clone)") {

            gameObject.layer = LayerMask.NameToLayer("Enemy");


            MapIcon.layer = LayerMask.NameToLayer("EnemyIcon");



            // リスト内の全オブジェクトのレイヤーを変更
            foreach (GameObject obj in target)
            {
                if (obj != null)
                {
                    obj.layer = LayerMask.NameToLayer("Target");
                }
            }




            Plane.layer = LayerMask.NameToLayer("Me");
            transform.parent.gameObject.AddComponent<EnemyTag>();

        }

            

    }
    
}
