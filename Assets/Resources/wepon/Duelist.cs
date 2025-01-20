using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Duelist{

    float rate = 1;
    int damage = 150;
    int headdamage = 200;
    int magazine = 2;
    bool auto = false;
    float reloadtime = 3f;
    bool zoomable = true;
    float zoomratio = 40;
    bool accuracy = false;
    float PeekingSpeed = 1f;
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
