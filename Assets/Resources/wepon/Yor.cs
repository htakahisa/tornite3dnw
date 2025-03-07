using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Yor {

    float rate = 0.75f;
    int damage = 125;
    int headdamage = 179;
    int magazine = 3;
    bool auto = false;
    float reloadtime = 1f;
    bool zoomable = true;
    float zoomratio = 70;
    bool accuracy = true;
    float YRecoil = 5f;
    float RecoilDuration = 0.1f;
    float PeekingSpeed = 0.02f;

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

    public float GetRecoilDuration()
    {
        return RecoilDuration;
    }

    public float GetPeekingSpeed()
    {
        return PeekingSpeed;
    }
    public void GetStatus() {

    }




}
