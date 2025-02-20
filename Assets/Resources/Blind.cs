using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blind : MonoBehaviourPun
{
    [SerializeField]
    private LayerMask wall;

    // Start is called before the first frame update
    void Awake()
    {
        Invoke("PunDestroy", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            return;
        }
        Vector3 direction = (Camera.main.transform.position - transform.position).normalized;
        RaycastHit hit;
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        if (!Physics.Raycast(transform.position, direction, out hit, distance, wall))
        {           
            Camera.main.GetComponent<ScanCamera>().GetBlindCamera().gameObject.SetActive(true);
        }
        else
        {
            Camera.main.GetComponent<ScanCamera>().GetBlindCamera().gameObject.SetActive(false);
        }

        Debug.DrawRay(transform.position, direction * 100, Color.green);

    }

    private void OnDestroy()
    {
        Camera.main.GetComponent<ScanCamera>().GetBlindCamera().gameObject.SetActive(false);
    }


    private void PunDestroy()
    {
        PhotonNetwork.Destroy(gameObject);
    }


}
