using UnityEngine;
using UnityEngine.SceneManagement;

public class LoungeBgmManager : MonoBehaviour
{
    private static LoungeBgmManager instance;
    private AudioSource audioSource;

    private AudioClip defaultBGM;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // 既存のインスタンスがあれば、新しいものを破棄
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // オブジェクトを破棄しない
        audioSource = gameObject.AddComponent<AudioSource>();

    }

    void Start()
    {
        // 初回のシーンに応じて BGM を再生
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "lounge")
        {
            PlayBGM(defaultBGM, 0.25f);
        }
    }

    public void SetDefaultBGM(AudioClip clip)
    {
        defaultBGM = clip;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 停止したいシーン名の条件をチェック
        if (scene.name == "Needless" || scene.name == "DuelLand" || scene.name == "Secondhouse")
        {
            StopBGM();
        }
        else if (scene.name == "lounge")
        {
            PlayBGM(defaultBGM, 0.25f);
        }
    }

    private void PlayBGM(AudioClip clip, float volume)
    {
        if (audioSource == null || audioSource.clip == clip) return; // 同じ曲なら何もしない

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = true; // 必要ならループ再生
        audioSource.Play();
    }

    private void StopBGM()
    {
        if (audioSource != null)
        {
            audioSource.Stop(); // BGM 停止
        }
    }

    public static LoungeBgmManager GetInstance()
    {
        // インスタンスがまだない場合は生成
        if (instance == null)
        {
            GameObject bgmManagerObject = new GameObject("LoungeBgmManager");
            instance = bgmManagerObject.AddComponent<LoungeBgmManager>();
        }

        return instance;
    }
}
