using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryController : MonoBehaviourPunCallbacks {

    [SerializeField] private GameObject[] ability;
    private int currentAblityIndex = 0;
    private Ability able;






    // Start is called before the first frame update
    void Awake() {


        // 最初の武器をアクティブにする
        SwitchAbility(currentAblityIndex);


    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentAblityIndex > 0) {
            currentAblityIndex -= 1;
            SwitchAbility(currentAblityIndex);

        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentAblityIndex < ability.Length - 1) {
            currentAblityIndex += 1;
            SwitchAbility(currentAblityIndex);

        }

       








              




        
    }






    void SwitchAbility(int index) {

        if (ability.Length != 0) {
            for (int i = 0; i < ability.Length; i++) {
                ability[i].SetActive(i == index);
            }
        }
    }


}
