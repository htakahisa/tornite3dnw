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
        GameObject character = GameObject.FindGameObjectWithTag("Me");
        character.transform.position = warpposition;
    }


    void OnMouseOver()
    {

        // �E�N���b�N�ɂ����
        if (Input.GetKeyDown(KeyCode.F))
        {
            Warp(warppos);
        }


    }
}
