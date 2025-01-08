using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpGate : MonoBehaviour
{
    [SerializeField]
    private Vector3 warppos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Warp(Vector3 warpposition)
    {
        SoundManager sm = Camera.main.transform.parent.GetComponent<SoundManager>();
        sm.PlaySound("warp");
        GameObject character = GameObject.FindGameObjectWithTag("Me");
        character.transform.position = warpposition;
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
           
            Warp(warppos);
        }


    }
}
