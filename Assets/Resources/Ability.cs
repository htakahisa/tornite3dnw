using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ability : MonoBehaviourPun {


    CameraController cc;
    RayController rc;

    private string Able1 = "";
    private string Able2 = "";
    public int number1 = 0;
    public int number2 = 0;
    public bool CanAbility = true;

    private KeyCode aquaCloseMap;

    // Start is called before the first frame update
    void Start() {

        aquaCloseMap = KeyController.Instance.Settings.AquaCloseMap;

    }

    // Update is called once per frame
    void Update() {

        if (PhaseManager.pm.GetPhase() != "Duel")
        {
            if (PhaseManager.pm.GetPhase() == null)
            {
                return;
            }

            if (PhaseManager.pm.GetPhase().Equals("Buy"))
            {
                return;
            }
        }

        if (cc == null)
        {
            cc = gameObject.GetComponent<CameraController>();
        }


        if (!CanAbility)
        {

            // aqua などマップを開くものは閉じる
            if (Able2 == "Aqua" && Input.GetKeyDown(aquaCloseMap))
            {
                Use(Able2);
            }

            return;
        }


        

        if (cc.AbilityAble)
            {
            if (Input.GetKeyDown(KeyCode.E) && (number1 >= 1 || Able1 == "Kamaitachi"))
            {
                Use(Able1);
            }

            if (Input.GetKeyDown(KeyCode.Q) && (number2 >= 1 || Able2 == "Stray"))
            {
                Use(Able2);
            }
         


        }




    }



    public void Use(string able) {




        rc = RayController.rc;
        cc = gameObject.GetComponent<CameraController>();
        if (able.Equals("Phantom")) {
            cc.UpDraft();
            
        }

        if (able.Equals("Flash")) {
            cc.Flash();
            Spend(1, 1);

        }
        if (able.Equals("Stray"))
        {
            cc.Stray();
            

        }
        if (able.Equals("Whiteout")) {
            cc.Smoke();
            Spend(2, 1);

        }
        if (able.Equals("Cat"))
        {
            cc.Cat();
           
        }
        if (able.Equals("BlueLight")) {
            cc.BlueLight();
            

        }
        if (able.Equals("Diable")) {
            cc.RireDuDiable();
            Spend(2, 1);

        }
        if (able.Equals("Coward")) {
            cc.Coward();
            

        }
        if (able.Equals("Kamaitachi"))
        {
            cc.Kamaitachi();


        }
        if (able.Equals("Yor")) {
            rc.Yor();
            Spend(1,1);

        }
        if (able.Equals("Stradarts")) {
            cc.StraDarts();
            Spend(2, 1);

        }
        if (able.Equals("BlackBell")) {
            rc.BlackBell();
            Spend(1, 1);

        }
        if (able.Equals("Wolf")) {
            cc.Wolf();
            Spend(1, 1);

        }
    
        if (able.Equals("Eagle")) {
            cc.Eagle();
            Spend(2, 1);

        }
        if (able.Equals("Boostio")) {
            cc.Boostio();
           
        }
        if (able.Equals("Aqua"))
        {
            cc.Aqua();
           
        }
        if (able.Equals("Katarina"))
        {
            cc.C4();

        }
        if (able.Equals("Arte"))
        {
            cc.Arte();
            Spend(1, 1);

        }
        if (able.Equals("Molesta"))
        {
            cc.Molesta();
            Spend(1, 1);

        }


    }

    public void Spend(int number, int cost) {
        if (number == 1)
        {
            number1 -= cost;
        }
        else {
            number2 -= cost;
        }
    }

    public void Collect(int number, int many)
    {
        if (number == 1)
        {
            number1 += many;
        }
        else
        {
            number2 += many;
        }
    }

    public string GetAbility(int number)
    {
        if(number == 1)
        {
            return Able1;
        }
        if (number == 2)
        {
            return Able2;
        }
        return null;
    }


    public void Buy(string item, int kind) {

        if (item.Contains("cancel")) {
            if (kind == 1)
            {
                Able1 = "";
                number1 = 0;
            }
            if (kind == 2)
            {
                Able2 = "";
                number2 = 0;
            }
            if (kind == 3)
            {
                Able1 = "";
                Able2 = "";
                number1 = 0;
                number2 = 0;
            }
                return;
        }

      


        if (kind == 1) {
            if (item.Equals(Able1)) {
                number1++;
            } else {
                Able1 = item;
                number1 = 1;
            }

        } else {
            if (item.Equals(Able2)) {
                number2++;
            } else {
                Able2 = item;
                number2 = 1;
            }

        }

    }

    public bool Limit(int limit, int number, string name) {
        if (number == 1) {
            return (limit > number1 && (Able1.Equals(name) || Able1.Equals("")));
        } else {
            return (limit > number2 && (Able2.Equals(name) || Able2.Equals("")));
        }
    }






}
