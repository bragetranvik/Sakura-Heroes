using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    public string unitName;
    public int unitLevel;

    //Base stats should only be between 1-12.
    public int baseAttack, baseDefence, baseHP;
    public int currentHP, currentMP;

    public Ability ability1, ability2, ability3, ability4;

    [HideInInspector]
    public int attack, defence, maxHP, maxMP;

    private readonly int attackConstant = 5;
    private readonly int defenceConstant = 10;
    private readonly int HPConstant = 100;
    private readonly int MPConstant = 100;

    public bool isDead = false;

    /// <summary>
    /// Reduce currentHP of the unit by dmg*dmgMultiplier reduced by defence which get reduced again by armorPenetration.
    /// If target dies from the damage the isDead will be set to true and currentHP will be set to 0.
    /// </summary>
    /// <param name="dmg">The damage to deal.</param>
    /// <param name="damageMultiplier">Multiplies the damage to deal.</param>
    /// <param name="armorPenetration">How many percentage to reduce to defence of the unit.</param>
    public void TakeDamage(int dmg, int damageMultiplier, int armorPenetration) {
        currentHP -= Convert.ToInt32((dmg*damageMultiplier)*(1f-(defence*(1f-(armorPenetration/100f))/100f)));

        if(currentHP <= 0) {
            isDead = true;
            currentHP = 0;
        }
    }

    /// <summary>
    /// Drain mana from the unit if the unit has enough mana.
    /// </summary>
    /// <param name="amount">The amount of mana to drain.</param>
    /// <returns>Return true if the unit has enough mana.</returns>
    public bool DrainMana(int amount) {
        bool enoughMana = false;
        if(currentMP >= amount) {
            enoughMana = true;
            currentMP -= amount;
        }
        return enoughMana;
    }

    /// <summary>
    /// Restore mana to the unit. If current mana > max mana, current mana will be set to max mana.
    /// Wont restore mana if the unit is dead.
    /// </summary>
    /// <param name="amount">The amount of mana to restore.</param>
    public void GainMana(int amount) {
        if(!isDead) {
            currentMP += amount;
        }
        if(currentMP > maxMP) {
            currentMP = maxMP;
        }
    }

    /// <summary>
    /// Restore all stats of the unit to full.
    /// </summary>
    public void RestoreUnitStats() {
        isDead = false;
        currentHP = maxHP;
        currentMP = maxMP;
    }

    /// <summary>
    /// Calculates attack, defence, maxHP and maxMP of the unit
    /// and if current HP > maxHP, sets the current HP = maxHP.
    /// </summary>
    public void CalculateStats() {
        attack = Convert.ToInt32(unitLevel * baseAttack/12 + attackConstant);
        defence = Convert.ToInt32(unitLevel * baseDefence/20 + defenceConstant);
        maxHP = Convert.ToInt32(unitLevel * baseHP/2 + HPConstant);
        if(unitLevel.Equals(99)) {
            maxMP = 200;
        } else {
            maxMP = Convert.ToInt32(MPConstant + 1 * unitLevel -1);
        }
        if(currentHP > maxHP) {
            currentHP = Convert.ToInt32(maxHP);
        }
    }

    /// <summary>
    /// Return the ability damage depending on what ability you used.
    /// </summary>
    /// <param name="ability">Has to be ability1-4.</param>
    /// <returns>The damage of the used ability.</returns>
    public int GetAbilityDamageMultiplier(string ability) {
        int damage = 0;
        if(ability.Equals("ability1")) {
            damage = ability1.GetDamageMultiplier();
        } else if(ability.Equals("ability2")) {
            damage = ability2.GetDamageMultiplier();
        } else if(ability.Equals("ability3")) {
            damage = ability3.GetDamageMultiplier();
        } else if(ability.Equals("ability4")) {
            damage = ability4.GetDamageMultiplier();
        }
        return damage;
    }

    /// <summary>
    /// Return the ability name.
    /// </summary>
    /// <param name="ability">Has to be "ability1", "ability2", "ability3" or "ability4".</param>
    /// <returns>Return name of the ability as string.</returns>
    public string GetAbilityName(string ability) {
        string name = null;
        if (ability.Equals("ability1")) {
            name = ability1.GetAbilityName();
        }
        else if (ability.Equals("ability2")) {
            name = ability2.GetAbilityName();
        }
        else if (ability.Equals("ability3")) {
            name = ability3.GetAbilityName();
        }
        else if (ability.Equals("ability4")) {
            name = ability4.GetAbilityName();
        }
        return name;
    }

    /// <summary>
    /// Return the ability armor penetration.
    /// </summary>
    /// <param name="ability">Has to be "ability1", "ability2", "ability3" or "ability4"</param>
    /// <returns>Returns the armor penetration of the ability as int.</returns>
    public int GetAbilityArmorPenetration(string ability) {
        int armorPenetration = 0;
        if (ability.Equals("ability1")) {
            armorPenetration = ability1.GetArmorPenetration();
        }
        else if (ability.Equals("ability2")) {
            armorPenetration = ability2.GetArmorPenetration();
        }
        else if (ability.Equals("ability3")) {
            armorPenetration = ability3.GetArmorPenetration();
        }
        else if (ability.Equals("ability4")) {
            armorPenetration = ability4.GetArmorPenetration();
        }
        return armorPenetration;
    }

    /// <summary>
    /// Return the ability mana cost.
    /// </summary>
    /// <param name="ability">Has to be "ability1", "ability2", "ability3" or "ability4"</param>
    /// <returns>Returns the mana cost of the ability as int.</returns>
    public int GetAbilityManaCost(string ability) {
        int manaCost = 0;
        if (ability.Equals("ability1")) {
            manaCost = ability1.GetManaCost();
        }
        else if (ability.Equals("ability2")) {
            manaCost = ability2.GetManaCost();
        }
        else if (ability.Equals("ability3")) {
            manaCost = ability3.GetManaCost();
        }
        else if (ability.Equals("ability4")) {
            manaCost = ability4.GetManaCost();
        }
        return manaCost;
    }
}
