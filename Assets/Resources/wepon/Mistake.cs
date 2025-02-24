using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Mistake {

    float rate = 0.55f;
    int damage = 50;
    int headdamage = 125;
    int magazine = 6;
    bool auto = false;
    float reloadtime = 0.9f;
    bool accuracy = true;
    float YRecoil = 5f;
    float punch = 4f;
    float RecoilDuration = 0.06f;

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

    public bool GetUnZoomAccuracy() {
        return accuracy;
    }

    public float GetYRecoil()
    {
        return YRecoil;
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
