using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PublicProperty : MonoBehaviourPun {

    // ���[�U�[��
    private int userCount;
    private int round;


    [PunRPC]//RPC���g���ČĂׂ悤�ɂ���ɂ͂��̑���������(���ꂪ�����ƌĂׂȂ�)
    void setUserCount(int i) {
        this.userCount = i;
    }

    [PunRPC]//RPC���g���ČĂׂ悤�ɂ���ɂ͂��̑���������(���ꂪ�����ƌĂׂȂ�)
    void addUserCount() {
        this.userCount = this.userCount + 1;
    }

    
  

    public int getUserCount() {
        return this.userCount;
    }

 

}
