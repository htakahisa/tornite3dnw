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

    private double startTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (PhaseManager.pm.GetPhase().Equals("Buy"))
        {
            startTime = PhotonNetwork.Time + TimerBeforePlant;
            return;
        }

        

        if(HasPlanting)
        {
            TimerAfterPlant = Disturber.plantedTime;
            TimerText.text = "Detonation in : " + (30 - (int)TimerAfterPlant);
        }
        else
        {
            TimerBeforePlant = PhotonNetwork.Time - startTime;
            TimerText.text = "Mission failure in : " + (int)TimerBeforePlant;
        }

        if(TimerBeforePlant <= 0)
        {
            if (RoundManager.rm.RoundProcessing)
            {
                return;
            }

            if (RoundManager.rm.GetSide().Equals("Leviathan"))
            {

                RoundManager.rm.RoundEnd(PhotonNetwork.LocalPlayer.ActorNumber == 1);

            }
            if (RoundManager.rm.GetSide().Equals("Valkyrie"))
            {

                RoundManager.rm.RoundEnd(PhotonNetwork.LocalPlayer.ActorNumber == 2);

            }
        }
    }

    public void Planted(Disturber disturber)
    {
        Disturber = disturber;
        HasPlanting = true;
    }
   
}
