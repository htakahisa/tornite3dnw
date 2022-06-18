using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscController : MonoBehaviour
{

    public SensityvityController sensityvity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            sensityvity.UpdateActive();
        }
    }
}
