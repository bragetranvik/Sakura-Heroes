using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour {
    public int damageMultiplier, armorPenetration, manaCost, levelToUse, manaDrain;
    public string abilityName, abilityTooltip;

    /// <summary>
    /// Return damage of the ability.
    /// </summary>
    /// <returns>Damage of the ability.</returns>
    public int GetDamageMultiplier() {
        return this.damageMultiplier;
    }

    /// <summary>
    /// Return name of the ability.
    /// </summary>
    /// <returns>Name of the ability.</returns>
    public string GetAbilityName() {
        return this.abilityName;
    }

    /// <summary>
    /// Return armor penetration of the ability.
    /// </summary>
    /// <returns>Armor penetration of the ability.</returns>
    public int GetArmorPenetration() {
        return this.armorPenetration;
    }

    /// <summary>
    /// Return mana cost of the ability.
    /// </summary>
    /// <returns>Mana cost of the ability.</returns>
    public int GetManaCost() {
        return this.manaCost;
    }

    /// <summary>
    /// Return level to use the ability.
    /// </summary>
    /// <returns>Level to use the ability.</returns>
    public int GetLevelToUse() {
        return this.levelToUse;
    }

    /// <summary>
    /// Return amount of mana the ability drain.
    /// </summary>
    /// <returns>Amount of mana the ability drain.</returns>
    public int GetManaDrain() {
        return this.manaDrain;
    }

    /// <summary>
    /// Return tooltip of the ability.
    /// </summary>
    /// <returns>Tooltip of the ability.</returns>
    public string GetAbilityTooltip() {
        return this.abilityTooltip;
    }
}
