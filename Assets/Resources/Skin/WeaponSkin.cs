using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSkin : MonoBehaviour
{
    public static WeaponSkin weaponSkin;
    static public int ClassicSkinNumber = 0;
    static public int SilverSkinNumber = 0;
    static public int YorSkinNumber = 0;
    static public int NoelSkinNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        weaponSkin = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Confirmed(string weapon) {
        if (weapon.Equals("classic"))
        {
            ClassicSkinNumber = SkinChanger.skinchanger.GetIndex();
            Debug.Log(ClassicSkinNumber);
        }
        if (weapon.Equals("silver")) {
            SilverSkinNumber = SkinChanger.skinchanger.GetIndex();
            Debug.Log(SilverSkinNumber);
        }
        if (weapon.Equals("yor"))
        {
            YorSkinNumber = SkinChanger.skinchanger.GetIndex();
            Debug.Log(YorSkinNumber);
        }
        if (weapon.Equals("noel"))
        {
            NoelSkinNumber = SkinChanger.skinchanger.GetIndex();
            Debug.Log(NoelSkinNumber);
        }
        SceneManager.LoadScene("Collecting");
    }


    public int GetSilver() {
        return SilverSkinNumber;
    }

    public int GetYor()
    {
        return YorSkinNumber;
    }

    public int GetNoel()
    {
        return NoelSkinNumber;
    }

    public int GetClassic()
    {
        return ClassicSkinNumber;
    }
}
