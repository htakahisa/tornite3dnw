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
    private int Acoin = 700;
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

    private Text scoretext;

    private GameObject score;


    private float recklesstime;

    private bool winnerIsA;

    public bool RoundProcessing = false;

    //private PhotonMethod pmc;

    BuyWeponManager bwm;

    GameObject finisherManager;

    List<string> finisherList = new List<string> { "wood", "fireaxe", "dummy", "blacknoir", "gridwhite", "liberation", "bloom", "snow", "sweet", "bloodrose" };

    public static RoundManager rm = null;


    public int BodyShots = 0;
    public int HeadShots = 0;

    public int LeviathanWin = 0;
    public int ValkyrieWin = 0;

    private bool HasPrefs = false;

    // Start is called before the first frame update
    void Start() {

// デバッグ用
#if UNITY_EDITOR
        Acoin = 70000;
#endif


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

        if (!HasPrefs)
        {
            BodyShots = PlayerPrefs.GetInt("Bodyshots");
            HeadShots = PlayerPrefs.GetInt("Headshots");
            LeviathanWin = PlayerPrefs.GetInt(MapManager.mapmanager.GetMapName() + "LeviathanWin");
            ValkyrieWin = PlayerPrefs.GetInt(MapManager.mapmanager.GetMapName() + "ValkyrieWin");
            HasPrefs = true;
        }




        if (Input.GetKeyDown(KeyCode.I)) {
            RoundEnd(false);

        }

    }

    public string GetScoreText()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            return Ascore + " - " + Bscore;
        }
        else
        {
            return Bscore + " - " + Ascore;
        }
    }


    private void GetData() {

        //pmc = pm.GetComponent<PhotonMethod>();
        bwm = gameObject.GetComponent<BuyWeponManager>();
        
    }


    public void RoundEnd(bool WinnerIsA) {

        judgec = Judge.judge;

        if (RoundProcessing)
        {
            return;
        }
        if (recklesstime <= 2)
        {
            return;
        }

        RoundScoreSet();

        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            BattleLog.battlelog.ChangeText(HpMaster.hpmaster.GetHp(2));
        }
        else
        {
            BattleLog.battlelog.ChangeText(HpMaster.hpmaster.GetHp(1));
        }

        RoundProcessing = true;

        winnerIsA = WinnerIsA;

  
        



        if (recklesstime <= 25)
        {
            judgec.Reckless();
            service += 500;
        }
        StartCoroutine(judgec.TextChange());
#if !UNITY_EDITOR

        Finisher();
#endif

        Invoke("RoundProcess", 5.0f);


    }

    private void RoundScoreSet()
    {
        PlayerPrefs.SetInt("Bodyshots", BodyShots);
        PlayerPrefs.SetInt("Headshots", HeadShots);
        PlayerPrefs.SetInt(MapManager.mapmanager.GetMapName() + "LeviathanWin", LeviathanWin);
        PlayerPrefs.SetInt(MapManager.mapmanager.GetMapName() + "ValkyrieWin", ValkyrieWin);
    }



    public void HitEnemy(bool head)
    {
        if (head)
        {
            HeadShots++;
        }
        else
        {
            BodyShots++;
        }
    }

    private void RoundProcess()
    {
        if (bwm != null)
        {
            bwm.nowweponcost = 0;
        }
        Debug.Log(winnerIsA);


        // ラウンド数よりも多い場合は2回目の処理が走ってしまっている。
       
        if (this.Ascore + this.Bscore + 1 > round)
        {
            // 取り消し
            round -= 1;
            sideRound -= 1;

            if (winnerIsA)
            {
                // 最初のBの分を取り消す
                this.Acoin -= 1500;
                this.streak = 0;
                this.Bscore -= 1;
                this.Bcoin -= 1500 + sideRound * 500;
                this.Acoin -= 1000 + sideRound * 300;
            }
            else
            {
                // 最初のAの分を取り消す
                this.Bcoin -= 1500;
                this.streak = 0;
                this.Ascore -= 1;
                this.Acoin -= 1500 + sideRound * 500;
                this.Bcoin -= 1000 + sideRound * 300;
            }
            // 常にプレイヤー2を勝者とする。
            winnerIsA = false;
        }
        // その後もともとの処理を流す


        if (winnerIsA == (PhotonNetwork.LocalPlayer.ActorNumber == 1))
        {
            judgec.Win();
            if (Side == "Leviathan")
            {
                LeviathanWin++;
            }
            else
            {
                ValkyrieWin++;
            }
        }
        else
        {
            judgec.Lose();
            if (Side == "Valkyrie")
            {
                LeviathanWin++;
            }
            else
            {
                ValkyrieWin++;
            }
        }


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
            this.Acoin += 1500 + sideRound * 500;
            this.Bcoin += 1000 + sideRound * 300;


        }
        else
        {

            this.Bwin = true;
            this.Bscore += 1;
            this.Bcoin += 1500 + sideRound * 500;
            this.Acoin += 1000 + sideRound * 300;
        }

    


        round += 1;
        sideRound += 1;

        if (Ascore > 12 || Bscore > 12)
        {
            
            StartLoadingScene("Needless_ending", winnerIsA);

        }
        else
        {         
            StartLoadingScene(MapManager.mapmanager.GetMapName(), winnerIsA);
        }
    }



    private void Finisher()
    {

        GameObject loser = EnemyTag.enemytag.gameObject;

        if (PhotonNetwork.LocalPlayer.ActorNumber == 1 == winnerIsA)
        {
            // finisher �̖��O�𕐊킩��擾
            GameObject winner = Camera.main.gameObject;
            RayController ray = winner.GetComponent<RayController>();

           if (IsFinisher(ray.getSkinName()))
           {
                PhotonNetwork.Instantiate(ray.getSkinName(), loser.transform.position, Quaternion.identity);
           }
        }
        else
        {
            GameObject camera = Camera.main.gameObject;
            Vector3 loserPosition = MyTag.mytag.transform.position; // 負けた側の位置
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
   
        //if ((Aloadout - Bloadout >= 2000 && Bwin) || (Bloadout - Aloadout >= 2000 && Awin)) {
        //    judgec.Upset();
        //    service += 1000;
        //}
     
        GetData();
        

        RoundProcessing = false;

        

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

        if (round == 13)
        {
            streak = 0;
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
        Debug.Log("Acoin:" + Acoin + " Bcoin:" + Bcoin);
        RayController.rc.Classic();

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

 
    public int GetNextMin()
    {
        int nowmoney;

        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            nowmoney = Acoin;
        }
        else
        {
            nowmoney = Bcoin;
        }

        return  nowmoney + 1000 + sideRound * 300;
    }

    public int GetNextMax()
    {
        int nowmoney;

        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            nowmoney = Acoin;
        }
        else
        {
            nowmoney = Bcoin;
        }

        return nowmoney + 1500 + sideRound * 500;
    }

    public int GetMoney()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            return Acoin;
        }
        else
        {
            return Bcoin;
        }
    }
}


