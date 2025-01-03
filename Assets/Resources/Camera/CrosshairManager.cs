using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CrosshairManager : MonoBehaviour {

    public static float crosshairsize;
    private GameObject customizebar;
    private GameObject crosshair;
    Slider slider;


    // Start is called before the first frame update
    void Awake() {


            crosshair = GameObject.FindGameObjectWithTag("Crosshair");
       
    }

    // Update is called once per frame
    void Update() {
       
        if (crosshair != null) {
            crosshair.transform.localScale = new Vector3(crosshairsize, crosshairsize, crosshairsize);
        }
    }

    public void DotSizeChange() {
        customizebar = GameObject.FindGameObjectWithTag("CrosshairBar");
        slider = customizebar.GetComponent<Slider>();
        crosshairsize = slider.value * 150;
    }

}
