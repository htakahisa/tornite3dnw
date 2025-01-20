using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class JawKha
{

    float rate = 0.09f;
    int damage = 21;
    int headdamage = 40;
    int magazine = 13;
    bool auto = true;
    float reloadtime = 0.8f;
    bool zoomable = false;
    float zoomratio = 1;
    bool accuracy = true;
    float YRecoil = 1.8f;
    float XRecoil = 0.7f;
    float punch = 4f;

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
