using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum BattleState { START, FRIENDLY1TURN, ENEMY1TURN, FRIENDLY2TURN, ENEMY2TURN, FRIENDLY3TURN, ENEMY3TURN, WON, LOST }

public class BattleSystem : MonoBehaviour {
    public EventSystem EventSystem;
    public BattleHUD BattleHUD;

    public GameObject friendly1Prefab, friendly2Prefab, friendly3Prefab;
    public GameObject enemy1Prefab, enemy2Prefab, enemy3Prefab;

    public GameObject friendly1BattleStation, friendly2BattleStation, friendly3BattleStation;
    public GameObject enemy1BattleStation, enemy2BattleStation, enemy3BattleStation;

    private Unit friendlyUnit1, friendlyUnit2, friendlyUnit3;
    private Unit enemyUnit1, enemyUnit2, enemyUnit3;

    public GameObject attack1ButtonGameObject;
    public Button attack1Button, attack2Button, attack3Button, attack4Button;
    public Button target1FriendlyButton, target2FriendlyButton, target3FriendlyButton;
    public Button target1EnemyButton, target2EnemyButton, target3EnemyButton;

    public CanvasGroup targetEnemyButtons, targetFriendlyButtons, attackButtons;

    private Unit target;
    private Unit unitsTurn;

    public Text dialogueText;

    public StatusHUD FriendlyStatus;
    public StatusHUD EnemyStatus;

    private bool targetHasBeenChosen = false, abilityHasBeenChosen = false;
    private Button whatAbilityButtonPressed;
    private string highlightedAbility = null;
    public EnemyAI enemyAI;

    public BattleState state;

    //Delay in seconds which happens after an attack.
    private const float delayAfterFriendlyAttack = 2f;
    private const float delayAfterEnemyAttack = 2.5f;

    // Start is called before the first frame update
    void Start() {
        state = BattleState.START;
        StartCoroutine(Battle());
    }

