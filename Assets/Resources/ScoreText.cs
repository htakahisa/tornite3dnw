using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MapManager.mapmanager.GetMapName() == "DuelLand")
        {
            return;
        }
        text.text = RoundManager.rm.GetScoreText();
    }
}
