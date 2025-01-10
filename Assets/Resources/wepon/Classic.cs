using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Classic : MonoBehaviour {

    float rate = 0.15f;
    int damage = 25;
    int headdamage = 75;
    int magazine = 9;
    bool auto = false;
    float reloadtime = 0.8f;
    bool accuracy = true;
    float YRecoil = 1.5f;


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



    public void GetStatus() { 
        
    }




}
