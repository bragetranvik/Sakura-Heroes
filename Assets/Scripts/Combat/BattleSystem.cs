using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum BattleState { START, FRIENDLY1TURN, ENEMY1TURN, FRIENDLY2TURN, ENEMY2TURN, FRIENDLY3TURN, ENEMY3TURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public EventSystem EventSystem;

    public GameObject friendly1Prefab, friendly2Prefab, friendly3Prefab;
    public GameObject enemy1Prefab, enemy2Prefab, enemy3Prefab;

    public Transform friendly1BattleStation, friendly2BattleStation, friendly3BattleStation;
    public Transform enemy1BattleStation, enemy2BattleStation, enemy3BattleStation;

    private Unit friendlyUnit1, friendlyUnit2, friendlyUnit3;
    private Unit enemyUnit1, enemyUnit2, enemyUnit3;

    public GameObject attack1ButtonGameObject;
    public Button attack1Button, attack2Button, attack3Button, attack4Button;
    public Button target1Button, target2Button, target3Button;
    public CanvasGroup targetButtons, attackButtons;

    private Unit target;
    private Unit unitsTurn;

    public Text dialogueText;

    public StatusHUD FriendlyStatus;
    public StatusHUD EnemyStatus;

    private bool targetHasBeenChosen = false, abilityHasBeenChosen = false;
    private Button whatAbilityButtonPressed;
    private string highlightedAbility = null;

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
                    if (!unitsTurn.isDead) {
                        FriendlyTurn();
                        yield return WaitForPlayerAction();
                    }
                    if (IsAllEnemiesDead()) {
                        state = BattleState.WON;
                    } else {
                        state = BattleState.ENEMY1TURN;
                    }
                    enableDisableAttackButtons(false);
                    enableDisableTargetButtons(false);
                    break;

                case BattleState.ENEMY1TURN:
                    yield return new WaitForSeconds(1f);
                    unitsTurn = enemyUnit1;
                    if (!unitsTurn.isDead) {
                        SimpleEnemyAI();
                        yield return new WaitForSeconds(2f);
                    }
                    if (IsAllFriendlyDead()) {
                        state = BattleState.LOST;
                    } else {
                        state = BattleState.FRIENDLY2TURN;
                    }
                    break;

                case BattleState.FRIENDLY2TURN:
                    unitsTurn = friendlyUnit2;
                    if (!unitsTurn.isDead) {
                        FriendlyTurn();
                        yield return WaitForPlayerAction();
                    }
                    if (IsAllEnemiesDead()) {
                        state = BattleState.WON;
                    }
                    else {
                        state = BattleState.ENEMY2TURN;
                    }
                    enableDisableAttackButtons(false);
                    enableDisableTargetButtons(false);
                    break;

                case BattleState.ENEMY2TURN:
                    yield return new WaitForSeconds(1f);
                    unitsTurn = enemyUnit2;
                    if (!unitsTurn.isDead) {
                        SimpleEnemyAI();
                        yield return new WaitForSeconds(2f);
                    }
                    if (IsAllFriendlyDead()) {
                        state = BattleState.LOST;
                    }
                    else {
                        state = BattleState.FRIENDLY3TURN;
                    }
                    break;

                case BattleState.FRIENDLY3TURN:
                    unitsTurn = friendlyUnit3;
                    if (!unitsTurn.isDead) {
                        FriendlyTurn();
                        yield return WaitForPlayerAction();
                    }
                    if (IsAllEnemiesDead()) {
                        state = BattleState.WON;
                    }
                    else {
                        state = BattleState.ENEMY3TURN;
                    }
                    enableDisableAttackButtons(false);
                    enableDisableTargetButtons(false);
                    break;

                case BattleState.ENEMY3TURN:
                    yield return new WaitForSeconds(1f);
                    unitsTurn = enemyUnit3;
                    if (!unitsTurn.isDead) {
                        SimpleEnemyAI();
                        yield return new WaitForSeconds(2f);
                    }
                    if (IsAllFriendlyDead()) {
                        state = BattleState.LOST;
                    }
                    else {
                        state = BattleState.FRIENDLY1TURN;
                    }
                    break;

                case BattleState.LOST:
                    enableDisableAttackButtons(false);
                    enableDisableTargetButtons(false);
                    ongoingBattle = false;
                    EndBattle();
                    break;

                case BattleState.WON:
                    enableDisableAttackButtons(false);
                    enableDisableTargetButtons(false);
                    ongoingBattle = false;
                    EndBattle();
                    break;

                default:
                    ongoingBattle = false;
                    break;

            }
        }
    }

    /// <summary>
    /// Places the friendly and enemy units on their battlestations.
    /// Sets the dialogue text to "Fight!".
    /// Gets the unit stats from each unit.
    /// Sets up the HUD for the friendly and enemy side in the battleHUD, and disables all the buttons before start.
    /// </summary>
    private void SetupBattle()
    {
        // Places the friendly and enemy units on their battlestations.
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

        //Sets the dialogue text to "Fight!".
        dialogueText.text = "Fight!";

        //Gets the unit stats from each unit.
        GetAllUnitStats();
        //Just for testing to restore health and mana to full before battle.
        friendlyUnit1.RestoreUnitStats();
        friendlyUnit2.RestoreUnitStats();
        friendlyUnit3.RestoreUnitStats();
        enemyUnit1.RestoreUnitStats();
        enemyUnit2.RestoreUnitStats();
        enemyUnit3.RestoreUnitStats();

        //Sets up the HUD for the friendly and enemy side in the battleHUD, and disables all the buttons before start.
        FriendlyStatus.SetHUD(friendlyUnit1, friendlyUnit2, friendlyUnit3);
        EnemyStatus.SetHUD(enemyUnit1, enemyUnit2, enemyUnit3);
        enableDisableAttackButtons(false);
        enableDisableTargetButtons(false);
    }

    /// <summary>
    /// Runs all functions for a friendly units turn.
    /// </summary>
    private void FriendlyTurn() {
        dialogueText.text = unitsTurn.unitName + "'s turn choose an action:";
        RegenMana();
        FriendlyStatus.SetAbilityName(unitsTurn);
        enableDisableAttackButtons(true);
    }

    private IEnumerator DoAttack() {
        target.TakeDamage(unitsTurn.attack, unitsTurn.GetAbilityDamageMultiplier(highlightedAbility), unitsTurn.GetAbilityArmorPenetration(highlightedAbility));

        EnemyStatus.SetHPandMP(enemyUnit1, enemyUnit2, enemyUnit3);
        FriendlyStatus.SetHPandMP(friendlyUnit1, friendlyUnit2, friendlyUnit3);
        dialogueText.text = unitsTurn.GetAbilityName(highlightedAbility) + " is successful!";

        yield return new WaitForSeconds(2f);
    }

    /// <summary>
    /// Checks if all anemy units are dead.
    /// </summary>
    /// <returns>True if all enemy units is dead.</returns>
    private bool IsAllEnemiesDead() {
        bool allEnemiesDead = false;

        if (enemyUnit1.isDead && enemyUnit2.isDead && enemyUnit3.isDead) {
            allEnemiesDead = true;
        }
        return allEnemiesDead;
    }

    /// <summary>
    /// Checks if all friendly units are dead.
    /// </summary>
    /// <returns>True if all friendly units is dead.</returns>
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
        target.TakeDamage(unitsTurn.attack, 1, 0);

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

    /// <summary>
    /// Generates a random number between and including min to max and return the number as int.
    /// </summary>
    /// <param name="min">The minium value to be rolled as integer.</param>
    /// <param name="max">The maxium value to be rolled as integer.</param>
    /// <returns>Return the random number as int.</returns>
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
    /// <param name="button">The button to choose the target.</param>
    public void AttackChosenTarget(Button button) {
        if (button.name.Equals("Target1Button")) {
            if (unitsTurn.DrainMana((unitsTurn.GetAbilityManaCost(highlightedAbility)))) {
                target = enemyUnit1;
                targetHasBeenChosen = true;
                StartCoroutine(DoAttack());
            }
        } else if(button.name.Equals("Target2Button")) {
            if (unitsTurn.DrainMana((unitsTurn.GetAbilityManaCost(highlightedAbility)))) {
                target = enemyUnit2;
                targetHasBeenChosen = true;
                StartCoroutine(DoAttack());
            }
        } else if(button.name.Equals("Target3Button")) {
            if (unitsTurn.DrainMana((unitsTurn.GetAbilityManaCost(highlightedAbility)))) {
                target = enemyUnit3;
                targetHasBeenChosen = true;
                StartCoroutine(DoAttack());
            }
        }
    }

        enableDisableTargetButtons(false); 
    /// <summary>
    /// Disables target buttons and sets the ability has been chosen boolean to false
    /// </summary>
    public void deselectButtons() {
        abilityHasBeenChosen = false;
        targetHasBeenChosen = false;
        enableDisableTargetButtons(false);
    }

    /// <summary>
    /// If its the turn of a friendly unit and an ability button is pressed enable the target buttons.
    /// </summary>
    /// <param name="button">The ability button.</param>
    public void OnAbilityButton(Button button) {
        if((state == BattleState.FRIENDLY1TURN) || (state == BattleState.FRIENDLY2TURN) || (state == BattleState.FRIENDLY3TURN)) {
            whatAbilityButtonPressed = button;
            abilityHasBeenChosen = true;
            enableDisableTargetButtons(true);
            EnableTargetButtonsForAliveEnemies();
        }
    }

    /// <summary>
    /// Sets the global variable highlighedAbility to ability(1-4) depending on what button was pressed.
    /// </summary>
    public void WhatAbilityToUse() {
        if(whatAbilityButtonPressed.name.Equals("Attack1Button")) {
            highlightedAbility = "ability1";
        } else if(whatAbilityButtonPressed.name.Equals("Attack2Button")) {
            highlightedAbility = "ability2";
        } else if(whatAbilityButtonPressed.name.Equals("Attack3Button")) {
            highlightedAbility = "ability3";
        } else if(whatAbilityButtonPressed.name.Equals("Attack4Button")) {
            highlightedAbility = "ability4";
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
            if (EventSystem.currentSelectedGameObject == null) {
                deselectButtons();
            }
            if (targetHasBeenChosen && abilityHasBeenChosen) {
                done = true; // breaks the loop
            }
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }
        targetHasBeenChosen = false;
        abilityHasBeenChosen = false;
        EventSystem.SetSelectedGameObject(attack1ButtonGameObject);


        // now this function returns
    }
    /// <summary>
    /// Enables or disables the 4 attack buttons.
    /// </summary>
    /// <param name="state">What state you want the buttons to be in</param>
    private void enableDisableAttackButtons(bool state) {
        attackButtons.interactable = state;
   }

    /// <summary>
    /// Enables or disables the 3 target buttons.
    /// </summary>
    /// <param name="state">What state you want the buttons to be in</param>
    private void enableDisableTargetButtons(bool state) {
        targetButtons.interactable = state;
    }

    /// <summary>
    /// Enables the target buttons for enemies which is still alive.
    /// </summary>
    private void EnableTargetButtonsForAliveEnemies() {
        if(enemyUnit1.isDead) {
            target1Button.interactable = false;
        }
        if (enemyUnit2.isDead) {
            target2Button.interactable = false;
        }
        if (enemyUnit3.isDead) {
            target3Button.interactable = false;
        }
    }

    /// <summary>
    /// Restore 20 mana to the unit which has the turn
    /// and set the stats of the unit.
    /// </summary>
    private void RegenMana() {
        unitsTurn.GainMana(20);
        FriendlyStatus.SetHPandMP(friendlyUnit1, friendlyUnit2, friendlyUnit3);
    }

    /// <summary>
    /// Calculate the stats of every unit in the battle.
    /// </summary>
    private void GetAllUnitStats() {
        friendlyUnit1.CalculateStats();
        friendlyUnit2.CalculateStats();
        friendlyUnit3.CalculateStats();
        enemyUnit1.CalculateStats();
        enemyUnit2.CalculateStats();
        enemyUnit3.CalculateStats();
    }
}