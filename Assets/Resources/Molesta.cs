using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Molesta : MonoBehaviourPunCallbacks
{
    public float speed = 2.0f; // �ǂ̑O�i���x

    private bool IsMoving = true;


    private void Start()
    {
       
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (IsMoving)
            {
                // �ǂ�O�i������
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Head") && photonView.IsMine)
        {
            other.gameObject.GetComponentInParent<CameraController>().Stuned(1, 0.1f);
            Debug.Log($"Player {other.gameObject.name} touched the wall!");
        }
    }

    private void OnMouseOver()
    {
        if (!photonView.IsMine)
        {
            return;
        }

            if (Input.GetMouseButtonDown(3))
        {
            IsMoving = !IsMoving;
        }
    }




}
