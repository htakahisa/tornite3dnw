using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Duelist{

    float rate = 1.5f;
    int damage = 300;
    int headdamage = 400;
    int magazine = 2;
    bool auto = false;
    float reloadtime = 3f;
    bool zoomable = true;
    float zoomratio = 20;
    bool accuracy = false;
    float PeekingSpeed = 0.07f;
    float YRecoil = 13;
    float punch = 4f;
    float RecoilDuration = 0.08f;
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
    public float GetYRecoil()
    {
        return YRecoil;
    }

    public void GetStatus() {

    }




}
