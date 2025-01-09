using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Pegasus : MonoBehaviour {

    float rate = 0.07f;
    int damage = 20;
    int headdamage = 45;
    int magazine = 15;
    bool auto = true;
    float reloadtime = 1.8f;
    bool zoomable = true;
    float zoomratio = 10;
    bool accuracy = true;
    float YRecoil = 1f;
    float XRecoil = 1f;

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

    public void GetStatus() {

    }




}
