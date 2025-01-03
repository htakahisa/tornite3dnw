using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiberationController : MonoBehaviour
{
    private float delay = 3.5f; // 待機時間（秒）
    public float moveSpeed = 3.0f; // 移動スピード（変更可能）

    public Vector3 moveOffset = new Vector3(0, 500.0f, 0); // 移動量
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float elapsedTime = 0.0f;
    private bool isMoving = false;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + moveOffset;

        // 指定時間後に移動を開始
        Invoke("StartMoving", delay);
    }

    void Update()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;

            // 移動スピードに基づいて進行割合を計算
            float distanceCovered = moveSpeed * elapsedTime; // 移動距離
            float t = Mathf.Clamp01(distanceCovered / moveOffset.magnitude); // 補間の進行割合

            // 緩やかに位置を補間して移動
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            // 移動が完了したら停止
            if (t >= 1.0f)
            {
                isMoving = false;
            }
        }
    }

    void StartMoving()
    {
        isMoving = true;
    }
}
