using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogToScreen : MonoBehaviour
{

    public Text logText;  // UIのTextコンポーネントを参照
    private List<string> logMessages = new List<string>();  // ログメッセージを保持するリスト
    private const int maxLogLines = 10;  // 表示する最大行数

    private void OnEnable() {
        Application.logMessageReceived += HandleLog; // ログメッセージを受信するためのイベント登録
    }

    private void OnDisable() {
        Application.logMessageReceived -= HandleLog; // イベントの解除
    }

    private void HandleLog(string logString, string stackTrace, LogType type) {
        // ログメッセージを追加
        logMessages.Add(logString);

        // 最大行数を超えた場合、最古のメッセージを削除
        if (logMessages.Count > maxLogLines) {
            logMessages.RemoveAt(0); // 最古のメッセージを削除
        }

        UpdateLogText();  // テキストを更新するメソッドを呼び出す
    }

    private void UpdateLogText() {
        logText.text = string.Join("\n", logMessages);  // UIのTextに最新のログを表示
    }
}
