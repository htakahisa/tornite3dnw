using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleData : MonoBehaviour
{
    Text battletext;
    public static BattleData bd;

    // Start is called before the first frame update
    void Start()
    {
        bd = this;
        battletext = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Detect(string text) {
        battletext.text = text;
    }

    
}
