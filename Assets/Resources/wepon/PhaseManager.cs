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

    // Start is called before the first frame update
    void Start() {

        Invoke("BuyPhase", 3f);
    }
    private void Awake() {
        pm = this;
    }

    // Update is called once per frame
    void Update() {
        if (phase == "Buy")
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

            }
        }

    }


        public void BuyPhase() {

        phase = "Buy";
        buypanel.SetActive(true);
        wall.SetActive(true);
        // 15ïbå„Ç…CallMethodÇåƒÇ—èoÇ∑
        Invoke(nameof(EndBuyPhase), 15.0f);

    }

    public void EndBuyPhase() {

        phase = "Battle";
        buypanel.SetActive(false);
        wall.SetActive(false);

    }

    public string GetPhase() {

        return phase;

    }

}
