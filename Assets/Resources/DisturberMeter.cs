using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisturberMeter : MonoBehaviour
{

    public GameObject meter;
    private Slider meterslider;
         
    // Start is called before the first frame update
    void Start()
    {
        meterslider = meter.GetComponent<Slider>();
        meter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Defuse(float DefuseTime)
    {
        meter.SetActive(true);
        meterslider.value = (4 - DefuseTime) / 4;
        
    }
    public void Plant(float PlantTime)
    {
        meter.SetActive(true);
        meterslider.value = (5 - PlantTime) / 5;
        
    }

    public void Collect(float CollectTime, float AllTime)
    {
        meter.SetActive(true);
        meterslider.value =  CollectTime / AllTime;

    }

    public void MeterInactive()
    {
        meter.SetActive(false);
    }
}
