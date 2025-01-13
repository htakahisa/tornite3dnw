using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class RoundManager : MonoBehaviourPun {

    public int round = 1;
    public int sideRound = 1;
    private int Ascore = 0;
    private int Bscore = 0;
    private int Acoin = 70000;
    private int Bcoin = 700;
    private bool Awin = false;
    private bool Bwin = false;
    private int service = 0;

    public int streak = 0;

    private bool olive = false; 

    private int Aloadout = 0;
    private int Bloadout = 0;

    private string Side = "";




    private GameObject judget;

    private Judge judgec;

    private Text mont;

    private GameObject money;

    private Text scoretext;

    private GameObject score;


    private float recklesstime;

    private bool winnerIsA;


    //private PhotonMethod pmc;

    BuyWeponManager bwm;

    GameObject finisherManager;

    List<string> finisherList = new List<string> { "wood", "fireaxe", "dummy", "blacknoir", "gridwhite", "liberation", "bloom", "snow"};

    public static RoundManager rm = null;

    // Start is called before the first frame update
    void Start() {

        
        GetData();
        // サンプルのリスト
     
        Invoke("FirstWeapon", 0.01f);

    }

    private void FirstWeapon()
    {
        RayController.rc.Classic();

    }

    // 指定された文字列がリストに存在するかを確認するメソッド
    public bool IsFinisher(string finisher)
    {
        if (finisherList == null || finisher == null)
        {
            return false;
        }

        return finisherList.Contains(finisher);
    }

    public string GetSide()
    {
        return Side;
    }


    private void Awake() {



    

        if (rm == null) {
            rm = this;
            DontDestroyOnLoad(gameObject);
           
        } else {
            Destroy(gameObject);
        }

      


    }




    // Update is called once per frame
    void Update() {

        if (Side.Equals(""))
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            {
                Side = "Leviathan";
            }
            else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
            {
                Side = "Valkyrie";
            }
        }
            recklesstime += Time.deltaTime;
        if (mont != null) {           
            if (PhotonNetwork.LocalPlayer.ActorNumber == 1) {
                mont.text = Acoin + " platinum";
            } else {
                mont.text = Bcoin + " platinum";
                }
            }
            if (scoretext != null)
            {
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                {
                    scoretext.text = Ascore + " - " + Bscore;
                }
                else
                {
                    scoretext.text = Bscore + " - " + Ascore;
                }

            }



            if (Input.GetKeyDown(KeyCode.I)) {
            RoundEnd(false);

        }

    }


    private void GetData() {


        

        judget = GameObject.FindWithTag("Judge");

        money = GameObject.FindWithTag("Money");

        mont = money.GetComponent<Text>();

        score = GameObject.FindWithTag("Score");

        scoretext = score.GetComponent<Text>();

        //pmc = pm.GetComponent<PhotonMethod>();
        bwm = gameObject.GetComponent<BuyWeponManager>();
        
        judgec = judget.GetComponent<Judge>();
    }


    public void RoundEnd(bool WinnerIsA) {

        winnerIsA = WinnerIsA;

        Finisher();
  
        Invoke("RoundProcess", 5.0f);
      

    }



    private void RoundProcess()
    {
        if (bwm != null)
        {
            bwm.nowweponcost = 0;
        }
        Debug.Log(winnerIsA);









        



        // RPC��B���\�b�h��Ăяo���A���ׂẴN���C�A���g�ɓ���
        //photonView.RPC("InstanceAvatar", RpcTarget.All);








        if (Awin && winnerIsA)
        {
            this.Bcoin += 1500;
            this.streak = 1;
        }
        else if (Bwin && !winnerIsA)
        {
            this.Acoin += 1500;
            this.streak = 2;
        }


        if (winnerIsA)
        {

            this.Awin = true;
            this.Ascore += 1;

            this.Acoin += 1000 + sideRound * 500;
            this.Bcoin += 500 + sideRound * 300;


        }
        else
        {

            this.Bwin = true;
            this.Bscore += 1;
            this.Bcoin += 1000 + sideRound * 500;
            this.Acoin += 500 + sideRound * 300;
        }

        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            BattleLog.battlelog.ChangeText(HpMaster.hpmaster.GetHp(2));
        }
        else
        {
            BattleLog.battlelog.ChangeText(HpMaster.hpmaster.GetHp(1));
        }


        round += 1;
        sideRound += 1;

        if (Ascore > 12 || Bscore > 12)
        {

            //GameEnd();
            StartLoadingScene("lounge", winnerIsA);

        }
        else
        {
            StartLoadingScene("battle", winnerIsA);
        }
      

            //if (pmc != null) {
            //    pmc.Method("Scene");
            //}

            //if (SceneManager.GetActiveScene().name.Equals("battle")) {
            //    SceneManager.LoadScene("battle2");
            //} else {
            //    SceneManager.LoadScene("battle");
            //}
        }



    private void Finisher()
    {
        GameObject loser = GameObject.FindGameObjectWithTag("Player");

        if (PhotonNetwork.LocalPlayer.ActorNumber == 1 == winnerIsA)
        {
            // finisher �̖��O�𕐊킩��擾
            GameObject winner = GameObject.FindGameObjectWithTag("MainCamera");
            RayController ray = winner.GetComponent<RayController>();

           if (IsFinisher(ray.getSkinName()))
           {
                PhotonNetwork.Instantiate(ray.getSkinName(), loser.transform.position, Quaternion.identity);
           }
        }
        else
        {
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            Vector3 loserPosition = GameObject.FindGameObjectWithTag("Me").transform.position; // 負けた側の位置
            Vector3 offset = new Vector3(0, 4, -4); // カメラの位置を調整（斜め上に移動）
            camera.transform.position = loserPosition + offset; // カメラを新しい位置に移動

            // カメラの向きを負けた側の位置に向ける
            camera.transform.LookAt(loserPosition);
        }

    }



        // �V�[���̔񓯊��ǂݍ��݂�s���R���[�`��
    public IEnumerator LoadYourScene(string sceneName, bool WinnerIsA) {
        // �V�[���̓ǂݍ��݊J�n
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // �V�[���̓ǂݍ��݂���������܂őҋ@
        while (!asyncLoad.isDone) {
            yield return null;
        }

        // �V�[���̓ǂݍ��݊�����ɍs����������
        Debug.Log("Scene Loaded. Now executing the next task.");


        // �����Ŏ��̏�����s��
        GetData();
        if (recklesstime <= 25) {
            judgec.Reckless();
            service += 500;
        }
        //if ((Aloadout - Bloadout >= 2000 && Bwin) || (Bloadout - Aloadout >= 2000 && Awin)) {
        //    judgec.Upset();
        //    service += 1000;
        //}
        if (olive) {
            judgec.Olive();
            service += 500;
        }

        if (round == 13)
        {
            sideRound = 1;
            Acoin = 700;
            Bcoin = 700;
            if (Side.Equals("Leviathan"))
            {
                Side = "Valkyrie";
            }
            else if (Side.Equals("Valkyrie"))
            {
                Side = "Leviathan";
            }
        }

        RayController.rc.Classic();

        Aloadout = 0;
        Bloadout = 0;
        olive = false;
        recklesstime = 0f;

        if (WinnerIsA) {
            Acoin += service;
        }
        if (!WinnerIsA) {
            Bcoin += service;
        }
        Debug.Log("Acoin:" + Acoin + " Bcoin:" + Bcoin);


    }

    // �V�[���ǂݍ��݂�J�n���郁�\�b�h
    public void StartLoadingScene(string sceneName, bool WinnerIsA) {
        StartCoroutine(LoadYourScene(sceneName, WinnerIsA));
    }




    public void IsOlive() {

        olive = true;

    }



    public bool IsCanBuy(int number, int player) {

        if (player == 1 && number <= Acoin || player == 2 && number <= Bcoin) {
            return true;
        } else {
            return false;
        }

    }

    public void ChangeCoin(int number, int player) {
        if (player == 1) {
            Acoin += number;
        } else {
            Bcoin += number;
        }
    }



    public int GetAScore() {
        return Ascore;
    }

    public int GetBScore() {
        return Bscore;
    }


    public void GameEnd() {

        SceneManager.LoadScene("lounge");

    }
   
    public void AddOutLoad(int outload, int number) {

        if (number == 1) {
            Aloadout += outload;
        } else {
            Bloadout += outload;
        }

       

    }

 




}
