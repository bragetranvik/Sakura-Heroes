using System;
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

    private bool targetHasBeenChosen = false, abilityHasBeenChosen = false;
    private Button whatTargetButtonPressed, whatAbilityButtonPressed;
    private int damageMultiplier = 1; //Just for testing

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
                    yield return WaitForPlayerAction();
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
                    yield return WaitForPlayerAction();
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
                    yield return WaitForPlayerAction();
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
        Debug.Log("Dummy Attack used");
        target.TakeDamage(unitsTurn.attack * damageMultiplier);

        EnemyStatus.SetHPandMP(enemyUnit1, enemyUnit2, enemyUnit3);
        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);
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
        dialogueText.text = unitsTurn.unitName + " attacks!";
        ChooseRandomTarget(false);

        //yield return new WaitForSeconds(1f);

        //Return true if target's health is < 0.
        target.TakeDamage(unitsTurn.attack);

        FriendlyStatus.SetHPandMP(friendlyUnit1, friendlyUnit2, friendlyUnit3);
        //yield return new WaitForSeconds(2f);
    }

    ///Choose a random target depedning on a roll 1-3.
    ///<param name="friendlyIsAttacking"> True if a friendly is attacking false if enemy is attacking.
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

    /// <summary>
    /// Choose the target depending on what target button has been pressed and set targetHasBeen to true.
    /// </summary>
    /// <param name="button"></param>
    public void OnTargetButton(Button button) {
        if (button.name.Equals("Target1Button")) {
            target = enemyUnit1;
            targetHasBeenChosen = true;
            Debug.Log("1");
            StartCoroutine(DummyAttack());
        } else if(button.name.Equals("Target2Button")) {
            target = enemyUnit2;
            targetHasBeenChosen = true;
            Debug.Log("2");
            StartCoroutine(DummyAttack());
        } else if(button.name.Equals("Target3Button")) {
            target = enemyUnit3;
            targetHasBeenChosen = true;
            Debug.Log("3");
            StartCoroutine(DummyAttack());
        }
    }

    public void OnAbilityButton(Button button) {
        if((state == BattleState.FRIENDLY1TURN) || (state == BattleState.FRIENDLY2TURN) || (state == BattleState.FRIENDLY3TURN)) {
            whatAbilityButtonPressed = button;
            abilityHasBeenChosen = true;
        }
    }

    public void WhatAbilityToUse() {
        Debug.Log("Pressed button is: " + whatAbilityButtonPressed.name);
        if(whatAbilityButtonPressed.name.Equals("Attack1Button")) {
            Debug.Log("Using ability 1");
            damageMultiplier = 1;
        } else if(whatAbilityButtonPressed.name.Equals("Attack2Button")) {
            Debug.Log("Using ability 2");
            damageMultiplier = 2;
        } else if(whatAbilityButtonPressed.name.Equals("Attack3Button")) {
            Debug.Log("Using ability 3");
            damageMultiplier = 3;
        } else if(whatAbilityButtonPressed.name.Equals("Attack4Button")) {
            Debug.Log("Using ability 4");
            damageMultiplier = 4;
        }
    }

    /// <summary>
    /// Locks the player in a loop until a target button has been pressed.
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForPlayerAction() {
        bool done = false;
        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
            if (targetHasBeenChosen && abilityHasBeenChosen) {
                done = true; // breaks the loop
            }
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }
        targetHasBeenChosen = false;
        abilityHasBeenChosen = false;

        // now this function returns
    }
}
