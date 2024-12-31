using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager {


    public void causeDamage(GameObject enemy, int damage) {

        DamageCounter dmc = enemy.GetComponent<DamageCounter>();
        if (dmc != null) {
            dmc.DamageCount(damage, dmc.gameObject);
        }
        
        

    }



}
