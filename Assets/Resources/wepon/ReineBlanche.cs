using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class ReineBlanche {

    float rate = 0.8f;
    int damage = 105;
    int headdamage = 155;
    int magazine = 6;
    bool auto = false;
    float reloadtime = 2.5f;
    bool zoomable = true;
    float zoomratio = 30;
    bool accuracy = true;
    float PeekingSpeed = 0.05f;
    float YRecoil = 3.5f;
    float punch = 2f;
    float RecoilDuration = 0.05f;

    void Start() {

    }

    // Update is called once per frame
    void Update() {


    }

    public float GetPunch()
    {
        return punch;
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
    public float GetPeekingSpeed()
    {
        return PeekingSpeed;
    }

    public float GetYRecoil()
    {
        return YRecoil;
    }

    public float GetRecoilDuration()
    {
        return RecoilDuration;
    }
    public void GetStatus() {

    }




}
