using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SmokeManager : MonoBehaviourPun {

   
  

    // Start is called before the first frame update
    void Start() {

    }

    private void Awake() {

       

        gameObject.SetActive(true);
        if (gameObject.name.Equals("WhiteSmoke(Clone)")) {
       
            Invoke("Destroy", 4f);
        }
        if (gameObject.name.Equals("BlueLightSmoke(Clone)")) {
       
            Invoke("Destroy", 15f);
        }
        if (gameObject.name.Equals("KatarinaSmoke(Clone)"))
        {
           
            Invoke("Destroy", 2f);
        }
        if (gameObject.name.Equals("AquaSmoke(Clone)"))
        {
            

        }


    }

    public void Destroy()
    {
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update() {

    }



    public void PhotonDestroy()
    {

      
        PhotonNetwork.Destroy(gameObject);

    }




  




    
}

