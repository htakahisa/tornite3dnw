using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFlashEffect : MonoBehaviour {

    [SerializeField]
    private GameObject effect;

    [SerializeField]
    private GameObject stun;

    private Image image;

    public static PlayerFlashEffect pfe;

    private bool IsStun = false;

    public void ApplyFlash(float duration) {
        StartCoroutine(FlashRoutine(duration));

    }

    private void Awake() {
        
        image = stun.GetComponent<Image>();
        pfe = this;
    }

    private IEnumerator FlashRoutine(float duration) {
        effect.SetActive(true); // �t���b�V���I�[�o�[���C��\��
        yield return new WaitForSeconds(duration); // �w�肳�ꂽ���ԑ҂�
        effect.SetActive(false); // �t���b�V���I�[�o�[���C���\��
    }

    public void Stun(Color color) {
        if (IsStun) {
            return;
        }
        stun.SetActive(true); // �t���b�V���I�[�o�[���C��\��
        image.color = color;
        IsStun = true;
        Camera.main.GetComponent<Camera>().farClipPlane = 5;

    }

    public void Distun() {
        stun.SetActive(false); // �t���b�V���I�[�o�[���C��\��
        IsStun = false;
        Camera.main.GetComponent<Camera>().farClipPlane = 1000;
    }
}
