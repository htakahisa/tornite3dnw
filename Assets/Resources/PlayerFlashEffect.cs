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
        effect.SetActive(true); // フラッシュオーバーレイを表示
        yield return new WaitForSeconds(duration); // 指定された時間待つ
        effect.SetActive(false); // フラッシュオーバーレイを非表示
    }

    public void Stun(Color color) {
        if (IsStun) {
            return;
        }
        stun.SetActive(true); // フラッシュオーバーレイを表示
        image.color = color;
        IsStun = true;
        Camera.main.GetComponent<Camera>().farClipPlane = 5;

    }

    public void Distun() {
        stun.SetActive(false); // フラッシュオーバーレイを表示
        IsStun = false;
        Camera.main.GetComponent<Camera>().farClipPlane = 1000;
    }
}
