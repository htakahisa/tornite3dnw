using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Stella : MonoBehaviour {

    float rate = 0.09f;
    int damage = 45;
    int headdamage = 100;
    int magazine = 17;
    bool auto = true;
    float reloadtime = 1.5f;
    bool zoomable = true;
    float zoomratio = 15;
    bool accuracy = true;
    float YRecoil = 1.5f;
    float XRecoil = 0.5f;
    int Burst = 3;
    float BurstRate = 0.3f;

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

    public void GetStatus() {

    }




}
