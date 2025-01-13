using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscriptionTextManager : MonoBehaviour
{

    public static DiscriptionTextManager dtm = null;

    // Start is called before the first frame update
    void Awake()
    {
        dtm = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TextChange(string text)
    {
        gameObject.GetComponent<Text>().text = text;
    }

}
