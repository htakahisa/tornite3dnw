using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
     

        // �G�f�B�^�[�̏ꍇ�A�v���C���[�h���I��
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    
        Application.Quit();
#endif
    }




}
