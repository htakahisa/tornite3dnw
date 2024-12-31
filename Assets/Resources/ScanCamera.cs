using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanCamera : MonoBehaviour
{
    public static ScanCamera sc = null;

    // Start is called before the first frame update
    void Awake()
    {
        sc = this;
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveScan() {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void InActiveScan() {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
