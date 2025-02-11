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
    public KeyCode AquaCloseMap = KeyCode.Q;
}

// 🔹 JSON で文字列として保存するためのクラス
[Serializable]
public class JsonSettingsData
{
    public string KnifeKey;
    public string MapKey;
    public string MainWeaponKey;
    public string SubWeaponKey;
    public string AquaCloseMap;
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
    private KeySettings keySettings = new KeySettings();

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
            JsonSettingsData data = JsonUtility.FromJson<JsonSettingsData>(json);

            keySettings.KnifeKey = data.KnifeKey == null ? keySettings.KnifeKey : Enum.Parse<KeyCode>(data.KnifeKey);
            keySettings.MapKey = data.MapKey == null ? keySettings.MapKey : Enum.Parse<KeyCode>(data.MapKey);
            keySettings.MainWeaponKey = data.MainWeaponKey == null ? keySettings.MainWeaponKey : Enum.Parse<KeyCode>(data.MainWeaponKey);
            keySettings.SubWeaponKey = data.SubWeaponKey == null ? keySettings.SubWeaponKey : Enum.Parse<KeyCode>(data.SubWeaponKey);
            keySettings.AquaCloseMap = data.AquaCloseMap == null ? keySettings.AquaCloseMap : Enum.Parse<KeyCode>(data.AquaCloseMap);

        }
        else
        {
            keySettings = new KeySettings();
            SaveSettings();
        }
    }

    private void SaveSettings()
    {
        JsonSettingsData data = new JsonSettingsData
        {
            KnifeKey = keySettings.KnifeKey.ToString(),
            MapKey = keySettings.MapKey.ToString(),
            MainWeaponKey = keySettings.MainWeaponKey.ToString(),
            SubWeaponKey = keySettings.SubWeaponKey.ToString(),
            AquaCloseMap = keySettings.AquaCloseMap.ToString(),
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetFilePath(), json);
    }

    private string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, FILE_NAME);
    }
}
