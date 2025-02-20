using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseManager : MonoBehaviourPun {

    [SerializeField]
    GameObject buypanel;

    [SerializeField]
    GameObject wall;

    private string phase = "Buy";

    public static PhaseManager  pm = null;

    private AudioListener al;

    // Start is called before the first frame update
    void Start() {
        pm = this;
        if (MapManager.mapmanager.GetMapName() == "DuelLand")
        {
            phase = "Duel";
            al = GetComponentInParent<AudioListener>();
        }
        else
        {            
            Invoke("BuyPhase", 3f);
        }
    }
    private void Awake() {
      
      
        
    }

    // Update is called once per frame
    void Update() {
 
        if (phase == "Buy" || phase == "Duel")
        {
           

            if (Input.GetKeyDown(KeyCode.B))
            {
                if (Cursor.visible == false)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                buypanel.SetActive(!buypanel.activeSelf);
                RayController.rc.BuyPanelOpening = buypanel.activeSelf;
            }
        }

    }


        public void BuyPhase() {

        phase = "Buy";
        buypanel.SetActive(true);
        wall.SetActive(true);
        // 15ïbå„Ç…CallMethodÇåƒÇ—èoÇ∑
        Invoke(nameof(EndBuyPhase), 5.0f);

    }

    public void EndBuyPhase() {

        phase = "Battle";
        buypanel.SetActive(false);
        wall.SetActive(false);
        RayController.rc.BuyPanelOpening = buypanel.activeSelf;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    public string GetPhase() {

        return phase;

    }

}
