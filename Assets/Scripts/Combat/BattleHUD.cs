using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour {

    public BattleSystem battleSystem;
    public Ability dummyAbility;

    public Button attack1Button, attack2Button, attack3Button, attack4Button;

    public Image tooltipBackground;
    public Text tooltipText;

    public Sprite darkBattleStation;
    public Sprite lightBattleStation;

    //public SpriteRenderer spriteRendererOfCharacter;
    public Image imageOfCharacter;

    private bool isStartupDone = false;

    [HideInInspector]
    public Boolean isPlayerHoveringButton;
    [HideInInspector]
    public Button hoveredButton;

    // Start is called before the first frame update
    void Start()
    {
        tooltipText.enabled = false;
        tooltipBackground.enabled = false;
        StartCoroutine(CountdownFromStart());
    }

    // Update is called once per frame
    void Update()
    {
        // When the player hovers over an ability button, this method will fire and it will
        // wait for two seconds, and if the player is still hovering over the ability button it will
        // display the tooltip of the ability the player is hovering.

        Unit unitsTurn = battleSystem.GetUnitsTurn();
        Ability hoveredAbility = GetUnitAbility(unitsTurn, hoveredButton);
        if (isPlayerHoveringButton && (hoveredAbility != dummyAbility)) {
            ShowTooltip(hoveredAbility, unitsTurn);
        } else {
            HideTooltip();
        }
    }
  
    /// <summary>
    /// Function that waits for 2.5seconds before the battle starts. This is used to avoid
    /// a null reference error if a player is hovering over an ability to get a tooltip when no
    /// abilities have been set yet.
    /// </summary>
    IEnumerator CountdownFromStart() {
        yield return new WaitForSeconds(2.5f);
        isStartupDone = true;
    }

    /// <summary>
    /// Show the tooltip of the hovered ability.
    /// </summary>
    /// <param name="ability">Ability that the player is currently hovering over</param>
    public void ShowTooltip(Ability ability, Unit unit) {
        //Debug.Log(ability.abilityName + ": " + ability.abilityTooltip);
        tooltipBackground.enabled = true;
        tooltipText.enabled = true;
        tooltipText.text = ability.abilityName + ": " + ability.abilityTooltip + "\n" +
            "Damage: " + ability.GetAbilityDamage(unit) + ", Mana cost: " + ability.GetManaCost();
    }

    /// <summary>
    /// Hide the tooltip for abilities.
    /// </summary>
    public void HideTooltip() {
        tooltipBackground.enabled = false;
        tooltipText.enabled = false;
        tooltipText.text = "This should be hidden";
    }

    public void SetIsPlayerHovering(Boolean hovering) {
        isPlayerHoveringButton = hovering;
    }

    public void SetHoveredButton(Button button) {
        hoveredButton = button;
    }

    /// <summary>
    /// Gets the ability of a given unit depending on the button that is being hovered over by the player
    /// </summary>
    /// <param name="unitsTurn">The unit whos turn it currently is</param>
    /// <param name="hoveredButton">The button the player is hovering over</param>
    /// <returns></returns>
    public Ability GetUnitAbility(Unit unitsTurn, Button hoveredButton) {
        Ability currentUnitAbility = dummyAbility;
        if (isStartupDone) {
            if ((hoveredButton == attack1Button) && (unitsTurn.ability1 != null)) {
                currentUnitAbility = unitsTurn.ability1;
            } else if ((hoveredButton == attack2Button) && (unitsTurn.ability2 != null)) {
                currentUnitAbility = unitsTurn.ability2;
            } else if ((hoveredButton == attack3Button) && (unitsTurn.ability3 != null)) {
                currentUnitAbility = unitsTurn.ability3;
            } else if ((hoveredButton == attack4Button) && (unitsTurn.ability4 != null)) {
                currentUnitAbility = unitsTurn.ability4;
            }
        }
        return currentUnitAbility;
    }

    public void SetBattleStationToDark(GameObject battleStation) {
        battleStation.GetComponent<SpriteRenderer>().sprite = darkBattleStation;
        battleStation.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.6f);
    }
    public void SetBattleStationToLight(GameObject battleStation) {
        battleStation.GetComponent<SpriteRenderer>().sprite = lightBattleStation;
        battleStation.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.95f);
    }

    public void showPortrait(Unit unitsTurn) {
        //spriteRendererOfCharacter.sprite = unitsTurn.getPortraitPicture();
        imageOfCharacter.sprite = unitsTurn.getPortraitPicture();
    }
}