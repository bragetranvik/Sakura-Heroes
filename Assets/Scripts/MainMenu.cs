using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject playerGO;

    void Start()
    {
        playerGO.GetComponent<SpriteRenderer>().enabled = false;
        playerGO.GetComponent<Transform>().position = new Vector3(9f, 9f, 0f);
    }

    void Update()
    {

    }
    
    public void NewGame()
    {
        SceneManager.LoadScene("StartHub");
        playerGO.GetComponent<SpriteRenderer>().enabled = true;
        playerGO.GetComponent<Transform>().position = new Vector3(4.5f, -0.3f, 0f);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
