using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Diable : MonoBehaviourPun {


   

    private float time = 0;

    Color green;

    // Start is called before the first frame update
    void Awake() {
        gameObject.SetActive(true);
        StartCoroutine(ScaleWidthOverTime());
        
        green = Color.green;
        green.a = 0.5f;
    }

    // Update is called once per frame
    void Update() {

    }



    void OnTriggerStay(Collider other) {

        PhotonView targetPhotonView = other.gameObject.GetComponent<PhotonView>();

        if (targetPhotonView != null)
        {
            return;
        }


        if (other.CompareTag("Head")) {
            PlayerFlashEffect.pfe.Stun(green);
            time += Time.deltaTime;
            if (time >= 0.5) {
                Damage(other.gameObject);
                time = 0;
            }
        }
    }


    void OnTriggerExit(Collider other) {
        PhotonView targetPhotonView = other.gameObject.GetComponent<PhotonView>();

        if (targetPhotonView != null)
        {
            return;
        }

        if (other.CompareTag("Head")) {

            PlayerFlashEffect.pfe.Distun();

        }



    }

    public void Damage(GameObject target) {

        DamageManager dm = new DamageManager();
        dm.causeDamage(target.transform.gameObject, 2);

    }

    IEnumerator ScaleWidthOverTime() {
    
        while (true) {
            Vector3 scale = transform.localScale;
            scale.x += 0.15f;
            scale.z += 0.15f;
            transform.localScale = scale;
            yield return new WaitForSeconds(0.1f); // 0.1•b‘Ò‚Â
        }
    }
}
