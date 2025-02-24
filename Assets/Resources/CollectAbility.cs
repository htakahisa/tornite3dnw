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

    [SerializeField]
    private LayerMask orb;

    // Start is called before the first frame update
    void Start()
    {
        if (meter == null)
        {
            meter = DisturberMeter.disturbermeter;
        }
        if (cc == null)
        {
            cc = Camera.main.GetComponentInParent<CameraController>();

        }
        if (rc == null)
        {
            rc = Camera.main.GetComponent<RayController>();
        }
    }

        // Update is called once per frame
    

     
    



    private void Update()
    {

        if (!photonView.IsMine)
        {
            return;
        }


        float distance = Vector3.Distance(transform.position, Camera.main.transform.parent.position);

        if (distance >= 1f)
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

        if (collectdeltatime >= CollectingTime)
        {
            Ability ability = Camera.main.GetComponentInParent<Ability>();
            ability.Collect(AbilityKind, 1);
            cc.WalkAble = true;
            cc.AbilityAble = true;
            rc.CanShoot = true;
            meter.MeterInactive();
            PhotonNetwork.Destroy(gameObject);
        }

    }
}
