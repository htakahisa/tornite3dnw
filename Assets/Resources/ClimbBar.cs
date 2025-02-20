using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbBar : MonoBehaviour
{
    [SerializeField]
    private Transform topbar;

    [SerializeField]
    private Transform bottombat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetTopBar()
    {
        return topbar;
    }
    public Transform GetBottomBar()
    {
        return bottombat;
    }

}
