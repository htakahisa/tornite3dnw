using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefuseZone : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Body")) { 

            PlantAndDefuse defuse = other.GetComponentInParent<PlantAndDefuse>();
            defuse.CanDefuse = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Body"))
        {
          
            PlantAndDefuse defuse = other.GetComponentInParent<PlantAndDefuse>();
            defuse.CanDefuse = false;
        }
    }


}
