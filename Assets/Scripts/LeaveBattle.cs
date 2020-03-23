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
        SetEnemyDefeated();
        Destroy(enemyTeamGO);
        SetSceneToLoad();
        CheckIfQuestIsCompleted(enemyTeam.teamName);
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

    private void SetEnemyDefeated() {
        enemyTeam.defeated = true;
        enemyTeam.AddToDefeatedList();
    }

    private void SetPlayerAndEnemyActive() {
        battleSystem.EnablePlayerAndEnemyObjects();
    }

    /// <summary>
    /// Find the player with tag and try to complete the quest.
    /// </summary>
    /// <param name="objective">Example the name of the enemyTeam or an area.</param>
    private void CheckIfQuestIsCompleted(string objective) {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerInventory>().CheckForCompletedQuest(objective);
    }
}
