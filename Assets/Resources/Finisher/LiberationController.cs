using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiberationController : MonoBehaviour
{
    private float delay = 3.5f; // �ҋ@���ԁi�b�j
    public float moveSpeed = 3.0f; // �ړ��X�s�[�h�i�ύX�\�j

    public Vector3 moveOffset = new Vector3(0, 500.0f, 0); // �ړ���
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float elapsedTime = 0.0f;
    private bool isMoving = false;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + moveOffset;

        // �w�莞�Ԍ�Ɉړ����J�n
        Invoke("StartMoving", delay);
    }

    void Update()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;

            // �ړ��X�s�[�h�Ɋ�Â��Đi�s�������v�Z
            float distanceCovered = moveSpeed * elapsedTime; // �ړ�����
            float t = Mathf.Clamp01(distanceCovered / moveOffset.magnitude); // ��Ԃ̐i�s����

            // �ɂ₩�Ɉʒu���Ԃ��Ĉړ�
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            // �ړ��������������~
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
