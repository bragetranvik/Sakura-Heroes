using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBattle : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject player;
    public GameObject enemyTeam;
    public bool enemyUnit = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && enemyUnit) {
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(enemyTeam);
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void StartBattle() {
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(enemyTeam);
        SceneManager.LoadScene(sceneToLoad);
    }
}
