using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLog : MonoBehaviour
{
    public static BattleLog battlelog = null;
    private GameObject battlelogtext;
    private static string text = "";
    private Text blt = null;

    // Start is called before the first frame update



    void Awake()
    {
        battlelog = this;
        battlelogtext = transform.GetChild(0).gameObject;
        blt = battlelogtext.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            blt.text = text;
            battlelogtext.SetActive(!battlelogtext.activeSelf);
        }
    }

    public void ChangeText(float hp) {
        text = "前ラウンドで相手が\n受けたダメージ\n:" + (100 - hp);
    }
}
