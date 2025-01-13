using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyWeapon : MonoBehaviour
{
    [SerializeField]
    private int Cost;
    [SerializeField]
    private string Weapon;
    [SerializeField]
    private string Discription;

    private BuyWeponManager bwm;

 

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
            if (bwm == null) {
                bwm = gameObject.GetComponentInParent<BuyWeponManager>();
            }
            bwm.Buy(Cost, Weapon);
           
        }
    }
}
