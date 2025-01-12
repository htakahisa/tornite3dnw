using UnityEngine;

public class PlantZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Body"))
        {
            CameraController cc = other.GetComponentInParent<CameraController>();
            cc.CanPlant = true;
            Debug.Log("a");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Body"))
        {
            CameraController cc = other.GetComponentInParent<CameraController>();
            cc.CanPlant = false;
        }
    }
}
