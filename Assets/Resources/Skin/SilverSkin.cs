using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverSkin : MonoBehaviour
{
    WeponSkin weponskin = new WeponSkin();
    [SerializeField] private GameObject[] weapons; // 武器のプロファブを格納する配列
    private int currentWeaponIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentWeaponIndex = weponskin.GetSilver();
        SwitchWeapon(currentWeaponIndex);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void SwitchWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == index);
        }
    }
}
