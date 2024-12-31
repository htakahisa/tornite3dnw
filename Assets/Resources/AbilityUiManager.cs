using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class AbilityUiManager : MonoBehaviour
{

    // TextMeshProオブジェクトをアタッチ
    public TextMeshProUGUI TacticalText;
    // TextMeshProオブジェクトをアタッチ
    public TextMeshProUGUI BattleText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleText != null)
        {
            BattleText.text = Ability.ability.number1.ToString();
        }
        if (TacticalText != null)
        {
            TacticalText.text = Ability.ability.number2.ToString();
        }
        else
        {
            Debug.LogWarning("TextMeshProUGUIがアタッチされていません。");
        }
    }
}
