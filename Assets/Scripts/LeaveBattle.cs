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

    public void ChangeSceneToPrevious() {
        SetBattleSystem();
        SetPlayerTeam();
        SetPlayerAndEnemyActive();
        SetEnemyTeam();
        SetEnemyDefeated(enemyTeam);
        Destroy(enemyTeamGO);
        SetSceneToLoad();
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ChangeSceneToStartHub() {
        SetBattleSystem();
        SetPlayerTeam();
        SetPlayerAndEnemyActive();
        SetEnemyTeam();
        Destroy(enemyTeamGO);
        SetSceneToLoad();
        SceneManager.LoadScene("StartHub");
    }

    private void SetPlayerTeam() {
        playerTeam = battleSystem.friendlyTeamGO.GetComponent<UnitTeam>();
    }
    private void SetEnemyTeam() {
        enemyTeamGO = battleSystem.enemyTeamGO;
        enemyTeam = enemyTeamGO.GetComponent<UnitTeam>();
    }
    private void SetSceneToLoad() {
        sceneToLoad = playerTeam.previousScene;
    }
    private void SetBattleSystem() {
        battleSystem = FindObjectOfType<BattleSystem>();
    }

    private void SetEnemyDefeated(UnitTeam enemyTeamGO) {
        enemyTeam.defeated = true;
        enemyTeam.AddToDefeatedList();
    }

    private void SetPlayerAndEnemyActive() {
        battleSystem.EnablePlayerAndEnemyObjects();
    }
}
