using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrashGradation : MonoBehaviour {
    private Image image;
    private float duration = 1.0f;
    private float time = 0f;
    // Start is called before the first frame update
    void Start() {
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {


        time += Time.deltaTime;
        float t = Mathf.Clamp01(time / duration);
        image.color = Color.Lerp(Color.blue, Color.green, t);

        if (image.color == Color.green) {
            Restart();
        }
    }

    public void Restart() {
        time = 0;
    }
}
