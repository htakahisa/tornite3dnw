using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CrosshairManager : MonoBehaviour {

    private static float crosshairAdjust = 150f;
    public static float crosshairsize = 1f * crosshairAdjust;
    public TMP_InputField inputField;
    private float floatValue;
    public static CrosshairManager crshrM;

    // Start is called before the first frame update
    void Awake() {
        crshrM = this;
        floatValue = PlayerPrefs.GetFloat("Crosshairsize");
        crosshairsize = floatValue * crosshairAdjust;
    }

    // Update is called once per frame
    void Update() {
       
        
         
        
    }

    public float GetSize()
    {
        return crosshairsize;
    }
 
    public void OnValueChanged()
    {
        // �e�L�X�g��float�ɕϊ�
        if (float.TryParse(inputField.text, out floatValue))
        {
            Debug.Log("���l�ɕϊ�����: " + floatValue);
            crosshairsize = floatValue * crosshairAdjust;

            PlayerPrefs.SetFloat("Crosshairsize", floatValue);
        }
        else
        {
            Debug.LogWarning("���͂������ł�: " + inputField.text);
        }
    }

}
