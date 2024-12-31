using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class armer : MonoBehaviour
{
    Image image = null;

    private int armerlevel = 1;

    public static armer armermanager = null;

    // Start is called before the first frame update
    void Awake()
    {

        armermanager = this;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ArmerNo(int level) {

        image = gameObject.GetComponent<Image>();
        armerlevel = level;
        if (armerlevel == 1) {
            image.color = Color.green;
        }
        if (armerlevel == 2) {
            image.color = Color.gray;
        }
        if (armerlevel == 3) {
            image.color = Color.yellow;
        }
        if (armerlevel == 4) {
            image.color = Color.white;
        }
    }
   
}
