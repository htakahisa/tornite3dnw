using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class DamageShowController : MonoBehaviourPunCallbacks
{

    private RectTransform myRectTfm;

    // Start is called before the first frame update
    void Start()
    {
        myRectTfm = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // 自身の向きをカメラに向ける
        myRectTfm.LookAt(Camera.main.transform);
    }



}
