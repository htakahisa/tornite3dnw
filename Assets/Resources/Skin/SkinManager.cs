using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    WeaponSkin weaponskin = new WeaponSkin();
    [SerializeField] private GameObject[] weapons; // ����̃v���t�@�u���i�[����z��
    private int currentWeaponIndex = 0;
    private string usingWeapon;

    // Start is called before the first frame update
    void Start()
    {

        usingWeapon = gameObject.name;

        if (usingWeapon.Equals("Silver"))
        {
            currentWeaponIndex = weaponskin.GetSilver();
        }
        if (usingWeapon.Equals("Noel"))
        {
            currentWeaponIndex = weaponskin.GetNoel();
        }
        if (usingWeapon.Equals("Yor"))
        {
            currentWeaponIndex = weaponskin.GetYor();
        }
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
