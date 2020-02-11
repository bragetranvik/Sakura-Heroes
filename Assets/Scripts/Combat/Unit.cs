using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    public string unitName;
    public int unitLevel;

    //Base stats should only be between 1-12.
    public int baseAttack;
    public int baseDefence;
    public int baseHP;

    public int currentHP;
    public int currentMP;

    public Ability ability1, ability2, ability3, ability4;

    [HideInInspector]
    public int attack, defence, maxHP, maxMP;

    private readonly int attackConstant = 5;
    private readonly int defenceConstant = 10;
    private readonly int HPConstant = 100;
    private readonly int MPConstant = 100;

    public bool isDead = false;

    private void Start() {
        GetStats();
    }

    public void TakeDamage(int dmg, int damageMultiplier, int armorPenetration) {
        currentHP -= ((dmg*damageMultiplier)*((1-(defence/100))*(100-armorPenetration)));

        if(currentHP <= 0) {
            isDead = true;
        }
    }

    private void GetStats() {
        attack = Convert.ToInt32(unitLevel * (baseAttack/12) + attackConstant);
        defence = Convert.ToInt32(unitLevel * (baseDefence/20) + defenceConstant);
        maxHP = Convert.ToInt32(unitLevel * baseHP/2 + HPConstant);
        if(unitLevel.Equals(99)) {
            maxMP = 200;
        } else {
            maxMP = Convert.ToInt32(MPConstant + 1 * unitLevel -1);
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
}
