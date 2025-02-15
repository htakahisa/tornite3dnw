using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMeter : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    public static AbilityMeter abilitymeter;

    // Start is called before the first frame update
    void Awake()
    {
        abilitymeter = this;
        slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(int value)
    {
        slider.value = value;
    }

}
