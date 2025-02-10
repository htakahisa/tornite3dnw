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
        // 二つのリストの要素が一致しているかを確認
        if (AreListsEqual(AllGameObjects, LoadedGameObjects))
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
    // リストの比較を行うメソッド
    bool AreListsEqual(List<string> list1, List<string> list2)
    {
        // リストのサイズが異なれば一致しない
        if (list1.Count != list2.Count)
            return false;

        // 要素ごとに一致しているかを確認
        for (int i = 0; i < list1.Count; i++)
        {
            if (!list1[i].Equals(list2[i]))
            {
                return false;
            }
        }

        return true;
    }

}
