using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNumber
{

    public float GetHeadRatio()
    {
        return ((PlayerPrefs.GetInt("Headshots") * 100.00f) / (PlayerPrefs.GetInt("Bodyshots") + PlayerPrefs.GetInt("Headshots")));
    }

    public float GetShootingWinRate()
    {
        return ((PlayerPrefs.GetInt("Shootinglose") * 100.00f) / (PlayerPrefs.GetInt("Shootinglose") + PlayerPrefs.GetInt("Shootingwin")));
    }

    public float GetLeviathanWinRate(string map)
    {
        return ((PlayerPrefs.GetInt(map+ "LeviathanWin") * 100.00f) / (PlayerPrefs.GetInt(map+ "LeviathanWin") + PlayerPrefs.GetInt(map+ "ValkyrieWin")));
    }

    public float GetValkyrieWinRate(string map)
    {
        Debug.Log(PlayerPrefs.GetInt("WhisperValkyrieWin"));
        return ((PlayerPrefs.GetInt(map+ "ValkyrieWin") * 100.00f) / (PlayerPrefs.GetInt(map+ "ValkyrieWin") + PlayerPrefs.GetInt(map+ "LeviathanWin")));
    }

}
