using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenSoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBrokenSound(AudioClip ac)
    {
        GetComponent<AudioSource>().PlayOneShot(ac);
    }
}
