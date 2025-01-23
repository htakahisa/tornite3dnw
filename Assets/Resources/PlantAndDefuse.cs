using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private SoundManager sm;
    private DisturberMeter meter;

    private bool IsPlanting = false;
    private bool IsDefusing = false;

    private bool SpecialPermission = false;
    private Vector3 PermissionPos;

    // Start is called before the first frame update
    void Start()
    {
        if (MapManager.mapmanager.GetMapName() == "DuelLand")
        {
            
            return;
        }
        cc = GetComponent<CameraController>();
        rc = Camera.main.GetComponent<RayController>();
        sm = GetComponent<SoundManager>();
        meter = GameObject.FindGameObjectWithTag("Meter").GetComponent<DisturberMeter>();

    }

    // Update is called once per frame
    void Update()
    {

        if (!photonView.IsMine)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if(PermissionPos == transform.position)
            {
                SpecialPermission = true;
            }
            else
            {
                SpecialPermission = false;
            }
            if (RoundManager.rm.GetSide().Equals("Leviathan") && (CanDefuse || SpecialPermission))
            {
                 sm.PlaySound("defuse");
                IsDefusing = true;                
                PermissionPos = transform.position;
            }
            else if (RoundManager.rm.GetSide().Equals("Valkyrie") && (CanPlant || SpecialPermission) && disturber == null)
            {
                
                IsPlanting = true;
                PermissionPos = transform.position;
               
            }
        }

        if (IsPlanting)
        {
            PlantTime -= Time.deltaTime;

            meter.Plant(PlantTime);

            cc.WalkAble = false;
            cc.AbilityAble = false;
            rc.CanShoot = false;
        }
        if (IsDefusing)
        {
           
            DefuseTime -= Time.deltaTime;
            meter.Defuse(DefuseTime);
            Debug.Log(DefuseTime);
            if (DefuseTime <= 3)
            {
                IsHalf = true;
            }

            cc.WalkAble = false;
            cc.AbilityAble = false;
            rc.CanShoot = false;
        }

       
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            cc.WalkAble = true;
            cc.AbilityAble = true;
            rc.CanShoot = true;
            IsPlanting = false;
            IsDefusing = false; 

            if (IsHalf)
            {
                DefuseTime = 3;
            }
            else if (!IsHalf)
            {
                DefuseTime = 6;
            }

            PlantTime = 4;

            meter.MeterInactive();

        }
      
        if (!HasDefuse)
        {
            if (DefuseTime <= 0)
            {

                disturber = GameObject.Find("Disturber(Clone)").GetComponent<Disturber>();
                disturber.Defuse();
                meter.MeterInactive();
                HasDefuse = true;
                if (RoundManager.rm.GetSide() == "Leviathan")
                {
                    RoundManager.rm.ChangeCoin(300, PhotonNetwork.LocalPlayer.ActorNumber);
                }
            }
        }


        if (PlantTime <= 0)
        {
            disturber = PhotonNetwork.Instantiate("Disturber", transform.position, transform.rotation).GetComponent<Disturber>();
            disturber.Plant();
            meter.MeterInactive();
            PlantTime = 5;
            if(RoundManager.rm.GetSide() == "Valkyrie")
            {
                RoundManager.rm.ChangeCoin(500, PhotonNetwork.LocalPlayer.ActorNumber);
            }
        }

    
    }

 



        
    
 

}
