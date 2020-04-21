using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Fungus;

public enum BattleState { START, FRIENDLY1TURN, ENEMY1TURN, FRIENDLY2TURN, ENEMY2TURN, FRIENDLY3TURN, ENEMY3TURN, WON, LOST }

public class BattleSystem : MonoBehaviour {
    public EventSystem EventSystem;
    public BattleHUD BattleHUD;

    //public GameObject friendly1Prefab, friendly2Prefab, friendly3Prefab;
    //public GameObject enemy1Prefab, enemy2Prefab, enemy3Prefab;

    public GameObject friendly1BattleStation, friendly2BattleStation, friendly3BattleStation;
    public GameObject enemy1BattleStation, enemy2BattleStation, enemy3BattleStation;

    public GameObject friendlyTeamGO;
    public UnitTeam friendlyTeam;

    public GameObject enemyTeamGO;
    public UnitTeam enemyTeam;

    private GameObject friendlyUnit1GO, friendlyUnit2GO, friendlyUnit3GO;
    private GameObject enemyUnit1GO, enemyUnit2GO, enemyUnit3GO;
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
    private string highlightedAbility = null;
    public EnemyAI enemyAI;
    public LeaveBattle leaveBattle;

    public BattleState state;

    //Delay in seconds which happens after an attack.
    private const float delayAfterFriendlyAttack = 2f;
    private const float delayAfterEnemyAttack = 2.5f;

    private List<Unit> unitList;

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

    //Awake is called when the script instance is being loaded.
    private void Awake() {
        attack1Button.onClick.AddListener(() => { highlightedAbility = "ability1"; });
        attack2Button.onClick.AddListener(() => { highlightedAbility = "ability2"; });
        attack3Button.onClick.AddListener(() => { highlightedAbility = "ability3"; });
        attack4Button.onClick.AddListener(() => { highlightedAbility = "ability4"; });
        target1EnemyButton.onClick.AddListener(() => { target = enemyUnit1; AttackChosenTarget(); });
        target2EnemyButton.onClick.AddListener(() => { target = enemyUnit2; AttackChosenTarget(); });
        target3EnemyButton.onClick.AddListener(() => { target = enemyUnit3; AttackChosenTarget(); });
        target1FriendlyButton.onClick.AddListener(() => { target = friendlyUnit1; AttackChosenTarget(); });
        target2FriendlyButton.onClick.AddListener(() => { target = friendlyUnit2; AttackChosenTarget(); });
        target3FriendlyButton.onClick.AddListener(() => { target = friendlyUnit3; AttackChosenTarget(); });
    }

