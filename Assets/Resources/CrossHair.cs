using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    private bool HasSetSize = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (HasSetSize)
        {
            return;
        }
        float size = CrosshairManager.crshrM.GetSize();
        transform.localScale = new Vector3(size, size, size);
        HasSetSize = true;
    }



}
