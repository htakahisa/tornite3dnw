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
            Destroy(gameObject); // �����̃C���X�^���X������΁A�V�������̂�j��
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // �I�u�W�F�N�g��j�����Ȃ�
        audioSource = gameObject.AddComponent<AudioSource>();

    }

    void Start()
    {
        // ����̃V�[���ɉ����� BGM ���Đ�
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
        // ��~�������V�[�����̏������`�F�b�N
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
        if (audioSource == null || audioSource.clip == clip) return; // �����ȂȂ牽�����Ȃ�

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = true; // �K�v�Ȃ烋�[�v�Đ�
        audioSource.Play();
    }

    private void StopBGM()
    {
        if (audioSource != null)
        {
            audioSource.Stop(); // BGM ��~
        }
    }

    public static LoungeBgmManager GetInstance()
    {
        // �C���X�^���X���܂��Ȃ��ꍇ�͐���
        if (instance == null)
        {
            GameObject bgmManagerObject = new GameObject("LoungeBgmManager");
            instance = bgmManagerObject.AddComponent<LoungeBgmManager>();
        }

        return instance;
    }
}
