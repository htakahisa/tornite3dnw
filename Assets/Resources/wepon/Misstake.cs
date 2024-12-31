using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Misstake : MonoBehaviour {

    float rate = 0.65f;
    int damage = 50;
    int headdamage = 125;
    int magazine = 6;
    bool auto = false;
    float reloadtime = 1f;
    bool accuracy = true;

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

    public void GetStatus() {

    }




}
