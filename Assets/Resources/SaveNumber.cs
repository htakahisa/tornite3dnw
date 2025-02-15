using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNumber
{


    public void SaveScore(string type, string name, string score)
    {
        if (type == "string")
        {
            PlayerPrefs.SetString(name, score);
        }
        else if (type == "float")
        {
            PlayerPrefs.SetFloat(name, float.Parse(score));
        }
        else if (type == "int")
        {
            PlayerPrefs.SetFloat(name, int.Parse(score));
        }
        PlayerPrefs.Save();
    }

    public float GetHeadRatio()
    {

        return PlayerPrefs.GetInt("Headshots") / (PlayerPrefs.GetInt("Bodyshots") + PlayerPrefs.GetInt("Headshots")) * 100;
    }

}
