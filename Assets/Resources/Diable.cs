using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Diable : MonoBehaviourPun {


   

    private float time = 0;



    // Start is called before the first frame update
    void Awake() {
        gameObject.SetActive(true);
        StartCoroutine(ScaleWidthOverTime());
        

       
    }

    // Update is called once per frame
    void Update() {

    }



    private void OnTriggerStay(Collider other) {

        PhotonView targetPhotonView = other.gameObject.GetComponent<PhotonView>();
 
        if (targetPhotonView == null || !targetPhotonView.IsMine)
        {
            return;
        }
      

        if (other.CompareTag("Head")) {
            CameraController cc = other.gameObject.GetComponentInParent<CameraController>();
            AudioListener al = Camera.main.GetComponent<AudioListener>();
            time += Time.deltaTime;
            if (time >= 0.5) {
                Damage(other.gameObject);
                time = 0;
            }
            cc.servicespeed = 0.8f;
            al.enabled = false;
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        PhotonView targetPhotonView = other.gameObject.GetComponent<PhotonView>();

        if (targetPhotonView == null || !targetPhotonView.IsMine)
        {
            return;
        }
        if (other.CompareTag("Head"))
        {
            CameraController cc = other.gameObject.GetComponentInParent<CameraController>();
            AudioListener al = Camera.main.GetComponent<AudioListener>();
            cc.servicespeed = 1f;
            al.enabled = true;
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
