using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBuyManager : MonoBehaviourPunCallbacks
{


    public int able1cost = 0;
    public int able2cost = 0;
    private string nowability = "";


    private Ability able;


    GameObject rmo;
    RoundManager rmc;



    // Start is called before the first frame update
    void Awake()
    {







        rmo = GameObject.Find("Roundmanager");
        rmc = rmo.GetComponent<RoundManager>();

    }

    // Update is called once per frame
    void Update()
    {








    }
    public void Buy(int cost, string Ability, int limits, int kind)
    {
        able = Camera.main.GetComponentInParent<Ability>();

        if (Ability == "cancel")
        {
            
            if (kind == 1)
            {
                Return(able1cost);
                able1cost = 0;
                able.Buy("cancel", 1);
            }
            if (kind == 2)
            {
                Return(able2cost);
                able2cost = 0;
                able.Buy("cancel", 2);
            }
            if (kind == 3)
            {
                Return(able1cost);
                Return(able2cost);
                able1cost = 0;
                able2cost = 0;
                able.Buy("cancel", 3);
            }

            
            
            nowability = "";
        }
        else
        {


            if (CanBuy(cost) && able.Limit(limits, kind, Ability))
            {
                
                Pay(cost);
                able.Buy(Ability, kind);
                if (kind == 1)
                {
                    able1cost += cost;
                }
                else
                {
                    able2cost += cost;
                }
                    nowability = Ability;
            }
        }
    }



    public bool CanBuy(int cost)
    {


        return (rmc.IsCanBuy(cost, PhotonNetwork.LocalPlayer.ActorNumber));


    }




    public void Pay(int cost)
    {



        rmc.ChangeCoin(cost * -1, PhotonNetwork.LocalPlayer.ActorNumber);
        rmc.AddOutLoad(cost, PhotonNetwork.LocalPlayer.ActorNumber);
    }

    public void Return(int cost)
    {

        rmc.ChangeCoin(cost, PhotonNetwork.LocalPlayer.ActorNumber);
        rmc.AddOutLoad(-1 * cost, PhotonNetwork.LocalPlayer.ActorNumber);

    }






}