    /// <summary>
    /// Runs in a state loop until either the battle is lost or won.
    /// It will cycle thought each units turn from friendly unit to enemy unit.
    /// If a unit is missing or dead it will skip that unit and go to the next.
    /// </summary>
    /// <returns>Not sure.</returns>
    private IEnumerator Battle() {
        bool ongoingBattle = true;

        while (ongoingBattle) {
            CheckForUnitsWithDot();
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
    /// Places the friendly and enemy units on their battle stations.
    /// Ensures that the battle stations are using the dark sprite at the start.
    /// Sets the dialogue text to "Fight!".
    /// Gets the unit stats from each unit.
    /// Sets up the HUD for the friendly and enemy side in the battleHUD, and disables all the buttons before start.
    /// </summary>
    private void SetupBattle()
    {
        // Sets the friendly and enemy units and GameObjects. Also disables the objects
        // that were not destroyed on load, and sets the correct position and scale to the units
        PrepareFriendlyUnits();
        PrepareEnemyUnits();

        // Places the friendly and enemy units on their battle stations.
        GameObject friendly1GO = Instantiate(friendlyUnit1GO, friendly1BattleStation.GetComponent<Transform>());
        friendlyUnit1 = friendly1GO.GetComponent<Unit>();
        GameObject friendly2GO = Instantiate(friendlyUnit2GO, friendly2BattleStation.GetComponent<Transform>());
        friendlyUnit2 = friendly2GO.GetComponent<Unit>();
        GameObject friendly3GO = Instantiate(friendlyUnit3GO, friendly3BattleStation.GetComponent<Transform>());
        friendlyUnit3 = friendly3GO.GetComponent<Unit>();

        GameObject enemy1GO = Instantiate(enemyUnit1GO, enemy1BattleStation.GetComponent<Transform>());
        enemyUnit1 = enemy1GO.GetComponent<Unit>();
        GameObject enemy2GO = Instantiate(enemyUnit2GO, enemy2BattleStation.GetComponent<Transform>());
        enemyUnit2 = enemy2GO.GetComponent<Unit>();
        GameObject enemy3GO = Instantiate(enemyUnit3GO, enemy3BattleStation.GetComponent<Transform>());
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

        HideImportedObjects();

        //Makes a list of all units in the battle.
        MakeUnitList();
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
    /// Checks if all enemy units are dead.
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
        SetFriendlySprite(false);
        if(state == BattleState.WON) {
            //Debug.Log("Battle won STATE");
            dialogueText.text = "You won the battle!";
            if (!enemyTeam.teamType.Equals(TeamType.npcTeam)) {
                enemyTeam.AddToDefeatedList();
            }
            PlayerInventory playerInventory = friendlyTeamGO.GetComponent<PlayerInventory>();
            playerInventory.GainXPFromEnemies(enemyUnit2.unitLevel);
            playerInventory.EarnMoney(playerInventory.CalculateHowMuchMoneyToEarn(enemyUnit2.unitLevel));
            //yield return new WaitForSeconds(2f);
            leaveBattle.ChangeSceneToPrevious();
        } else if(state == BattleState.LOST) {
            dialogueText.text = "You were defeated.";
            //yield return new WaitForSeconds(2f);
            leaveBattle.ChangeSceneToStartHub();
        }
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
    /// Sets targetHasBeenChosen to true and attack the chosen target.
    /// </summary>
    private void AttackChosenTarget() {
        targetHasBeenChosen = true;
        DoAttack();
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
    public void OnAbilityButton() {
        if((state == BattleState.FRIENDLY1TURN) || (state == BattleState.FRIENDLY2TURN) || (state == BattleState.FRIENDLY3TURN)) {
            abilityHasBeenChosen = true;
            EnableDisableTargetButtons(true);
            EnableTargetButtonsForAliveEnemies();
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
        } if (friendlyUnit2.isDead) {
            target2FriendlyButton.interactable = false;
        } if (friendlyUnit3.isDead) {
            target3FriendlyButton.interactable = false;
        } if (enemyUnit1.isDead) {
            target1EnemyButton.interactable = false;
        } if (enemyUnit2.isDead) {
            target2EnemyButton.interactable = false;
        } if (enemyUnit3.isDead) {
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

    /// <summary>
    /// Return unit which currently has the turn.
    /// </summary>
    /// <returns>Unit which has the turn.</returns>
    public Unit GetUnitsTurn() {
        return unitsTurn;
    }

    /// <summary>
    /// Checks every unit if they have an active dot and damage them if they do.
    /// </summary>
    private void CheckForUnitsWithDot() {
        if (unitList != null) {
            foreach (Unit unit in unitList) {
                if (unit.IsDotActive()) {
                    unit.TakeDotDmg();
                }
            }
        }
    }

    /// <summary>
    /// Makes a unit list with every unit in the battle.
    /// </summary>
    private void MakeUnitList() {
        unitList = new List<Unit> {
            friendlyUnit1,
            friendlyUnit2,
            friendlyUnit3,
            enemyUnit1,
            enemyUnit2,
            enemyUnit3
        };
    }

    /// <summary>
    /// When loading this scene from a different scene these two functions will set the
    /// player and enemy units to the correct ones dependent on what enemy was encountered and
    /// how the team of the player was built up
    /// </summary>
    private void PrepareFriendlyUnits() {
        friendlyTeamGO = GameObject.FindGameObjectWithTag("Player");
        friendlyTeam = friendlyTeamGO.GetComponent<UnitTeam>();

        friendlyUnit1GO = friendlyTeam.GetUnit1GO();
        friendlyUnit2GO = friendlyTeam.GetUnit2GO();
        friendlyUnit3GO = friendlyTeam.GetUnit3GO();

        friendlyUnit1 = friendlyUnit1GO.GetComponent<Unit>();
        friendlyUnit2 = friendlyUnit2GO.GetComponent<Unit>();
        friendlyUnit3 = friendlyUnit3GO.GetComponent<Unit>();

        //Ensure that the sprite renderer is enabled
        SetFriendlySprite(true);

        friendlyUnit1GO.GetComponent<Transform>().localScale = friendlyUnit1.GetScale();
        friendlyUnit2GO.GetComponent<Transform>().localScale = friendlyUnit2.GetScale();
        friendlyUnit3GO.GetComponent<Transform>().localScale = friendlyUnit3.GetScale();

        friendlyUnit1GO.GetComponent<Transform>().localPosition = friendlyUnit1.GetPosition();
        friendlyUnit2GO.GetComponent<Transform>().localPosition = friendlyUnit2.GetPosition();
        friendlyUnit3GO.GetComponent<Transform>().localPosition = friendlyUnit3.GetPosition();
    }
    private void PrepareEnemyUnits() {
        enemyTeamGO = GameObject.FindGameObjectWithTag("Enemy");
        enemyTeam = enemyTeamGO.GetComponent<UnitTeam>();

        enemyUnit1GO = enemyTeam.GetUnit1GO();
        enemyUnit2GO = enemyTeam.GetUnit2GO();
        enemyUnit3GO = enemyTeam.GetUnit3GO();

        enemyUnit1 = enemyUnit1GO.GetComponent<Unit>();
        enemyUnit2 = enemyUnit2GO.GetComponent<Unit>();
        enemyUnit3 = enemyUnit3GO.GetComponent<Unit>();

        //Ensure that the sprite renderer is enabled
        enemyUnit1GO.GetComponent<SpriteRenderer>().enabled = true;
        enemyUnit2GO.GetComponent<SpriteRenderer>().enabled = true;
        enemyUnit3GO.GetComponent<SpriteRenderer>().enabled = true;

        enemyUnit1GO.GetComponent<Transform>().localScale = enemyUnit1.GetScale();
        enemyUnit2GO.GetComponent<Transform>().localScale = enemyUnit2.GetScale();
        enemyUnit3GO.GetComponent<Transform>().localScale = enemyUnit3.GetScale();

        enemyUnit1GO.GetComponent<Transform>().localPosition = enemyUnit1.GetPosition();
        enemyUnit2GO.GetComponent<Transform>().localPosition = enemyUnit2.GetPosition();
        enemyUnit3GO.GetComponent<Transform>().localPosition = enemyUnit3.GetPosition();
    }

    private void HideImportedObjects() {
        friendlyTeamGO.SetActive(false);
        foreach (GameObject pet in friendlyTeamGO.GetComponent<PlayerInventory>().petList) {
            pet.GetComponent<SpriteRenderer>().enabled = false;
        }
        enemyTeamGO.SetActive(false);
    }

    public void EnablePlayerAndEnemyObjects() {
        friendlyTeamGO.SetActive(true);
        enemyTeamGO.SetActive(true);
    }

    /// <summary>
    /// Sets the state of the friendly units sprite renderer.
    /// </summary>
    /// <param name="state">State to set the friendly sprite renderer.</param>
    public void SetFriendlySprite(bool state) {
        friendlyUnit1GO.GetComponent<SpriteRenderer>().enabled = state;
        friendlyUnit2GO.GetComponent<SpriteRenderer>().enabled = state;
        friendlyUnit3GO.GetComponent<SpriteRenderer>().enabled = state;
    }
}