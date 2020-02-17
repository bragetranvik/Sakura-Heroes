using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("StartHub");
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
