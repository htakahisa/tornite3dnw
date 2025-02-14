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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mintext.text = "IF YOU LOSE : " + RoundManager.rm.GetNextMin();
        maxtext.text = "IF YOU WIN : " + RoundManager.rm.GetNextMax();
    }
}
