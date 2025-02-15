using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanCamera : MonoBehaviour
{
    public static ScanCamera sc = null;

    [SerializeField]
    private Camera scancamera;

    [SerializeField]
    private Camera uicamera;

    [SerializeField]
    private Camera secondcamera;

    [SerializeField]
    private Camera blindcamera;

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
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void InActiveScan() {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public Camera GetUiCamera()
    {
        return uicamera;
    }

    public Camera GetSecondCamera()
    {
        return secondcamera;
    }

    public Camera GetBlindCamera()
    {
        return blindcamera;
    }


}
