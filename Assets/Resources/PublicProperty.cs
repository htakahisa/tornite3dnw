using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PublicProperty : MonoBehaviourPun {

    // ユーザー数
    private int userCount;
    private int round;


    [PunRPC]//RPCを使って呼べようにするにはこの属性をつける(これが無いと呼べない)
    void setUserCount(int i) {
        this.userCount = i;
    }

    [PunRPC]//RPCを使って呼べようにするにはこの属性をつける(これが無いと呼べない)
    void addUserCount() {
        this.userCount = this.userCount + 1;
    }

    
  

    public int getUserCount() {
        return this.userCount;
    }

 

}
