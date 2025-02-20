using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SensityvityController : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_InputField inputField;
    private float floatValue;
    // Update is called once per frame
    void Start()
    {

        Camera.main.GetComponent<CameraController>().SensityvityChange(PlayerPrefs.GetFloat("Crosshairsize"));

    }
    void Update() {

    }

    public void UpdateActive() {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void SensityvityControl() {
        // テキストをfloatに変換
        if (float.TryParse(inputField.text, out floatValue))
        {
            
            float sensi = floatValue;
            Camera.main.GetComponent<CameraController>().SensityvityChange(sensi);
            PlayerPrefs.SetFloat("Sensitivity", floatValue);
        }
    }



}
