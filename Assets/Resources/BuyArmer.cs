using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyArmer : MonoBehaviourPun
{
    [SerializeField]
    private float shield = 0;

    [SerializeField]
    private int level = 0;

    [SerializeField]
    private int platinum = 0;

    [SerializeField]
    private string discription = "";

    HpMaster hm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LostArmer()
    {
        hm = HpMaster.hpmaster;
        hm.LastCostChange(0);
        hm.SetShield(1);
        armer.armermanager.ArmerNo(1);
    }

    private void OnMouseOver()
    {

        hm = HpMaster.hpmaster;

        RoundManager rm = RoundManager.rm;
        DiscriptionTextManager.dtm.TextChange(discription);

        if (Input.GetMouseButtonDown(0))
        {
            

            if(!rm.IsCanBuy(platinum - hm.GetLastCost(), PhotonNetwork.LocalPlayer.ActorNumber))
            {
                return;
            }
            rm.ChangeCoin(-platinum, PhotonNetwork.LocalPlayer.ActorNumber);
           

            rm.ChangeCoin(hm.GetLastCost(), PhotonNetwork.LocalPlayer.ActorNumber);
            hm.LastCostChange(platinum);
            hm.SetShield(shield);
            armer.armermanager.ArmerNo(level);
            

            

        }
      
    }
    

       

    
}
