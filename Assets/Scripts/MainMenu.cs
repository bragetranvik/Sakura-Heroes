using System;
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
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Flag"));
        SceneManager.LoadScene("StartHub");
        playerGO.GetComponent<SpriteRenderer>().enabled = true;
        playerGO.GetComponent<Transform>().position = new Vector3(4.5f, -0.3f, 0f);
    }

    private void DontDestroyOnLoad() {
        throw new NotImplementedException();
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
