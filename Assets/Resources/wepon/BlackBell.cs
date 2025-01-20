using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class BlackBell {

    float rate = 0.8f;
    int damage = 200;
    int headdamage = 380;
    int magazine = 5;
    bool auto = false;
    float reloadtime = 2f;
    bool zoomable = true;
    float zoomratio = 30;
    bool accuracy = false;
    float PeekingSpeed = 0.35f;

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
