using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Custom : MonoBehaviour
{
    public static Custom custom;

    [SerializeField]
    private TMP_InputField Password;

    public static string password = "";

    // Start is called before the first frame update
    void Awake()
    {
        custom = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CustomToLoading()
    {
        password = Password.text;
        SceneManager.LoadScene("Loading");
    }

}
