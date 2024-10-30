using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();

    }
    public void ReloadGame()
    {
        PlatformStateController.ClearPlatformStates();
        SceneManager.LoadScene("FirstScene");
    }
}
