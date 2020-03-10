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
    public LocalFlag defeatedEnemyList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && enemyUnit && !(enemyTeam.GetComponent<UnitTeam>().defeated)) {
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
