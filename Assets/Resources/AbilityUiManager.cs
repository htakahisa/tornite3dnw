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

    private Ability ability;

    // Start is called before the first frame update
    void Start()
    {
        ability = Camera.main.transform.parent.GetComponent<Ability>();
    }

    // Update is called once per frame
    void Update()
    {
        
            if (ability == null)
            {
            if (Camera.main.transform.parent != null)
            {
                ability = Camera.main.transform.parent.GetComponent<Ability>();
            }
            }

        else { 
        

            if (BattleText != null)
            {
                BattleText.text = ability.number1.ToString();
            }
            if (TacticalText != null)
            {
                TacticalText.text = ability.number2.ToString();
            }
            else
            {
                Debug.LogWarning("TextMeshProUGUIがアタッチされていません。");
            }

        }
    }
}
