using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, FRIENDLY1TURN, ENEMY1TURN, FRIENDLY2TURN, ENEMY2TURN, FRIENDLY3TURN, ENEMY3TURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject friendly1Prefab;
    public GameObject friendly2Prefab;
    public GameObject friendly3Prefab;
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;

    public Transform friendly1BattleStation;
    public Transform friendly2BattleStation;
    public Transform friendly3BattleStation;
    public Transform enemy1BattleStation;
    public Transform enemy2BattleStation;
    public Transform enemy3BattleStation;

    private Unit friendlyUnit1;
    private Unit friendlyUnit2;
    private Unit friendlyUnit3;
    private Unit enemyUnit1;
    private Unit enemyUnit2;
    private Unit enemyUnit3;

    public Text dialogueText;

    public StatusHUD FriendlyStatus;
    public StatusHUD EnemyStatus;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(Battle());
    }

    //Runs in a state loop until either the battle is lost or won.
    //It will sycle thought each units turn from friendly unit to enemy unit.
    //If a unit is missing or dead it will skip that unit and go to the next.
    private IEnumerator Battle() {
        bool ongoingBattle = true;

        while (ongoingBattle) {
            switch (state) {

                case BattleState.START:
                    SetupBattle();
                    yield return new WaitForSeconds(2f);
                    state = BattleState.FRIENDLY1TURN;
                    break;

                case BattleState.FRIENDLY1TURN:
                    if(IsAllEnemiesDead()) {
                        state = BattleState.WON;
                    } else {
                        state = BattleState.ENEMY1TURN;
                    }
                    break;

                case BattleState.ENEMY1TURN:
                    if(IsAllFriendlyDead()) {
                        state = BattleState.LOST;
                    } else {
                        state = BattleState.FRIENDLY2TURN;
                    }
                    break;

                case BattleState.FRIENDLY2TURN:
                    if (IsAllEnemiesDead()) {
                        state = BattleState.WON;
                    }
                    else {
                        state = BattleState.ENEMY2TURN;
                    }
                    break;

                case BattleState.ENEMY2TURN:
                    if (IsAllFriendlyDead()) {
                        state = BattleState.LOST;
                    }
                    else {
                        state = BattleState.FRIENDLY3TURN;
                    }
                    break;

                case BattleState.FRIENDLY3TURN:
                    if (IsAllEnemiesDead()) {
                        state = BattleState.WON;
                    }
                    else {
                        state = BattleState.ENEMY3TURN;
                    }
                    break;

                case BattleState.ENEMY3TURN:
                    if (IsAllFriendlyDead()) {
                        state = BattleState.LOST;
                    }
                    else {
                        state = BattleState.FRIENDLY1TURN;
                    }
                    break;

                case BattleState.LOST:
                    ongoingBattle = false;
                    break;

                case BattleState.WON:
                    ongoingBattle = false;
                    break;

                default:
                    ongoingBattle = false;
                    break;

            }
        }
    }

    private void SetupBattle()
    {
        GameObject friendly1GO = Instantiate(friendly1Prefab, friendly1BattleStation);
        friendlyUnit1 = friendly1GO.GetComponent<Unit>();
        GameObject friendly2GO = Instantiate(friendly2Prefab, friendly2BattleStation);
        friendlyUnit2 = friendly2GO.GetComponent<Unit>();
        GameObject friendly3GO = Instantiate(friendly3Prefab, friendly3BattleStation);
        friendlyUnit3 = friendly3GO.GetComponent<Unit>();


        GameObject enemy1GO = Instantiate(enemy1Prefab, enemy1BattleStation);
        enemyUnit1 = enemy1GO.GetComponent<Unit>();
        GameObject enemy2GO = Instantiate(enemy2Prefab, enemy2BattleStation);
        enemyUnit2 = enemy2GO.GetComponent<Unit>();
        GameObject enemy3GO = Instantiate(enemy3Prefab, enemy3BattleStation);
        enemyUnit3 = enemy3GO.GetComponent<Unit>();


        dialogueText.text = "Fight!";

        FriendlyStatus.SetHUD(friendlyUnit1, friendlyUnit2, friendlyUnit3);
        EnemyStatus.SetHUD(enemyUnit1, enemyUnit2, enemyUnit3);

    }

    //
    private void FriendlyTurn(GameObject friendlyPrefab, Transform friendlyBattleStation, Unit friendlyUnit) {
        dialogueText.text = friendlyUnit.name + "'s trun choose an action:";
    }

    //Should take in a varible which is the ability
    public void OnAbilityButton1(Unit enemyTarget, Unit friendlyUnitInUse) {
        if (state != BattleState.FRIENDLY1TURN)
            return;

        StartCoroutine(DummyAttack(enemyTarget, friendlyUnitInUse));
    }

    private IEnumerator DummyAttack(Unit target, Unit friendlyUnit) {
        bool targetIsDead = target.TakeDamage(friendlyUnit.attack);


        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);

        target.isDead = targetIsDead;
    }

    //Checks if all anemy units are dead.
    private bool IsAllEnemiesDead() {
        bool allEnemiesDead = false;

        if (enemyUnit1.isDead && enemyUnit2.isDead && enemyUnit3.isDead) {
            allEnemiesDead = true;
        }
        return allEnemiesDead;
    }

    //Checks if all friendly units are dead.
    private bool IsAllFriendlyDead() {
        bool allFriendlyDead = false;

        if(friendlyUnit1.isDead && friendlyUnit2.isDead && friendlyUnit3.isDead) {
            allFriendlyDead = true;
        }
        return allFriendlyDead;
    }

}
