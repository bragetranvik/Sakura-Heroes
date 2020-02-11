using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour {
    public int damageMultiplier, armorPenetration;
    public string abilityName;

    /// <summary>
    /// Return the damage of the ability.
    /// </summary>
    /// <returns>The damage of the ability</returns>
    public int GetDamageMultiplier() {
        return this.damageMultiplier;
    }

    public string GetAbilityName() {
        return this.abilityName;
    }

    public int GetArmorPenetration() {
        return this.armorPenetration;
    }
}