    private void Update() {
        //Updates the HP and mana in the HUD.
        FriendlyStatus.SetHUD(friendlyUnit1, friendlyUnit2, friendlyUnit3);
        EnemyStatus.SetHUD(enemyUnit1, enemyUnit2, enemyUnit3);
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
                    BattleHUD.SetBattleStationToDark(enemy3BattleStation);
                    BattleHUD.SetBattleStationToLight(friendly1BattleStation);
                    BattleHUD.showPortrait(unitsTurn);
                    if (!unitsTurn.isDead) {
                        yield return FriendlyTurn();
                    }
                    if (IsAllEnemiesDead()) {
                        state = BattleState.WON;
                    } else {
                        state = BattleState.ENEMY1TURN;
                    }
                    break;

                case BattleState.ENEMY1TURN:
                    unitsTurn = enemyUnit1;
                    BattleHUD.SetBattleStationToDark(friendly1BattleStation);
                    BattleHUD.SetBattleStationToLight(enemy1BattleStation);
                    BattleHUD.showPortrait(unitsTurn);
                    if (!unitsTurn.isDead) {
                        yield return DoEnemyAI();
                    }
                    if (IsAllFriendlyDead()) {
                        state = BattleState.LOST;
                    } else {
                        state = BattleState.FRIENDLY2TURN;
                    }
                    break;

                case BattleState.FRIENDLY2TURN:
                    unitsTurn = friendlyUnit2;
                    BattleHUD.SetBattleStationToDark(enemy1BattleStation);
                    BattleHUD.SetBattleStationToLight(friendly2BattleStation);
                    BattleHUD.showPortrait(unitsTurn);
                    if (!unitsTurn.isDead) {
                        yield return FriendlyTurn();
                    }
                    if (IsAllEnemiesDead()) {
                        state = BattleState.WON;
                    }
                    else {
                        state = BattleState.ENEMY2TURN;
                    }
                    break;

                case BattleState.ENEMY2TURN:
                    unitsTurn = enemyUnit2;
                    BattleHUD.SetBattleStationToDark(friendly2BattleStation);
                    BattleHUD.SetBattleStationToLight(enemy2BattleStation);
                    BattleHUD.showPortrait(unitsTurn);
                    if (!unitsTurn.isDead) {
                        yield return DoEnemyAI();
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
                    BattleHUD.SetBattleStationToDark(enemy2BattleStation);
                    BattleHUD.SetBattleStationToLight(friendly3BattleStation);
                    BattleHUD.showPortrait(unitsTurn);
                    if (!unitsTurn.isDead) {
                        yield return FriendlyTurn();
                    }
                    if (IsAllEnemiesDead()) {
                        state = BattleState.WON;
                    }
                    else {
                        state = BattleState.ENEMY3TURN;
                    }
                    break;

                case BattleState.ENEMY3TURN:
                    unitsTurn = enemyUnit3;
                    BattleHUD.SetBattleStationToDark(friendly3BattleStation);
                    BattleHUD.SetBattleStationToLight(enemy3BattleStation);
                    BattleHUD.showPortrait(unitsTurn);
                    if (!unitsTurn.isDead) {
                        yield return DoEnemyAI();
                    }
                    if (IsAllFriendlyDead()) {
                        state = BattleState.LOST;
                    }
                    else {
                        state = BattleState.FRIENDLY1TURN;
                    }
                    break;

                case BattleState.LOST:
                    EnableDisableAttackButtons(false);
                    EnableDisableTargetButtons(false);
                    ongoingBattle = false;
                    EndBattle();
                    break;

                case BattleState.WON:
                    EnableDisableAttackButtons(false);
                    EnableDisableTargetButtons(false);
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
    /// Ensures that the battlestations are using the dark sprite at the start.
    /// Sets the dialogue text to "Fight!".
    /// Gets the unit stats from each unit.
    /// Sets up the HUD for the friendly and enemy side in the battleHUD, and disables all the buttons before start.
    /// </summary>
    private void SetupBattle()
    {
        // Places the friendly and enemy units on their battlestations.
        GameObject friendly1GO = Instantiate(friendly1Prefab, friendly1BattleStation.GetComponent<Transform>());
        friendlyUnit1 = friendly1GO.GetComponent<Unit>();
        GameObject friendly2GO = Instantiate(friendly2Prefab, friendly2BattleStation.GetComponent<Transform>());
        friendlyUnit2 = friendly2GO.GetComponent<Unit>();
        GameObject friendly3GO = Instantiate(friendly3Prefab, friendly3BattleStation.GetComponent<Transform>());
        friendlyUnit3 = friendly3GO.GetComponent<Unit>();

        GameObject enemy1GO = Instantiate(enemy1Prefab, enemy1BattleStation.GetComponent<Transform>());
        enemyUnit1 = enemy1GO.GetComponent<Unit>();
        GameObject enemy2GO = Instantiate(enemy2Prefab, enemy2BattleStation.GetComponent<Transform>());
        enemyUnit2 = enemy2GO.GetComponent<Unit>();
        GameObject enemy3GO = Instantiate(enemy3Prefab, enemy3BattleStation.GetComponent<Transform>());
        enemyUnit3 = enemy3GO.GetComponent<Unit>();

        BattleHUD.SetBattleStationToDark(friendly1BattleStation);
        BattleHUD.SetBattleStationToDark(friendly2BattleStation);
        BattleHUD.SetBattleStationToDark(friendly3BattleStation);
        BattleHUD.SetBattleStationToDark(enemy1BattleStation);
        BattleHUD.SetBattleStationToDark(enemy2BattleStation);
        BattleHUD.SetBattleStationToDark(enemy3BattleStation);



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

        //Disables all the buttons before start.
        EnableDisableAttackButtons(false);
        EnableDisableTargetButtons(false);
    }

    /// <summary>
    /// Runs all functions for a friendly units turn.
    /// </summary>
    private IEnumerator FriendlyTurn() {
        dialogueText.text = unitsTurn.unitName + "'s turn choose an action:";
        RegenMana();
        FriendlyStatus.SetAbilityName(unitsTurn);
        EnableAbilityButtonsUnitCanUse(unitsTurn);
        yield return WaitForPlayerAction();
        EnableDisableAttackButtons(false);
        EnableDisableTargetButtons(false);
        yield return new WaitForSeconds(delayAfterFriendlyAttack);
    }

    private void DoAttack() {
        if(unitsTurn.UseAbility(highlightedAbility, unitsTurn, target, friendlyUnit1, friendlyUnit2, friendlyUnit3, enemyUnit1, enemyUnit2, enemyUnit3)) {
            dialogueText.text = unitsTurn.GetAbilityName(highlightedAbility) + " is successful!";
        } else {
            dialogueText.text = "The ability " + unitsTurn.GetAbilityName(highlightedAbility) + " failed!";
        }
        //yield return new WaitForSeconds(2f);
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
        BattleHUD.SetBattleStationToDark(friendly1BattleStation);
        BattleHUD.SetBattleStationToDark(friendly2BattleStation);
        BattleHUD.SetBattleStationToDark(friendly3BattleStation);
        BattleHUD.SetBattleStationToDark(enemy1BattleStation);
        BattleHUD.SetBattleStationToDark(enemy2BattleStation);
        BattleHUD.SetBattleStationToDark(enemy3BattleStation);
    }

    /// <summary>
    /// Does what an enemy should do.
    /// </summary>
    private IEnumerator DoEnemyAI() {
        RegenMana();
        string abilityUsed = enemyAI.DoEnemyAI(unitsTurn, enemyUnit1, enemyUnit2, enemyUnit3, friendlyUnit1, friendlyUnit2, friendlyUnit3);
        dialogueText.text = unitsTurn.unitName + " use " + unitsTurn.GetAbilityName(abilityUsed) + "!";
        yield return new WaitForSeconds(delayAfterEnemyAttack);
    }

    /// <summary>
    /// Choose the target depending on what target button has been pressed and set targetHasBeenChosen to true.
    /// </summary>
    /// <param name="button">The button to choose the target.</param>
    public void AttackChosenTarget(Button button) {
        if (button.name.Equals("Target1FriendlyButton")) {
            target = friendlyUnit1;
            targetHasBeenChosen = true;
            DoAttack();
        } else if(button.name.Equals("Target2FriendlyButton")) {
            target = friendlyUnit2;
            targetHasBeenChosen = true;
            DoAttack();
        } else if(button.name.Equals("Target3FriendlyButton")) {
            target = friendlyUnit3;
            targetHasBeenChosen = true;
            DoAttack();
        } else if (button.name.Equals("Target1EnemyButton")) {
            target = enemyUnit3;
            targetHasBeenChosen = true;
            DoAttack();
        } else if (button.name.Equals("Target2EnemyButton")) {
            target = enemyUnit3;
            targetHasBeenChosen = true;
            DoAttack();
        } else if (button.name.Equals("Target3EnemyButton")) {
            target = enemyUnit3;
            targetHasBeenChosen = true;
            DoAttack();
        }
    }

    /// <summary>
    /// Disables target buttons and sets the ability has been chosen boolean to false
    /// </summary>
    public void DeselectButtons() {
        abilityHasBeenChosen = false;
        targetHasBeenChosen = false;
        EnableDisableTargetButtons(false);
    }

    /// <summary>
    /// If its the turn of a friendly unit and an ability button is pressed enable the target buttons.
    /// </summary>
    /// <param name="button">The ability button.</param>
    public void OnAbilityButton(Button button) {
        if((state == BattleState.FRIENDLY1TURN) || (state == BattleState.FRIENDLY2TURN) || (state == BattleState.FRIENDLY3TURN)) {
            whatAbilityButtonPressed = button;
            abilityHasBeenChosen = true;
            EnableDisableTargetButtons(true);
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
                DeselectButtons();
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
    private void EnableDisableAttackButtons(bool state) {
        attackButtons.interactable = state;
        attack1Button.interactable = state;
        attack2Button.interactable = state;
        attack3Button.interactable = state;
        attack4Button.interactable = state;
    }

    /// <summary>
    /// Enables or disables the 3 target buttons.
    /// </summary>
    /// <param name="state">What state you want the buttons to be in</param>
    private void EnableDisableTargetButtons(bool state) {
        targetFriendlyButtons.interactable = state;
        targetEnemyButtons.interactable = state;
    }

    /// <summary>
    /// Enables the target buttons for enemies which is still alive.
    /// </summary>
    private void EnableTargetButtonsForAliveEnemies() {
        if(friendlyUnit1.isDead) {
            target1FriendlyButton.interactable = false;
        }
        if (friendlyUnit2.isDead) {
            target2FriendlyButton.interactable = false;
        }
        if (friendlyUnit3.isDead) {
            target3FriendlyButton.interactable = false;
        }
        if (enemyUnit1.isDead) {
            target1EnemyButton.interactable = false;
        }
        if (enemyUnit2.isDead) {
            target2EnemyButton.interactable = false;
        }
        if (enemyUnit3.isDead) {
            target3EnemyButton.interactable = false;
        }
    }

    /// <summary>
    /// Restore 20 mana to the unit which has the turn
    /// and set the stats of the unit.
    /// </summary>
    private void RegenMana() {
        unitsTurn.GainMana(20);
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

    /// <summary>
    /// Enables the ability buttons of abilities the unit got mana and level for.
    /// </summary>
    /// <param name="unit">The unit to enable buttons for.</param>
    private void EnableAbilityButtonsUnitCanUse(Unit unit) {
        EnableDisableAttackButtons(true);
        if ((unit.currentMP < unit.GetAbilityManaCost("ability1")) || (unit.unitLevel < unit.GetAbilityLevelToUse("ability1"))) {
            attack1Button.interactable = false;
        }
        if ((unit.currentMP < unit.GetAbilityManaCost("ability2")) || (unit.unitLevel < unit.GetAbilityLevelToUse("ability2"))) {
            attack2Button.interactable = false;
        }
        if ((unit.currentMP < unit.GetAbilityManaCost("ability3")) || (unit.unitLevel < unit.GetAbilityLevelToUse("ability3"))) {
            attack3Button.interactable = false;
        }
        if ((unit.currentMP < unit.GetAbilityManaCost("ability4")) || (unit.unitLevel < unit.GetAbilityLevelToUse("ability4"))) {
            attack4Button.interactable = false;
        }
    }

    public Unit GetUnitsTurn() {
        return unitsTurn;
    }
}