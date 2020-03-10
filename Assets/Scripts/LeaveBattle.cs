using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveBattle : MonoBehaviour
{
    public BattleSystem battleSystem;
    public GameObject enemyTeamGO;
    public UnitTeam playerTeam, enemyTeam;
    public string sceneToLoad;

    public void changeSceneToPrevious() {
        setBattleSystem();
        setPlayerTeam();
        setPlayerAndEnemyActive();
        setEnemyTeam();
        setEnemyDefeated(enemyTeam);
        Destroy(enemyTeamGO);
        setSceneToLoad();
        SceneManager.LoadScene(sceneToLoad);
    }

    public void changeSceneToStartHub() {
        setBattleSystem();
        setPlayerTeam();
        setPlayerAndEnemyActive();
        setEnemyTeam();
        Destroy(enemyTeamGO);
        setSceneToLoad();
        SceneManager.LoadScene("StartHub");
    }

    private void setPlayerTeam() {
        playerTeam = battleSystem.friendlyTeamGO.GetComponent<UnitTeam>();
    }
    private void setEnemyTeam() {
        enemyTeamGO = battleSystem.enemyTeamGO;
        enemyTeam = enemyTeamGO.GetComponent<UnitTeam>();
    }
    private void setSceneToLoad() {
        sceneToLoad = playerTeam.previousScene;
    }
    private void setBattleSystem() {
        battleSystem = FindObjectOfType<BattleSystem>();
    }

    private void setEnemyDefeated(UnitTeam enemyTeamGO) {
        enemyTeam.deafeted = true;
    }

    private void setPlayerAndEnemyActive() {
        battleSystem.enablePlayerAndEnemyObjects();
    }
}
