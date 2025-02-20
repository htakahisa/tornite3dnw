using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTag : MonoBehaviour
{

    public static EnemyTag enemytag;

    // Start is called before the first frame update
    void Awake()
    {
        enemytag = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
