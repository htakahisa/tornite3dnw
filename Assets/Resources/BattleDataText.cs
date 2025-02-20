using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleDataText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI headratio;

    [SerializeField]
    private TextMeshProUGUI headshots;

    [SerializeField]
    private TextMeshProUGUI Shootingwinrate;

    [SerializeField]
    private TextMeshProUGUI Whisperleviathanwinrate;

    [SerializeField]
    private TextMeshProUGUI WhisperValkyriewinrate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SaveNumber savenumber = new SaveNumber();
        headratio.text = "HS Ratio : " + savenumber.GetHeadRatio().ToString("F2") + "%";
        headshots.text = "HS Count: " + PlayerPrefs.GetInt("Headshots");
        Shootingwinrate.text = "Shooting Win Rate : " + savenumber.GetShootingWinRate().ToString("F2") + "%";
        Whisperleviathanwinrate.text = "Leviathan Win Rate In Whisper : " + savenumber.GetLeviathanWinRate("Whisper").ToString("F2") + "%";
        WhisperValkyriewinrate.text = "Valkyrie Win Rate In Whisper : " + savenumber.GetValkyrieWinRate("Whisper").ToString("F2") + "%";
    }
    




}
