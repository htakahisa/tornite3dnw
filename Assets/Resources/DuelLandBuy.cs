using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelLandBuy : MonoBehaviour
{
    [SerializeField]
    private string Weapon;
    [SerializeField]
    private float Shield;
    [SerializeField]
    private int ArmerLevel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Buy()
    {
        RayController.rc.Invoke(Weapon,0);
        HpMaster.hpmaster.SetShield(Shield);
        armer.armermanager.ArmerNo(ArmerLevel);
    }

}
