//using Photon.Pun;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class PhotonMethod : MonoBehaviourPun {

//    private PhotonView pv;

//    // Start is called before the first frame update
//    void Start() {

//    }

//    // Update is called once per frame
//    void Update() {

//    }


//    public void Method(string name) {
//        pv = gameObject.GetComponent<PhotonView>();
//        pv.RPC("name", RpcTarget.All);
//    }

//    [PunRPC]
//    public void Scene() {
//        if (SceneManager.GetActiveScene().name.Equals("battle")) {
//            SceneManager.LoadScene("battle2");
//        } else {
//            SceneManager.LoadScene("battle");
//        }
//    }
//}
