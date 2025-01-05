using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyFinisherController : MonoBehaviour
{

    [SerializeField]
    public AudioClip audioClip;



    [SerializeField]
    private AudioSource blast;

    // Start is called before the first frame update
    void Start()
    {
        playDelayedGunSound(1f);
        playDelayedGunSound(1.3f);
        playDelayedGunSound(1.9f);
        playDelayedGunSound(2.6f);
        playDelayedGunSound(3.1f);
        playDelayedGunSound(3.6f);
        playDelayedGunSound(4.4f);


        blast.PlayDelayed(4f);
    }


    public void playDelayedGunSound(float delay)
    {
        Invoke(nameof(PlaySound), delay);
    }

    private void PlaySound()
    {
        blast.PlayOneShot(audioClip);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
