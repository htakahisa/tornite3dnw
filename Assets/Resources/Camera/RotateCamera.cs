using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private float rotationSpeed = 15f; // ��]���x

    void Update()
    {
        // Y���i������j����Ɏ��v���ɉ�]
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
