using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBattle : MonoBehaviour
{
    public string sceneToLoad;
    private GameObject player;
    public GameObject enemyTeam;
    public bool enemyUnit = true;
    public LocalFlag defeatedEnemyList;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && enemyUnit) {
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(enemyTeam);
            DontDestroyOnLoad(defeatedEnemyList);
            player.GetComponent<UnitTeam>().SetPreviousScene();
            enemyTeam.GetComponent<UnitTeam>().SetPreviousScene();
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void StartBattle() {
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(enemyTeam);
        DontDestroyOnLoad(defeatedEnemyList);
        player.GetComponent<UnitTeam>().SetPreviousScene();
        enemyTeam.GetComponent<UnitTeam>().SetPreviousScene();
        SceneManager.LoadScene(sceneToLoad);
    }
}