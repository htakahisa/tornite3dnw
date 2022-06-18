using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SensityvityController : MonoBehaviour
{
    // Start is called before the first frame update
  

    // Update is called once per frame
     void Start()
    { 

        gameObject.SetActive(false);
      
    }
      void Update() {

    }

    public void UpdateActive() {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void SensityvityControl() {
        Slider value = gameObject.GetComponent<Slider>();
        float sensi = value.value;
        FindObjectOfType<CameraController>().SensityvityChange(sensi);
    }

       
    
}
