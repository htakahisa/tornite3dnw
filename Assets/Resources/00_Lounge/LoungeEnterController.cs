using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class LoungeEnterController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    PhotonView uiSync;

    [SerializeField]
    Text userCountText;

    [SerializeField]
    PublicProperty pp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
         //   clickEnter();
        }
    }

    public void clickEnter()
    {
        //uiSync.RPC("addUserCount", RpcTarget.All);

        //userCountText.text = "USER COUNT : " +  pp.getUserCount().ToString();

        SceneManager.LoadScene("battle");

    }
}
