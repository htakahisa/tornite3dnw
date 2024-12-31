using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Invoke("DestroyThis", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
