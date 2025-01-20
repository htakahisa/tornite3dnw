using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class mischief {

    float rate = 0.085f;
    int damage = 35;
    int headdamage = 140;
    int magazine = 25;
    bool auto = true;
    float reloadtime = 1.2f;
    bool zoomable = true;
    float zoomratio = 50;
    bool accuracy = true;
    float YRecoil = 0.6f;
    float XRecoil = 0.2f;
    float PeekingSpeed = 0.08f;
    float punch = 3f;

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
    public float GetPunch()
    {
        return punch;
    }

    public void GetStatus() {

    }




}
