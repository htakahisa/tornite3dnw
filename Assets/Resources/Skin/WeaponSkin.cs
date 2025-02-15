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
    static public int MischiefSkinNumber = 0;
    static public int DuelistSkinNumber = 0;

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
        if (weapon.Equals("mischief"))
        {
            MischiefSkinNumber = SkinChanger.skinchanger.GetIndex();
            Debug.Log(MischiefSkinNumber);
        }
        if (weapon.Equals("duelist"))
        {
            DuelistSkinNumber = SkinChanger.skinchanger.GetIndex();
            Debug.Log(DuelistSkinNumber);
        }
        SceneManager.LoadScene("Collecting");
    }

    public int GetClassic()
    {
        return ClassicSkinNumber;
    }

    public int GetSilver() {
        return SilverSkinNumber;
    }
    public int GetNoel()
    {
        return NoelSkinNumber;
    }
    public int GetMischief()
    {
        return MischiefSkinNumber;
    }
    public int GetDuelist()
    {
        return DuelistSkinNumber;
    }

    public int GetYor()
    {
        return YorSkinNumber;
    }

   

  
 
}
