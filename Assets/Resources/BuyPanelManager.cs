using UnityEngine;
using UnityEngine.UI;

public class BuyPanelManager : MonoBehaviour
{

    private void Awake()
    {
    }

    void Start()
    {      
        AdjustSpriteOpacity("");
    }

    public void AdjustSpriteOpacity(string name)
    {
        // すべての子オブジェクトを取得
        foreach (Transform child in transform)
        {
            Image spriteRenderer = child.GetComponent<Image>();

            if (spriteRenderer != null)
            {
                // 指定した名前のオブジェクトがあった場合
                if (child.gameObject.name == name)
                {
                    // 透明度を0.5に設定
                    Color color = spriteRenderer.color;
                    color.a = 0.5f;
                    spriteRenderer.color = color;
                }
                else
                {
                    // 透明度を1に設定
                    Color color = spriteRenderer.color;
                    color.a = 1f;
                    spriteRenderer.color = color;
                }
            }
        }
    }
}
