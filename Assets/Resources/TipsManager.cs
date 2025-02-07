using UnityEngine;

public class TipsManager : MonoBehaviour
{
    // 親オブジェクトの子オブジェクトを格納する配列
    private GameObject[] children;
    private GameObject currentChild;

    void Start()
    {
        // 親オブジェクトの子オブジェクトをすべて取得
        children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
        }

        // 最初にランダムな子オブジェクトを表示
        ShowRandomChild();
    }

    void Update()
    {
        // 左クリックでランダムな子オブジェクトに切り替え
        if (Input.GetMouseButtonDown(0))  // 0は左クリック
        {
            ShowRandomChild();
        }
    }

    // ランダムな子オブジェクトを表示
    void ShowRandomChild()
    {
        // すべての子オブジェクトを非表示にする
        foreach (var child in children)
        {
            child.SetActive(false);
        }

        // ランダムにインデックスを選択
        int randomIndex = Random.Range(0, children.Length);

        // ランダムな子オブジェクトを表示
        currentChild = children[randomIndex];
        currentChild.SetActive(true);
    }
}
