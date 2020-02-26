﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour {
    public int armorPenetration, manaCost, levelToUse, manaDrain;
    public float damageMultiplier, executeDmgMultiplier, pctAOEDmg, pctLifeSteal;
    public string abilityName;
    [TextArea(7, 7)]
    public string abilityTooltip;
    [Tooltip("simpleDmg, simpleDmgLifeSteal, executeDmg, executeDmgLifeSteal, AOEDmg")]
    public string abilityType;
    public bool isHeal = false;

    /// <summary>
    /// Uses the global variable abilityType to use the correct type of ability.
    /// </summary>
    /// <param name="unit">The unit which is using the ability.</param>
    /// <param name="target">The chosen target.</param>
    /// <param name="friendly1">Friendly team mate 1.</param>
    /// <param name="friendly2">Friendly team mate 2.</param>
    /// <param name="friendly3">Friendly team mate 3.</param>
    /// <param name="enemy1">Enemy 1.</param>
    /// <param name="enemy2">Enemy 2.</param>
    /// <param name="enemy3">Enemy 3.</param>
    /// <returns>True if the ability was successfully used.</returns>
    public bool DoAbility(Unit unit, Unit target, Unit friendly1, Unit friendly2, Unit friendly3, Unit enemy1, Unit enemy2, Unit enemy3) {
        bool abilityWasSuccessful = false;

        //Ability will only be used if unit got enough mana for it and the correct level for it.
        //Also important that the global variable is correct.
        if (unit.DrainMana(manaCost) && (unit.unitLevel >= levelToUse)) {
            switch (abilityType) {

                case "simpleDmg":
                    target.TakeDamage(unit.attack, damageMultiplier, armorPenetration);
                    break;

                case "simpleDmgLifeSteal":
                    unit.Heal(Convert.ToInt32(target.TakeDamage(unit.attack, damageMultiplier, armorPenetration)*pctLifeSteal / 100f));
                    break;

                case "executeDmg":
                    //If target has under 25% HP the ability will deal x times more damage, if not it will deal normal damage with 0 armor penetration.
                    if (IsTargetInExecuteRange(target, 25)) {
                        target.TakeDamage(unit.attack, damageMultiplier * executeDmgMultiplier, armorPenetration);
                    }
                    else {
                        target.TakeDamage(unit.attack, damageMultiplier, armorPenetration * 0);
                    }
                    break;

                case "executeDmgLifeSteal":
                    //If target has under 25% HP the ability will deal x times more damage healing x% of the damage dealt, if not it will deal normal damage with 0 armor penetration.
                    if (IsTargetInExecuteRange(target, 25)) {
                        unit.Heal(Convert.ToInt32(target.TakeDamage(unit.attack, damageMultiplier * executeDmgMultiplier, armorPenetration) * pctLifeSteal / 100f));
                    }
                    else {
                        unit.Heal(Convert.ToInt32(target.TakeDamage(unit.attack, damageMultiplier, armorPenetration * 0) * pctLifeSteal / 100f));
                    }
                    break;

                case "AOEDmg":
                    int damageDone = target.TakeDamage(unit.attack, damageMultiplier, armorPenetration);
                    if(!target.Equals(enemy1)) {
                        enemy1.TakeDamageIgnoreArmor(Convert.ToInt32(damageDone * pctAOEDmg/100f));
                    }
                    if (!target.Equals(enemy2)) {
                        enemy2.TakeDamageIgnoreArmor(Convert.ToInt32(damageDone * pctAOEDmg/100f));
                    }
                    if (!target.Equals(enemy3)) {
                        enemy3.TakeDamageIgnoreArmor(Convert.ToInt32(damageDone * pctAOEDmg/100f));
                    }
                    break;

                default:
                    Debug.Log("Something went wrong with DoAbility!");
                    break;
            }
            abilityWasSuccessful = true;
        }
        return abilityWasSuccessful;
    }

    /// <summary>
    /// Checks if target is in execute range.
    /// </summary>
    /// <param name="target">The target to check.</param>
    /// <param name="percentHPForExecuteToWork">A number 1-100 where 20 would be 20% HP left for execute to work.</param>
    /// <returns></returns>
    private bool IsTargetInExecuteRange(Unit target, int percentHPForExecuteToWork) {
        bool targetIsInRange = false;

        if(target.currentHP <= target.maxHP * (percentHPForExecuteToWork/100f)) {
            targetIsInRange = true;
        }
        return targetIsInRange;
    }

    /// <summary>
    /// Return true if the unit got mana for the ability.
    /// </summary>
    /// <param name="unit">The unit to check.</param>
    /// <returns>True if unit got mana for the ability.</returns>
    public bool UnitGotManaForAbility(Unit unit) {
        bool unitGotManaForAbility = false;

        if(unit.currentMP >= GetManaCost()) {
            unitGotManaForAbility = true;
        }
        return unitGotManaForAbility;
    }

    /// <summary>
    /// Return damage the ability would do.
    /// </summary>
    /// <param name="unit">Unit using the ability.</param>
    /// <param name="target">Target of the ability.</param>
    /// <returns>Damage the ability would do.</returns>
    public int GetAbilityDamageOnTargt(Unit unit, Unit target) {
        return target.GetDamage(unit.attack, this.damageMultiplier, this.armorPenetration);
    }

    /// <summary>
    /// Return name of the ability.
    /// </summary>
    /// <returns>Name of the ability.</returns>
    public string GetAbilityName() {
        return this.abilityName;
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
    /// Return name of the ability.
    /// </summary>
    /// <returns>Name of the ability.</returns>
    public int GetAbilityDamage(Unit unit) {
        return Convert.ToInt32(this.damageMultiplier * unit.attack);
    }
}