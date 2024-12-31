using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogToScreen : MonoBehaviour
{

    public Text logText;  // UI��Text�R���|�[�l���g���Q��
    private List<string> logMessages = new List<string>();  // ���O���b�Z�[�W��ێ����郊�X�g
    private const int maxLogLines = 10;  // �\������ő�s��

    private void OnEnable() {
        Application.logMessageReceived += HandleLog; // ���O���b�Z�[�W����M���邽�߂̃C�x���g�o�^
    }

    private void OnDisable() {
        Application.logMessageReceived -= HandleLog; // �C�x���g�̉���
    }

    private void HandleLog(string logString, string stackTrace, LogType type) {
        // ���O���b�Z�[�W��ǉ�
        logMessages.Add(logString);

        // �ő�s���𒴂����ꍇ�A�ŌẪ��b�Z�[�W���폜
        if (logMessages.Count > maxLogLines) {
            logMessages.RemoveAt(0); // �ŌẪ��b�Z�[�W���폜
        }

        UpdateLogText();  // �e�L�X�g���X�V���郁�\�b�h���Ăяo��
    }

    private void UpdateLogText() {
        logText.text = string.Join("\n", logMessages);  // UI��Text�ɍŐV�̃��O��\��
    }
}
