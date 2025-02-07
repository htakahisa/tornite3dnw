using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class JawKha
{

    float rate = 0.09f;
    int damage = 21;
    int headdamage = 50;
    int magazine = 13;
    bool auto = true;
    float reloadtime = 0.7f;
    bool zoomable = true;
    float zoomratio = 80;
    bool accuracy = true;
    float YRecoil = 1f;
    float XRecoil = 0.7f;
    float punch = 4f;
    int Burst = 1;
    float BurstRate = 0.1f;
    float PeekingSpeed = 0f;

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
    public int GetBurst()
    {
        return Burst;
    }

    public float GetBurstRate()
    {
        return BurstRate;
    }

    public float GetPeekingSpeed()
    {
        return PeekingSpeed;
    }
    public void GetStatus()
    {

    }




}
