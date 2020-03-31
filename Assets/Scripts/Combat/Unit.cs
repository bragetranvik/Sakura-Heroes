using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    public string unitName;
    public int unitLevel;
    public int unitPriceInShop;

    //Base stats should only be between 1-12.
    public int baseAttack, baseDefence, baseHP;
    [HideInInspector]
    public int currentHP, currentMP;

    public Ability ability1, ability2, ability3, ability4;

    [HideInInspector]
    public int attack, defence, maxHP, maxMP;

    private readonly int attackConstant = 5;
    private readonly int defenceConstant = 10;
    private readonly int HPConstant = 100;
    private readonly int MPConstant = 100;

    private int killCount;
    private int dotDmg, dotRoundsLeft;
    private bool dotIsActive = false;

    [HideInInspector]
    public bool isDead = false;
    public Sprite portraitPicture;

    public Vector2 unitPosInBattle;
    public Vector3 unitScaleInBattle;

    /// <summary>
    /// Reduce currentHP of the unit by dmg*dmgMultiplier reduced by defence which get reduced again by armorPenetration.
    /// If target dies from the damage the isDead will be set to true and currentHP will be set to 0.
    /// </summary>
    /// <param name="dmg">The damage to deal.</param>
    /// <param name="damageMultiplier">Multiplies the damage to deal.</param>
    /// <param name="armorPenetration">How many percentage to reduce to defence of the unit.</param>
    /// <returns>The damage done as int.</returns>
    public int TakeDamage(int dmg, float damageMultiplier, int armorPenetration) {
        currentHP -= Convert.ToInt32((dmg*damageMultiplier)*(1f-(defence*(1f-(armorPenetration/100f))/100f)));

        if(currentHP <= 0) {
            isDead = true;
            currentHP = 0;
            //Sets the sprite color to be see through. (Hides the sprite)
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
        return Convert.ToInt32((dmg * damageMultiplier) * (1f - (defence * (1f - (armorPenetration / 100f)) / 100f)));
    }

    /// <summary>
    /// Unit takes flat amount of damage ignores armor.
    /// If target dies from the damage the isDead will be set to true and currentHP will be set to 0.
    /// </summary>
    /// <param name="dmg">Damage unit will take.</param>
    public void TakeDamageIgnoreArmor(int dmg) {
        currentHP -= dmg;

        if (currentHP <= 0) {
            isDead = true;
            currentHP = 0;
            //Sets the sprite color to be see through. (Hides the sprite)
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
    }

    /// <summary>
    /// Set the units HP equal to the parameter.
    /// </summary>
    /// <param name="hpToSet">Amount to set HP equal to.</param>
    public void SetHP(int hpToSet) {
        currentHP = hpToSet;

        if (currentHP <= 0) {
            isDead = true;
            currentHP = 0;
            //Sets the sprite color to be see through. (Hides the sprite)
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
    }

    /// <summary>
    /// Return the damage a unit would take.
    /// </summary>
    /// <param name="dmg">Attack of the unit using the ability.</param>
    /// <param name="dmgMultiplier">Ability damage multiplier.</param>
    /// <param name="armorPenetration">Ability armor penetration.</param>
    /// <returns>Damage unit would take.</returns>
    public int GetDamage(int dmg, float dmgMultiplier, int armorPenetration) {
        return Convert.ToInt32((dmg * dmgMultiplier) * (1f - (defence * (1f - (armorPenetration / 100f)) / 100f)));
    }

    /// <summary>
    /// Heal the unit. If current HP is greater than max HP current HP will
    /// be set to max HP.
    /// </summary>
    /// <param name="amountToHeal">The amount to heal the unit.</param>
    public void Heal(int amountToHeal) {
        if (!isDead) {
            currentHP += amountToHeal;
            if (currentHP > maxHP) {
                currentHP = maxHP;
            }
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
        dotIsActive = false;
        currentHP = maxHP;
        currentMP = maxMP;
    }

    /// <summary>
    /// Sets the dotDmg.
    /// </summary>
    /// <param name="dotDmg">The damage the dot will deal every round as long as its active.</param>
    public void SetDotDmg(int dotDmg) {
        this.dotDmg = dotDmg;
    }

    /// <summary>
    /// Sets "dotIsActive" to true and
    /// sets "dotRoundsLeft" to the same as the parameter.
    /// </summary>
    /// <param name="roundsDotWillLast">The amount of rounds the dot will last.</param>
    public void SetDot(int roundsDotWillLast) {
        dotIsActive = true;
        dotRoundsLeft = roundsDotWillLast;
    }

    /// <summary>
    /// Return true if dot is active.
    /// </summary>
    /// <returns>True if dot is active.</returns>
    public bool IsDotActive() {
        return dotIsActive;
    }

    /// <summary>
    /// Damage the unit the dot damage ignoring all armor.
    /// "dotRoundsLeft" will be set to -1 everytime this function is called.
    /// If "dotRoundsLeft" equals 0 "dotIsActive" will be set to false.
    /// </summary>
    public void TakeDotDmg() {
        if (dotIsActive) {
            currentHP -= dotDmg;
            dotRoundsLeft--;
            if(dotRoundsLeft.Equals(0)) {
                dotIsActive = false;
            }
        }

        if (currentHP <= 0) {
            isDead = true;
            currentHP = 0;
            //Sets the sprite color to be see through. (Hides the sprite)
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
    }

    /// <summary>
    /// Calculates attack, defence, maxHP and maxMP of the unit
    /// and if current HP > maxHP, sets the current HP = maxHP.
    /// </summary>
    public void CalculateStats() {
        attack = Convert.ToInt32(unitLevel * baseAttack/12f + attackConstant);
        defence = Convert.ToInt32(unitLevel * baseDefence/20f + defenceConstant);
        maxHP = Convert.ToInt32(unitLevel * baseHP/2f + HPConstant);
        if(unitLevel.Equals(99)) {
            maxMP = 200;
        } else {
            maxMP = Convert.ToInt32(MPConstant + 1 * unitLevel -1);
        }
        if(currentHP > maxHP) {
            currentHP = Convert.ToInt32(maxHP);
        }
        Repent();
    }

    /// <summary>
    /// Increases the kill count by 1.
    /// </summary>
    public void IncreaseKillCount() {
        killCount++;
    }

    /// <summary>
    /// Return the units kill count.
    /// </summary>
    /// <returns>Units kill count.</returns>
    public int GetKillCount() {
        return this.killCount;
    }

    /// <summary>
    /// Sets the kill count to 0.
    /// </summary>
    public void Repent() {
        killCount = 0;
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
    /// Return the portrait of the unit.
    /// </summary>
    /// <returns>Portrait of the unit</returns>
    public Sprite GetPortraitPicture() {
        return portraitPicture;
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

    /// <summary>
    /// Return the level to use ability.
    /// </summary>
    /// <param name="ability">Has to be "ability1", "ability2", "ability3" or "ability4"</param>
    /// <returns>Level to use the ability.</returns>
    public int GetAbilityLevelToUse(string ability) {
        int levelToUse = 0;
        if (ability.Equals("ability1")) {
            levelToUse = ability1.GetLevelToUse();
        }
        else if (ability.Equals("ability2")) {
            levelToUse = ability2.GetLevelToUse();
        }
        else if (ability.Equals("ability3")) {
            levelToUse = ability3.GetLevelToUse();
        }
        else if (ability.Equals("ability4")) {
            levelToUse = ability4.GetLevelToUse();
        }
        return levelToUse;
    }

    public Vector3 GetScale() {
        return unitScaleInBattle;
    }

    public Vector2 GetPosition() {
        return unitPosInBattle;
    }

    /// <summary>
    /// Do ability 1, 2, 3 or 4 depending on the string ability.
    /// This function will return false if the ability couldn't be used.
    /// </summary>
    /// <param name="ability">Has to be "ability1", "ability2", "ability3" or "ability4"</param>
    /// <param name="unit">The unit which is using the ability.</param>
    /// <param name="target">The chosen target.</param>
    /// <param name="friendly1">Friendly team mate 1.</param>
    /// <param name="friendly2">Friendly team mate 2.</param>
    /// <param name="friendly3">Friendly team mate 3.</param>
    /// <param name="enemy1">Enemy 1.</param>
    /// <param name="enemy2">Enemy 2.</param>
    /// <param name="enemy3">Enemy 3.</param>
    /// <returns>True if ability was successfully used.</returns>
    public bool UseAbility(string ability, Unit unit, Unit target, Unit friendly1, Unit friendly2, Unit friendly3, Unit enemy1, Unit enemy2, Unit enemy3) {
        bool abilityWasSuccessful = false;

        if (ability.Equals("ability1")) {
            if(ability1.DoAbility(unit, target, friendly1, friendly2, friendly3, enemy1, enemy2, enemy3)) {
                abilityWasSuccessful = true;
            }
        }
        else if (ability.Equals("ability2")) {
            if (ability2.DoAbility(unit, target, friendly1, friendly2, friendly3, enemy1, enemy2, enemy3)) {
                abilityWasSuccessful = true;
            }
        }
        else if (ability.Equals("ability3")) {
            if (ability3.DoAbility(unit, target, friendly1, friendly2, friendly3, enemy1, enemy2, enemy3)) {
                abilityWasSuccessful = true;
            }
        }
        else if (ability.Equals("ability4")) {
            if (ability4.DoAbility(unit, target, friendly1, friendly2, friendly3, enemy1, enemy2, enemy3)) {
                abilityWasSuccessful = true;
            }
        }
        return abilityWasSuccessful;
    }
}
