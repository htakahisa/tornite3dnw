using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationSpeed = new Vector3(0, 50, 0); // Y軸を中心に回転させる速度

    void Update()
    {
        // 毎フレーム回転させる
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
