using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerInRound : MonoBehaviourPun
{

    private float TimerBeforePlant = 60;
    private float TimerAfterPlant = 30;
    private Disturber Disturber;

    [SerializeField]
    private TextMeshProUGUI TimerText;
    private bool HasPlanting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (PhaseManager.pm.GetPhase().Equals("Buy"))
        {
            return;
        }

        

        if(HasPlanting)
        {
            TimerAfterPlant = Disturber.timer;
            TimerText.text = "Detonation in : " + (30 - Mathf.FloorToInt(TimerAfterPlant));
        }
        else
        {
            TimerBeforePlant -= Time.deltaTime;
            TimerText.text = "Mission failure in : " + Mathf.FloorToInt(TimerBeforePlant);
        }

        if(TimerBeforePlant <= 0)
        {
            if (RoundManager.rm.RoundProcessing)
            {
                return;
            }

            if (PhotonNetwork.LocalPlayer.ActorNumber == 1) {

                RoundManager.rm.RoundEnd((RoundManager.rm.GetSide() == "Leviathan") == RoundManager.rm.round < 13);

            }
            else
            {
                RoundManager.rm.RoundEnd((RoundManager.rm.GetSide() == "Valkyrie") == RoundManager.rm.round < 13);
            }
        }
    }

    public void Planted(Disturber disturber)
    {
        Disturber = disturber;
        HasPlanting = true;
    }
   
}
