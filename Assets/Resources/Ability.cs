using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ability : MonoBehaviourPun {

    private PhaseManager pm;
    private GameObject pmo;

    CameraController cc;
    RayController rc;

    string Able1 = "";
    string Able2 = "";
    public int number1 = 0;
    public int number2 = 0;

    

    // Start is called before the first frame update
    void Awake() {
        
    }

    // Update is called once per frame
    void Update() {

        if (pm == null) {
            pmo = GameObject.FindWithTag("Manager");
            pm = pmo.GetComponent<PhaseManager>();
        }
        if (pm.GetPhase() == null) {
            return;
        }

        if (pm.GetPhase().Equals("Buy")) {
            return;
        }


        if (Input.GetKeyDown(KeyCode.E) && number1 >= 1) {
            Use(Able1);
        }

        if (Input.GetKeyDown(KeyCode.Q) && number2 >= 1) {
            Use(Able2);
        }


    }

    public void Use(string able) {




        rc = RayController.rc;
        cc = gameObject.GetComponent<CameraController>();
        if (able.Equals("updraft2")) {
            cc.UpDraft();
            
        }

        if (able.Equals("flash1")) {
            cc.Flash();
            Spend(1);

        }

        if (able.Equals("straychild2")) {
            cc.Stray();
            Spend(2);

        }

        if (able.Equals("whiteout2")) {
            cc.Smoke();
            Spend(2);

        }
        if (able.Equals("bluelight2")) {
            cc.BlueLight();
            Spend(2);

        }
        if (able.Equals("diable1")) {
            cc.RireDuDiable();
            Spend(1);

        }
        if (able.Equals("coward2")) {
            cc.Coward();
            

        }
        if (able.Equals("yor1")) {
            rc.Yor();
            Spend(1);

        }
        if (able.Equals("stradarts2")) {
            cc.StraDarts();
            Spend(2);

        }
        if (able.Equals("blackbell1")) {
            rc.BlackBell();
            Spend(1);

        }
        if (able.Equals("wolf1")) {
            cc.Wolf();
            Spend(1);

        }
        if (able.Equals("eagle2")) {
            cc.Eagle();
            Spend(2);

        }
        if (able.Equals("boostio1")) {
            cc.Boostio();
           
        }
        if (able.Equals("kamaitachi1")) {
            cc.Kamaitachi();
            Spend(1);

        }
        if (able.Equals("aqua2"))
        {
            cc.Aqua();
            

        }
    }

    public void Spend(int number) {
        if (number == 1)
        {
            number1--;
        }
        else { 
            number2--;
        }
    }

    public void Collect(int number)
    {
        if (number == 1)
        {
            number1++;
        }
        else
        {
            number2++;
        }
    }



    public void Buy(string item) {

        if (item.Contains("cancel")) {
            Able1 = "";
            Able2 = "";
            number1 = 0;
            number2 = 0;
            return;
        }


        if (item.Contains("1")) {
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
