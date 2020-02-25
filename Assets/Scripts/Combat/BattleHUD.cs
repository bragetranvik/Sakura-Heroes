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

    [HideInInspector]
    public Boolean isPlayerHoveringButton;
    [HideInInspector]
    public Button hoveredButton;

    // Start is called before the first frame update
    void Start()
    {
        tooltipText.enabled = false;
        tooltipBackground.enabled = false;
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
    /// Show the tooltip of the hovered ability.
    /// </summary>
    /// <param name="ability">Ability that the player is currently hovering</param>
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

    public Ability GetUnitAbility(Unit unitsTurn, Button hoveredButton) {
        Ability currentUnitAbility = dummyAbility;
        if (hoveredButton == attack1Button) {
            currentUnitAbility = unitsTurn.ability1;
        } else if (hoveredButton == attack2Button) {
            currentUnitAbility = unitsTurn.ability2;
        } else if (hoveredButton == attack3Button) {
            currentUnitAbility = unitsTurn.ability3;
        } else if (hoveredButton == attack4Button) {
            currentUnitAbility = unitsTurn.ability4;
        } 

        return currentUnitAbility;
    }
}