using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarStatus : MonoBehaviour
{

    [SerializeField]
    private float Speed;

    [SerializeField]
    private bool CanBuyPhase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetSpeed()
    {
        return Speed;
    }

    public bool GetCanBuyPhase()
    {
        return CanBuyPhase;
    }
}
