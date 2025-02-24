using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Rapetter
{

    float rate = 0.075f;
    int damage = 50;
    int headdamage = 60;
    int magazine = 100;
    bool auto = true;
    float reloadtime = 5f;
    bool zoomable = true;
    float zoomratio = 45;
    bool accuracy = false;
    float YRecoil = 0.5f;
    float XRecoil = 0.7f;
    float PeekingSpeed = 0.8f;
    float RecoilDuration = 0.25f;

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

    public float GetPeekingSpeed()
    {
        return PeekingSpeed;
    }

    public float GetRecoilDuration()
    {
        return RecoilDuration;
    }
    public void GetStatus()
    {

    }




}
