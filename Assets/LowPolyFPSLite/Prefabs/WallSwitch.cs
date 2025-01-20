using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSwitch : MonoBehaviourPun
{
    [SerializeField]
    // アクティブ化または非アクティブ化したいオブジェクトのリスト
    private List<GameObject> objects = new List<GameObject>();

    [SerializeField]
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        if (PhaseManager.pm.GetPhase().Equals("Buy"))
        {
            return;
        }

        // で発動
        if (Input.GetMouseButtonDown(3))
        {
            photonView.RPC("Close", RpcTarget.All);

        }


    }
    [PunRPC]
    private void Close()
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null) // オブジェクトがnullでないことを確認
            {
                obj.SetActive(true);
            }
        }
        audioSource.Play();
    }

   
}
