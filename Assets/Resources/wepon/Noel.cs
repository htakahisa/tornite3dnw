using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Noel : ScriptableObject
{

    float rate = 0.09f;
    int damage = 40;
    int headdamage = 150;
    int magazine = 30;
    bool auto = true;
    float reloadtime = 1.5f;
    bool zoomable = true;
    float zoomratio = 50;
    bool accuracy = true;
    float YRecoil = 0.4f;
    float XRecoil = 0.3f;
    float PeekingSpeed = 0.03f;
    float punch = 2f;
    float RecoilDuration = 0.15f;

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
    public void GetStatus() {

    }




}
