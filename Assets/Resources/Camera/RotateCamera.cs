using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private float rotationSpeed = 15f; // 回転速度

    void Update()
    {
        // Y軸（上方向）を基準に時計回りに回転
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
