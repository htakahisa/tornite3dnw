using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleDataText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI headratio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SaveNumber savenumber = new SaveNumber();
        headratio.text = "HS-Ratio : " + savenumber.GetHeadRatio() + "%";
    }
    




}
