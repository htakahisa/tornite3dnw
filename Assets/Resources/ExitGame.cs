using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
     

        // エディターの場合、プレイモードを終了
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    
        Application.Quit();
#endif
    }




}
