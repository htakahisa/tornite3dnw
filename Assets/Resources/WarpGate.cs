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
        character.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        character.transform.position = warpposition;
        character.transform.rotation = warprotation;
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
