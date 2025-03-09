using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Stella : ScriptableObject
{

    float rate = 0.1f;
    int damage = 42;
    int headdamage = 100;
    int magazine = 20;
    bool auto = true;
    float reloadtime = 1.5f;
    bool zoomable = true;
    float zoomratio = 65;
    bool accuracy = true;
    float YRecoil = 0.9f;
    float XRecoil = 0.25f;
    int Burst = 2;
    float BurstingRate = 0.08f;
    float BurstRate = 0.23f;
    float PeekingSpeed = 0.04f;
    float punch = 1f;
    float RecoilDuration = 0.15f;

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
    public float GetPeekingSpeed()
    {
        return PeekingSpeed;
    }
    public float GetPunch()
    {
        return punch;
    }

    public float GetRecoilDuration()
    {
        return RecoilDuration;
    }

    public void GetStatus() {

    }




}
