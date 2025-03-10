using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerInRound : MonoBehaviourPun
{

    private double TimerAfterPlant = 30;
    private double  TimerBeforePlant = 45;
    private Disturber Disturber;

    [SerializeField]
    private TextMeshProUGUI TimerText;
    private bool HasPlanting = false;

    private double missionFailureTime = 0;

    public static TimerInRound TIR;


    private void Awake()
    {
        TIR = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (MapManager.mapmanager.GetMapName() == "DuelLand")
        {
            TimerBeforePlant = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (PhaseManager.pm.GetPhase().Equals("Duel"))
        {
            return;
        }


        if (PhaseManager.pm.GetPhase().Equals("Buy"))
        {
            missionFailureTime = PhotonNetwork.Time + TimerBeforePlant;
            return;
        }

    

        if (HasPlanting)
        {
            TimerAfterPlant = PhotonNetwork.Time - Disturber.plantedTime;
            TimerText.text = "Defuse in \n" + (30 - (int)TimerAfterPlant);
        }
        else
        {
            TimerBeforePlant = missionFailureTime - PhotonNetwork.Time;
            TimerText.text = "TimeUp in\n" + (int)TimerBeforePlant;
        }

        if(TimerBeforePlant <= 0)
        {
            if (RoundManager.rm.RoundProcessing)
            {
                return;
            }

            if (RoundManager.rm.GetSide().Equals("Leviathan"))
            {

                ResultSynchronizer.rs.SendResult(PhotonNetwork.LocalPlayer.ActorNumber);

            }
           
        }
    }

    public void Planted(Disturber disturber)
    {
        Disturber = disturber;
        HasPlanting = true;
    }
   
}
