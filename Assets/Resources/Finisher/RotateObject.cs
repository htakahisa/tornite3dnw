using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationSpeed = new Vector3(0, 50, 0); // Y���𒆐S�ɉ�]�����鑬�x

    void Update()
    {
        // ���t���[����]������
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
