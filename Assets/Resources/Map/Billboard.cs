using UnityEngine;

public class Billboard : MonoBehaviour {
    void Update() {
        // カメラの方向を取得
        Vector3 cameraDirection = Camera.main.transform.position - transform.position;

        // オブジェクトの向きをカメラの方向に設定
        transform.rotation = Quaternion.LookRotation(cameraDirection);
    }
}
