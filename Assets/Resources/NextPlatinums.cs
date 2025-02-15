using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextPlatinums : MonoBehaviour
{
    [SerializeField]
    Text mintext;
    [SerializeField]
    Text maxtext;
    [SerializeField]
    Text moneytext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mintext.text = "IF YOU LOSE : " + RoundManager.rm.GetNextMin();
        maxtext.text = "IF YOU WIN : " + RoundManager.rm.GetNextMax();
        moneytext.text = "PLATINUMS : " + RoundManager.rm.GetMoney();
    }
}
