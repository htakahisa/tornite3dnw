using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Pegasus {

    float rate = 0.06f;
    int damage = 20;
    int headdamage = 45;
    int magazine = 20;
    bool auto = true;
    float reloadtime = 1.8f;
    bool zoomable = true;
    float zoomratio = 70;
    bool accuracy = true;
    float YRecoil = 2f;
    float XRecoil = 1.6f;
    int Burst = 5;
    float BurstingRate = 0.05f;
    float BurstRate = 0.2f;
    float PeekingSpeed = 0.04f;
    void Start() {

    }

    // Update is called once per frame
    void Update() {


    }

    public float GetRate() {
        return rate;
    }
    public int GetDamage() {
        return damage;
    }
    public int GetHeadDamage() {
        return headdamage;
    }
    public int GetMagazine() {
        return magazine;
    }
    public bool GetAuto() {
        return auto;
    }

    public float GetReloadTime() {
        return reloadtime;
    }

    public bool GetZoomAble() {
        return zoomable;
    }
    public float GetZoomRatio() {
        return zoomratio;
    }

    public bool GetUnZoomAccuracy() {
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

    public int GetBurst()
    {
        return Burst;
    }
    public float GetBurstRate()
    {
        return BurstRate;
    }

    public float GetBurstingRate()
    {
        return BurstingRate;
    }
    public void GetStatus() {

    }




}
