using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTransformSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip walk;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayTransSound(string sound)
    {
        if (sound == "walk")
        {
            AudioSource aS = GetComponent<AudioSource>();
            aS.PlayOneShot(walk);
        }
    }
}
