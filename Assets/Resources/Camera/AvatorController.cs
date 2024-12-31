using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatorController : MonoBehaviour {
    private int playerNo;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    public void setPlayerNo(int no) {
        this.playerNo = no;
    }

    public int GetPlayerNo() {
        return this.playerNo;
    }




}
