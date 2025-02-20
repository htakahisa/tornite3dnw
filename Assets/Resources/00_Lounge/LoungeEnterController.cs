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

    [SerializeField]
    private AudioClip loungeBGM;

    private static LoungeBgmManager bgmManagerInstance;

    // Start is called before the first frame update
    void Start()
    {
        if (bgmManagerInstance == null)
        {
            // 動的に GameObject を生成
            GameObject bgmObject = new GameObject("LoungeBgmManager");

            // 必要なコンポーネントを追加
            bgmManagerInstance = bgmObject.AddComponent<LoungeBgmManager>();
            // defaultBGM を設定
            bgmManagerInstance.SetDefaultBGM(loungeBGM);
        }
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

        SceneManager.LoadScene("Loading");

    }

    public void Custom()
    {
        SceneManager.LoadScene("Custom");
    }

}
