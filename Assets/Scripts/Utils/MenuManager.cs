using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class MenuManager : MonoBehaviour
{
    public void ChangeSceneByName(string sceneName)
    {
        if (SceneExists(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' does not exist.");
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // Quits the application in a built game
#endif
    }

    private bool SceneExists(string sceneName)
    {
        int sceneIndex = SceneManager.GetSceneByName(sceneName).buildIndex;
        return sceneIndex >= 0;
    }
}