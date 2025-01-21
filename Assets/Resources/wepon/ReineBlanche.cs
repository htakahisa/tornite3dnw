using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class ReineBlanche {

    float rate = 0.7f;
    int damage = 75;
    int headdamage = 155;
    int magazine = 6;
    bool auto = false;
    float reloadtime = 2.5f;
    bool zoomable = true;
    float zoomratio = 50;
    bool accuracy = true;
    float PeekingSpeed = 0.3f;
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
    public float GetPeekingSpeed()
    {
        return PeekingSpeed;
    }

    public void GetStatus() {

    }




}
