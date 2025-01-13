using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyAbility : MonoBehaviour
{
    [SerializeField]
    private int Cost;
    [SerializeField]
    private string Ability;
    [SerializeField]
    private int Limits;
    [SerializeField]
    private int Kind;

    private AbilityBuyManager abm;

    [SerializeField]
    private string Discription;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {
        DiscriptionTextManager.dtm.TextChange(Discription);

        if (Input.GetMouseButtonDown(0))
        {
            if (abm == null)
            {
                abm = gameObject.GetComponentInParent<AbilityBuyManager>();
            }
            abm.Buy(Cost, Ability, Limits, Kind);

        }
        if (Input.GetMouseButtonDown(1))
        {
            if (abm == null)
            {
                abm = gameObject.GetComponentInParent<AbilityBuyManager>();
            }
            abm.Buy(Cost, "cancel", Limits, Kind);
        }
    }
}
