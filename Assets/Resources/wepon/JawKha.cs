using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class JawKha : MonoBehaviour
{

    float rate = 0.09f;
    int damage = 21;
    int headdamage = 40;
    int magazine = 10;
    bool auto = true;
    float reloadtime = 1.5f;
    bool zoomable = false;
    float zoomratio = 1;
    bool accuracy = true;
    float YRecoil = 2.0f;
    float XRecoil = 0.7f;
    float punch = 0.1f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public float GetRate()
    {
        return rate;
    }
    public int GetDamage()
    {
        return damage;
    }
    public int GetHeadDamage()
    {
        return headdamage;
    }
    public int GetMagazine()
    {
        return magazine;
    }
    public bool GetAuto()
    {
        return auto;
    }

    public float GetReloadTime()
    {
        return reloadtime;
    }

    public bool GetZoomAble()
    {
        return zoomable;
    }
    public float GetZoomRatio()
    {
        return zoomratio;
    }

    public bool GetUnZoomAccuracy()
    {
        return accuracy;
    }

    public float GetYRecoil()
    {
        return YRecoil;
    }

    public float GetXRecoil()
    {
        return XRecoil;
    }
    public float GetPunch()
    {
        return punch;
    }
    public void GetStatus()
    {

    }




}
