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

    [SerializeField]
    private BuyPanelManager bpm;

    private CameraController cc;

    // Start is called before the first frame update
    void Awake()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if(GetComponentInParent<CameraController>() != null && cc == null)
        {
            cc = GetComponentInParent<CameraController>();
        }






    }
    public void Buy(int cost, string Ability, int limits, int kind)
    {
        able = Camera.main.GetComponentInParent<Ability>();

        bool HadDecide = false;

        if (Ability == "cancel")
        {
            HadDecide = true;

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
        if (Ability == "Nakia")
        {
            if (CanBuy(cost) && able.Limit(limits, kind, Ability))
            {
                Pay(cost);
                able.Buy(Ability, kind);
                nowability = Ability;
                cc.SlingPower += 10;
            }
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
                bpm.AdjustSpriteOpacity(nowability);
    }



    public bool CanBuy(int cost)
    {


        return RoundManager.rm.IsCanBuy(cost, PhotonNetwork.LocalPlayer.ActorNumber);


    }




    public void Pay(int cost)
    {
        RoundManager.rm.ChangeCoin(cost * -1, PhotonNetwork.LocalPlayer.ActorNumber);
        RoundManager.rm.AddOutLoad(cost, PhotonNetwork.LocalPlayer.ActorNumber);
    }

    public void Return(int cost)
    {

        RoundManager.rm.ChangeCoin(cost, PhotonNetwork.LocalPlayer.ActorNumber);
        RoundManager.rm.AddOutLoad(-1 * cost, PhotonNetwork.LocalPlayer.ActorNumber);

    }






}
