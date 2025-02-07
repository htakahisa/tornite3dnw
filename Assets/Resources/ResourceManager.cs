using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    public static ResourceManager resourcemanager = null;

    private GameObject ArteBomb;
    private GameObject BrokenSoundManager;

    // Start is called before the first frame update
    void Awake()
    {
        if (resourcemanager == null)
        {
            resourcemanager = this;
            DontDestroyOnLoad(gameObject);

        }
   

        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ArteBomb == null)
        {
            ArteBomb = Resources.Load<GameObject>("ArteBomb");
        }
        if (BrokenSoundManager == null)
        {
            BrokenSoundManager = Resources.Load<GameObject>("BrokenSoundManager");
        }

    }

    public GameObject GetObject(string name)
    {
        if (name == "ArteBomb") {
            return ArteBomb;
        }
        if (name == "BrokenSoundManager")
        {
            return BrokenSoundManager;
        }

        return null;
        
    }


}
