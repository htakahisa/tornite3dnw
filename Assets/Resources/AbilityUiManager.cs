using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class AbilityUiManager : MonoBehaviour
{

    // TextMeshPro�I�u�W�F�N�g���A�^�b�`
    public TextMeshProUGUI TacticalText;
    // TextMeshPro�I�u�W�F�N�g���A�^�b�`
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
            Debug.LogWarning("TextMeshProUGUI���A�^�b�`����Ă��܂���B");
        }
    }
}
