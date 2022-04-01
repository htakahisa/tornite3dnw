using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class DamageShowController : MonoBehaviour
{

    //　DamageUIプレハブ
    [SerializeField]
    private GameObject damageUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Damage(Collision col)
    {
        //　DamageUIをインスタンス化。登場位置は接触したコライダの中心からカメラの方向に少し寄せた位置
        var obj = PhotonNetwork.Instantiate("DamageUi", gameObject.transform.up * 10f, Quaternion.identity);
    }
}
