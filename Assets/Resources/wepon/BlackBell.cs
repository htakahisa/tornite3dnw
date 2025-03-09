using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class BlackBell {

    float rate = 1.5f;
    int damage = 100;
    int headdamage = 150;
    int magazine = 50000;
    bool auto = false;
    float reloadtime = 0f;
    bool zoomable = false;
    float zoomratio = 30;
    bool accuracy = true;
    float PeekingSpeed = 0.08f;
    float RecoilDuration = 0.08f;
    float punch = 1.5f;

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
    public float GetRecoilDuration()
    {
        return RecoilDuration;
    }
    public void GetStatus() {

    }




}
