using System;
using System.IO;
using UnityEngine;

[Serializable]
public class KeySettings
{
    public KeyCode KnifeKey = KeyCode.Alpha3;
    public KeyCode MapKey = KeyCode.Alpha1;
    public KeyCode MainWeaponKey = KeyCode.Tab;
    public KeyCode SubWeaponKey = KeyCode.Tab;
}

// 🔹 JSON で文字列として保存するためのクラス
[Serializable]
public class KeySettingsData
{
    public string KnifeKey;
    public string MapKey;
    public string MainWeaponKey;
    public string SubWeaponKey;
}

public class KeyController : MonoBehaviour
{
    private static KeyController _instance;
    public static KeyController Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject("KeyController");
                _instance = singletonObject.AddComponent<KeyController>();
                DontDestroyOnLoad(singletonObject);
            }
            return _instance;
        }
    }

    private const string FILE_NAME = "keysettings.json";
    private KeySettings keySettings;

    public KeySettings Settings => keySettings; // 🔹 他のスクリプトからアクセスできる

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadSettings()
    {
        string path = GetFilePath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            KeySettingsData data = JsonUtility.FromJson<KeySettingsData>(json);

            keySettings = new KeySettings
            {
                KnifeKey = Enum.Parse<KeyCode>(data.KnifeKey),
                MapKey = Enum.Parse<KeyCode>(data.MapKey),
                MainWeaponKey = Enum.Parse<KeyCode>(data.MainWeaponKey),
                SubWeaponKey = Enum.Parse<KeyCode>(data.SubWeaponKey),
            };
        }
        else
        {
            keySettings = new KeySettings();
            SaveSettings();
        }
    }

    private void SaveSettings()
    {
        KeySettingsData data = new KeySettingsData
        {
            KnifeKey = keySettings.KnifeKey.ToString(),
            MapKey = keySettings.MapKey.ToString(),
            MainWeaponKey = keySettings.MainWeaponKey.ToString(),
            SubWeaponKey = keySettings.SubWeaponKey.ToString(),
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetFilePath(), json);
    }

    private string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, FILE_NAME);
    }
}
