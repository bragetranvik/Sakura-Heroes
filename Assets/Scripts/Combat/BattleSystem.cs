using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform friendly1BattleStation;
    public Transform friendly2BattleStation;
    public Transform friendly3BattleStation;

    public Transform enemy1BattleStation;
    public Transform enemy2BattleStation;
    public Transform enemy3BattleStation;

    Unit playerUnit1;
    Unit playerUnit2;
    Unit playerUnit3;
    Unit enemyUnit1;
    Unit enemyUnit2;
    Unit enemyUnit3;

    public Text dialogueText;

    public StatusHUD FriendlyStatus;
    public StatusHUD EnemyStatus;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        GameObject friendly1GO = Instantiate(playerPrefab, friendly1BattleStation);
        playerUnit1 = friendly1GO.GetComponent<Unit>();
        GameObject friendly2GO = Instantiate(playerPrefab, friendly2BattleStation);
        playerUnit2 = friendly2GO.GetComponent<Unit>();
        GameObject friendly3GO = Instantiate(playerPrefab, friendly3BattleStation);
        playerUnit3 = friendly3GO.GetComponent<Unit>();


        GameObject enemy1GO = Instantiate(enemyPrefab, enemy1BattleStation);
        enemyUnit1 = enemy1GO.GetComponent<Unit>();
        GameObject enemy2GO = Instantiate(enemyPrefab, enemy2BattleStation);
        enemyUnit2 = enemy2GO.GetComponent<Unit>();
        GameObject enemy3GO = Instantiate(enemyPrefab, enemy3BattleStation);
        enemyUnit3 = enemy3GO.GetComponent<Unit>();


        dialogueText.text = "Fight!";

        FriendlyStatus.SetHUD(playerUnit1, playerUnit2, playerUnit3);
        EnemyStatus.SetHUD(enemyUnit1, enemyUnit2, enemyUnit3);

    }
}
