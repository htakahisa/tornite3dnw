using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SmokeManager : MonoBehaviourPun {

    Color color;
    private bool InSmoke = false;

    // Start is called before the first frame update
    void Start() {

    }

    private void Awake() {

        color.a = 0.5f;

        gameObject.SetActive(true);
        if (gameObject.name.Equals("WhiteSmoke(Clone)")) {
            color = Color.white;
            Invoke("Destroy", 3f);
        }
        if (gameObject.name.Equals("BlueLightSmoke(Clone)")) {
            color = Color.cyan;
            Invoke("Destroy", 10f);
        }
        if (gameObject.name.Equals("KatarinaSmoke(Clone)"))
        {
            color = Color.magenta;
            Invoke("Destroy", 1.5f);
        }
        if (gameObject.name.Equals("AquaSmoke(Clone)"))
        {
            color = new Color(0.0f, 1.0f, 0.5f);

        }


    }

    // Update is called once per frame
    void Update() {

    }


    public void Destroy()
    {

        if (InSmoke)
        {
            PlayerFlashEffect.pfe.Distun();
            InSmoke = false;
        }
        Destroy(gameObject);

    }
    public void PhotonDestroy()
    {

        if (InSmoke)
        {
            PlayerFlashEffect.pfe.Distun();
            InSmoke = false;
        }
        PhotonNetwork.Destroy(gameObject);

    }


    void OnTriggerStay(Collider other) {

        InSmoke = true;
        // 衝突したオブジェクトにPhotonViewがあるか確認
        PhotonView targetPhotonView = other.gameObject.GetComponent<PhotonView>();

        if (targetPhotonView != null) {
            // そのオブジェクトが自分のコントローラーかどうかを確認
            if (targetPhotonView.IsMine) {




                if (other.CompareTag("Head")) {

                    PlayerFlashEffect.pfe.Stun(color);



                }
            }
        }
    }

        void OnTriggerExit(Collider other) {
        InSmoke = false;
        // 衝突したオブジェクトにPhotonViewがあるか確認
        PhotonView targetPhotonView = other.gameObject.GetComponent<PhotonView>();

        if (targetPhotonView != null) {
            // そのオブジェクトが自分のコントローラーかどうかを確認
            if (targetPhotonView.IsMine) {




                if (other.CompareTag("Head")) {

                    PlayerFlashEffect.pfe.Distun();

                }
            }
        }




    }
}

