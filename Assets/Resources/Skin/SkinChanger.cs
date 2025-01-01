using UnityEngine;
using UnityEngine.UI;

public class SkinChanger : MonoBehaviour
{
    public static SkinChanger skinchanger;
    [SerializeField] private GameObject[] weapons; // 武器のプロファブを格納する配列
    private int currentWeaponIndex = 0;
    private int index = 0;

    private void Start()
    {
        currentWeaponIndex = 0;
        skinchanger = this;
    }


    private void Update()
    {
        // スクロール値を計算
        float scrollValue = gameObject.GetComponent<Scrollbar>().value;


        index = Mathf.Clamp(Mathf.FloorToInt(scrollValue * (weapons.Length - 1)), 0, weapons.Length - 1);

        currentWeaponIndex = index;
        SwitchWeapon(currentWeaponIndex);
    }

    void SwitchWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == index);
        }
    }

    public int GetIndex()
    {
        return index;
    }
}
