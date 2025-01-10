using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpGate : MonoBehaviour
{
    [SerializeField]
    private Vector3 warppos;
    [SerializeField]
    private Quaternion warprot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Warp(Vector3 warpposition, Quaternion warprotation)
    {
        SoundManager sm = Camera.main.transform.parent.GetComponent<SoundManager>();
        sm.PlaySound("warp");
        GameObject character = GameObject.FindGameObjectWithTag("Me");
        character.GetComponent<CharacterController>().enabled = false;
        character.transform.position = warpposition;
        character.transform.rotation = warprotation;
        character.GetComponent<CharacterController>().enabled = true;
    }


    void OnMouseOver()
    {
        if (PhaseManager.pm.GetPhase().Equals("Buy"))
        {
            return;
        }

        // CÇ≈ÉèÅ[Év
        if (Input.GetKeyDown(KeyCode.C))
        {
           
            Warp(warppos, warprot);
        }


    }
}
