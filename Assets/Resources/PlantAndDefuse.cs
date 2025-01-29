using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantAndDefuse : MonoBehaviourPun
{

    public bool CanPlant = false;
    private float PlantTime = 5f;
    private float DefuseTime = 4f;
    private bool IsHalf = false;

    Disturber disturber = null;
    private bool HasDefuse = false;

    private CameraController cc;
    private RayController rc;
    private SoundManager sm;
    private DisturberMeter meter;




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
        if (Input.GetKey(KeyCode.Alpha4))
        {

            if (RoundManager.rm.GetSide().Equals("Leviathan"))
            {
                disturber = GameObject.Find("Disturber(Clone)").GetComponent<Disturber>();
                float distance = Vector3.Distance(transform.position, disturber.transform.position);
                if (distance <= 2) { 
                sm.PlaySound("defuse");
                    DefuseTime -= Time.deltaTime;
                    meter.Defuse(DefuseTime);
                    Debug.Log(DefuseTime);
                    if (DefuseTime <= 2)
                    {
                        IsHalf = true;
                    }

                    cc.WalkAble = false;
                    cc.AbilityAble = false;
                    rc.CanShoot = false;



                   
                }

            }
            else if (RoundManager.rm.GetSide().Equals("Valkyrie") && CanPlant && disturber == null)
            {

                PlantTime -= Time.deltaTime;



                meter.Plant(PlantTime);

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
                DefuseTime = 2;
            }
            else if (!IsHalf)
            {
                DefuseTime = 4;
            }

            PlantTime = 5;

            meter.MeterInactive();

        }
      
        if (!HasDefuse)
        {
            if (DefuseTime <= 0)
            {

                
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
