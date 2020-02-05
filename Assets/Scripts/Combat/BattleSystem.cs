﻿using System;
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

    private Unit target;
    private Unit unitsTurn;

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
                    unitsTurn = friendlyUnit1;
                    FriendlyTurn();
                    yield return waitForKeyPress(KeyCode.Space);
                    if(IsAllEnemiesDead()) {
                        state = BattleState.WON;
                    } else {
                        state = BattleState.ENEMY1TURN;
                    }
                    break;

                case BattleState.ENEMY1TURN:
                    yield return new WaitForSeconds(1f);
                    unitsTurn = enemyUnit1;
                    SimpleEnemyAI();
                    Debug.Log("1");
                    yield return new WaitForSeconds(2f);
                    if (IsAllFriendlyDead()) {
                        state = BattleState.LOST;
                    } else {
                        state = BattleState.FRIENDLY2TURN;
                    }
                    break;

                case BattleState.FRIENDLY2TURN:
                    unitsTurn = friendlyUnit2;
                    FriendlyTurn();
                    yield return waitForKeyPress(KeyCode.Space);
                    if (IsAllEnemiesDead()) {
                        state = BattleState.WON;
                    }
                    else {
                        state = BattleState.ENEMY2TURN;
                    }
                    break;

                case BattleState.ENEMY2TURN:
                    yield return new WaitForSeconds(1f);
                    unitsTurn = enemyUnit2;
                    SimpleEnemyAI();
                    Debug.Log("2");
                    yield return new WaitForSeconds(2f);
                    if (IsAllFriendlyDead()) {
                        state = BattleState.LOST;
                    }
                    else {
                        state = BattleState.FRIENDLY3TURN;
                    }
                    break;

                case BattleState.FRIENDLY3TURN:
                    unitsTurn = friendlyUnit3;
                    FriendlyTurn();
                    yield return waitForKeyPress(KeyCode.Space);
                    if (IsAllEnemiesDead()) {
                        state = BattleState.WON;
                    }
                    else {
                        state = BattleState.ENEMY3TURN;
                    }
                    break;

                case BattleState.ENEMY3TURN:
                    yield return new WaitForSeconds(1f);
                    unitsTurn = enemyUnit3;
                    Debug.Log("3");
                    SimpleEnemyAI();
                    yield return new WaitForSeconds(2f);
                    if (IsAllFriendlyDead()) {
                        state = BattleState.LOST;
                    }
                    else {
                        state = BattleState.FRIENDLY1TURN;
                    }
                    break;

                case BattleState.LOST:
                    ongoingBattle = false;
                    EndBattle();
                    break;

                case BattleState.WON:
                    ongoingBattle = false;
                    EndBattle();
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
    private void FriendlyTurn() {
        dialogueText.text = unitsTurn.unitName + "'s turn choose an action:";
    }

    //Should take in a varible which is the ability
    public void OnAbilityButton1() {
        if ((state == BattleState.FRIENDLY1TURN) || (state == BattleState.FRIENDLY2TURN) || (state == BattleState.FRIENDLY3TURN)) {
            StartCoroutine(DummyAttack());
        }
    }

    private IEnumerator DummyAttack() {
        //Choose a random target. Just for testing
        ChooseRandomTarget(true);
        bool targetIsDead = target.TakeDamage(unitsTurn.attack);

        EnemyStatus.SetHPandMP(enemyUnit1, enemyUnit2, enemyUnit3);
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

    private void EndBattle() {
        if(state == BattleState.WON) {
            dialogueText.text = "You won the battle!";
        } else if(state == BattleState.LOST) {
            dialogueText.text = "You were defeted.";
        }
    }

    private void SimpleEnemyAI() {
        Debug.Log("Hello");
        dialogueText.text = unitsTurn.unitName + " attacks!";
        ChooseRandomTarget(false);

        //yield return new WaitForSeconds(1f);

        //Return true if target's health is < 0.
        bool isDead = target.TakeDamage(unitsTurn.attack);

        FriendlyStatus.SetHPandMP(friendlyUnit1, friendlyUnit2, friendlyUnit3);
        //yield return new WaitForSeconds(2f);

        target.isDead = isDead;
    }

    //Choose a random target depedning on a roll 1-3.
    private void ChooseRandomTarget(bool friendlyIsAttacking) {
        int randomTarget = RandomNumber(1, 4);

        if (!friendlyIsAttacking) {
            if (randomTarget.Equals(1)) {
                target = friendlyUnit1;
            }
            else if (randomTarget.Equals(2)) {
                target = friendlyUnit2;
            }
            else {
                target = friendlyUnit3;
            }
        }

        if (friendlyIsAttacking) {
            if (randomTarget.Equals(1)) {
                target = enemyUnit1;
            }
            else if (randomTarget.Equals(2)) {
                target = enemyUnit2;
            }
            else {
                target = enemyUnit3;
            }
        }
    }

    private int RandomNumber(int min, int max) {
        var random = UnityEngine.Random.Range(min, max);
        return Convert.ToInt32(random);
    }


    private IEnumerator waitForKeyPress(KeyCode key) {
        bool done = false;
        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
            if (Input.GetKeyDown(key)) {
                done = true; // breaks the loop
            }
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }

        // now this function returns
    }

}
