using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    public static ResourceManager resourcemanager = null;

    private GameObject ArteBomb;
    private GameObject BrokenSoundManager;

    // ゲームオブジェクトのリストを宣言時に初期化
    public List<string> AllGameObjects = new List<string> { "ArteBomb", "BrokenSoundManager" };

    // ゲームオブジェクトのリストを宣言時に初期化
    public List<string> LoadedGameObjects = new List<string>();



    public bool HasLoadedAll = false;

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
            LoadedGameObjects.Add("ArteBomb");
        }
        if (BrokenSoundManager == null)
        {
            BrokenSoundManager = Resources.Load<GameObject>("BrokenSoundManager");
            LoadedGameObjects.Add("BrokenSoundManager");
        }
        if(AllGameObjects == LoadedGameObjects)
        {
            HasLoadedAll = true;
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
