using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CrosshairManager : MonoBehaviour {

    private static float crosshairAdjust = 150f;
    public static float crosshairsize = 1f * crosshairAdjust;
    private GameObject crosshair;
    public TMP_InputField inputField;
    private float floatValue;


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

 
    public void OnValueChanged()
    {
        // �e�L�X�g��float�ɕϊ�
        if (float.TryParse(inputField.text, out floatValue))
        {
            Debug.Log("���l�ɕϊ�����: " + floatValue);
            crosshairsize = floatValue * crosshairAdjust;
        }
        else
        {
            Debug.LogWarning("���͂������ł�: " + inputField.text);
        }
    }

}
