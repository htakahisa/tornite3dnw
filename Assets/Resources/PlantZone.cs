using UnityEngine;

public class PlantZone : MonoBehaviour
{



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Body"))
        {
          

            PlantAndDefuse defuse = other.GetComponentInParent<PlantAndDefuse>();
            defuse.CanPlant = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Body"))
        {
          

            PlantAndDefuse defuse = other.GetComponentInParent<PlantAndDefuse>();
            defuse.CanPlant = false;
        }
    }

}
