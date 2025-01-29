using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAbility : MonoBehaviourPun
{
    [SerializeField]
    private float CollectingTime;


    [SerializeField]
    private int AbilityKind;

    private float collectdeltatime;

    private CameraController cc;
    private RayController rc;

    private DisturberMeter meter;

    // Start is called before the first frame update
    void Start()
    {
      
    }

        // Update is called once per frame
    void OnMouseOver()
    {
        if (!photonView.IsMine){
            return;
        }


        float distance = Vector3.Distance(transform.position, Camera.main.transform.parent.position);
        if(distance >= 0.4f)
        {
            
                cc.WalkAble = true;
                cc.AbilityAble = true;
                rc.CanShoot = true;
            
           

                meter.MeterInactive();
            

                return;
            
        }
        if (Input.GetMouseButton(3))
        {

            
            collectdeltatime += Time.deltaTime;
            meter.Collect(collectdeltatime, CollectingTime);
            Debug.Log(collectdeltatime);
            cc.WalkAble = false;
            cc.AbilityAble = false;
            rc.CanShoot = false;

        }
        else
        {
            collectdeltatime = 0;
            cc.WalkAble = true;
            cc.AbilityAble = true;
            rc.CanShoot = true;
            meter.MeterInactive();
        }
    }

    private void OnMouseExit()
    {
        cc.WalkAble = true;
        cc.AbilityAble = true;
        rc.CanShoot = true;
        meter.MeterInactive();
    }

    private void Update()
    {
        if(collectdeltatime >= CollectingTime)
        {
            Ability ability = Camera.main.GetComponentInParent<Ability>();
            ability.Collect(AbilityKind, 1);
            cc.WalkAble = true;
            cc.AbilityAble = true;
            rc.CanShoot = true;
            meter.MeterInactive();
            PhotonNetwork.Destroy(gameObject);
        }
        if (meter == null)
        {
            meter = GameObject.FindGameObjectWithTag("Meter").GetComponent<DisturberMeter>();
        }
        if(cc == null)
        {
            cc = Camera.main.GetComponentInParent<CameraController>();

        }
        if(rc == null)
        {
            rc = Camera.main.GetComponent<RayController>();
        }

    }
}
