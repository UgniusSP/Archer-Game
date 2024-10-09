using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class MenuManager : MonoBehaviour
{
    public void ChangeSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
