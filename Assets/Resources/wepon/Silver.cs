using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Silver {

    float rate = 0.3f;
    int damage = 60;
    int headdamage = 163;
    int magazine = 6;
    bool auto = false;
    float reloadtime = 0.5f;
    bool accuracy = true;
    float YRecoil = 4f;
    float RecoilDuration = 0.04f;

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

    public float GetRecoilDuration()
    {
        return RecoilDuration;
    }
    public void GetStatus() {

    }




}
