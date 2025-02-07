using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip brokense;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnDestroy()
    {
        GameObject BrokenSound = Instantiate(ResourceManager.resourcemanager.GetObject("BrokenSoundManager"),transform.position,transform.rotation);
        BrokenSoundManager bsm = BrokenSound.GetComponent<BrokenSoundManager>();
        bsm.PlayBrokenSound(brokense);
    }


}
