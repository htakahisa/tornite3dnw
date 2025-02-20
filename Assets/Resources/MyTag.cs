using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTag : MonoBehaviour
{

    public static MyTag mytag;

    // Start is called before the first frame update
    void Awake()
    {
        mytag = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
