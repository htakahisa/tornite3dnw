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
        // ���ׂĂ̎q�I�u�W�F�N�g���擾
        foreach (Transform child in transform)
        {
            Image spriteRenderer = child.GetComponent<Image>();

            if (spriteRenderer != null)
            {
                // �w�肵�����O�̃I�u�W�F�N�g���������ꍇ
                if (child.gameObject.name == name)
                {
                    // �����x��0.5�ɐݒ�
                    Color color = spriteRenderer.color;
                    color.a = 0.5f;
                    spriteRenderer.color = color;
                }
                else
                {
                    // �����x��1�ɐݒ�
                    Color color = spriteRenderer.color;
                    color.a = 1f;
                    spriteRenderer.color = color;
                }
            }
        }
    }
}
