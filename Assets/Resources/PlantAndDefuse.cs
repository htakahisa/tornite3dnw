using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAndDefuse : MonoBehaviourPun
{

    public bool CanPlant = false;
    private float PlantTime = 4f;
    private float DefuseTime = 6f;
    private bool IsHalf = false;

    public bool CanDefuse = false;

    Disturber disturber = null;
    private bool HasDefuse = false;

    private CameraController cc;
    private RayController rc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CameraController>();
        rc = Camera.main.GetComponent<RayController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {

            if (RoundManager.rm.GetSide().Equals("Leviathan") && CanDefuse)
            {
                DefuseTime -= Time.deltaTime;
                Debug.Log(DefuseTime);
                if (DefuseTime <= 3)
                {
                    IsHalf = true;
                }

                cc.WalkAble = false;
                cc.AbilityAble = false;
                rc.CanShoot = false;


            }
            else if (RoundManager.rm.GetSide().Equals("Valkyrie") && CanPlant && disturber == null)
            {

                PlantTime -= Time.deltaTime;


                cc.WalkAble = false;
                cc.AbilityAble = false;
                rc.CanShoot = false;

            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            cc.WalkAble = true;
            cc.AbilityAble = true;
            rc.CanShoot = true;

            if (IsHalf)
            {
                DefuseTime = 3;
            }
            else if (!IsHalf)
            {
                DefuseTime = 6;
            }

            PlantTime = 4;
        }
      
        if (!HasDefuse)
        {
            if (DefuseTime <= 0)
            {

                disturber = GameObject.Find("Disturber(Clone)").GetComponent<Disturber>();
                disturber.Defuse();
                HasDefuse = true;
            }
        }


        if (PlantTime <= 0)
        {
            disturber = PhotonNetwork.Instantiate("Disturber", transform.position, transform.rotation).GetComponent<Disturber>();
            disturber.Plant();
            PlantTime = 5;
        }

    
    }
}
