using UnityEngine;

public class EnemyAI : MonoBehaviour {
    private bool gonnaHeal = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="unit">The enemy which has the turn.</param>
    /// <param name="friendly1">Enemies friendly1.</param>
    /// <param name="friendly2">Enemies friendly2.</param>
    /// <param name="friendly3">Enemies friendly3.</param>
    /// <param name="enemy1">Enemies enemy1.</param>
    /// <param name="enemy2">Enemies enemy2.</param>
    /// <param name="enemy3">Enemies enemy3.</param>
    /// <returns>Ability the unit is gonna use as string. (ability1, 2, 3, 4)</returns>
    public string DoEnemyAI(Unit unit, Unit friendly1, Unit friendly2, Unit friendly3, Unit enemy1, Unit enemy2, Unit enemy3) {
        Unit target = ChooseTarget(unit, enemy1, enemy2, enemy3, friendly1, friendly2, friendly3);
        string abilityToUse = ChooseAbility(unit, target);
        UseAbilityOnTarget(abilityToUse, unit, target, friendly1, friendly2, friendly3, enemy1, enemy2, enemy3);
        return abilityToUse;
    }

    /// <summary>
    /// Choose an ability to use depending on if the target would die from the ability or not.
    /// If not a roll will choose the ability if its not a heal with a higher weight to roll ability1. 
    /// </summary>
    /// <param name="unit">The unit to choose an ability to use.</param>
    /// <param name="target">The target of the ability.</param>
    /// <returns>A string with what ability to use. (ability1, 2, 3, 4)</returns>
    private string ChooseAbilityOld(Unit unit, Unit target) {
        string abilityToUse;
        int abilityRoll = RandomNumber(1, 6);

        if (EnemyInKillRange(unit, target, unit.ability1)) {
            abilityToUse = "ability1";
        } else if (EnemyInKillRange(unit, target, unit.ability2) && unit.ability2.UnitGotManaForAbility(unit) && !unit.ability2.abilityType.Equals(AbilityType.Heal)) {
            abilityToUse = "ability2";
        } else if (EnemyInKillRange(unit, target, unit.ability3) && unit.ability3.UnitGotManaForAbility(unit) && !unit.ability3.abilityType.Equals(AbilityType.Heal)) {
            abilityToUse = "ability3";
        } else if (EnemyInKillRange(unit, target, unit.ability4) && unit.ability4.UnitGotManaForAbility(unit) && !unit.ability4.abilityType.Equals(AbilityType.Heal)) {
            abilityToUse = "ability4";
        } else if (unit.currentMP >= 130 && !unit.ability4.abilityType.Equals(AbilityType.Heal)) {
            abilityToUse = "ability4";
        } else if (unit.ability4.UnitGotManaForAbility(unit) && !unit.ability4.isHeal && abilityRoll.Equals(5)) {
            abilityToUse = "ability4";
        } else if (unit.ability3.UnitGotManaForAbility(unit) && !unit.ability3.isHeal && abilityRoll.Equals(4)) {
            abilityToUse = "ability3";
        } else if (unit.ability2.UnitGotManaForAbility(unit) && !unit.ability2.isHeal && abilityRoll.Equals(3)) {
            abilityToUse = "ability2";
        } else {
            abilityToUse = "ability1";
        }
        return abilityToUse;
    }

    /// <summary>
    /// Choose the ability depending on some requirements.
    /// If no requirements is fulfilled the ability will be random
    /// with some higher weight on ability1.
    /// </summary>
    /// <param name="unit">The unit to choose an ability to use.</param>
    /// <param name="target">The target of the ability.</param>
    /// <returns>A string with what ability to use. (ability1, 2, 3, 4)</returns>
    private string ChooseAbility(Unit unit, Unit target) {
        string abilityToUse;
        int abilityRoll = RandomNumber(1, 6);

        if (gonnaHeal && unit.ability4.abilityType.Equals(AbilityType.Heal) && unit.ability4.UnitGotManaForAbility(unit)) {
            abilityToUse = "ability4";
        } else if (EnemyInKillRange(unit, target, unit.ability1)) {
            abilityToUse = "ability1";
        } else if (EnemyInKillRange(unit, target, unit.ability2) && unit.ability2.UnitGotManaForAbility(unit) && !unit.ability2.abilityType.Equals(AbilityType.Heal) && unit.ability2.IsAbilityRequirementfulfilled(unit, target)) {
            abilityToUse = "ability2";
        } else if (EnemyInKillRange(unit, target, unit.ability3) && unit.ability3.UnitGotManaForAbility(unit) && !unit.ability3.abilityType.Equals(AbilityType.Heal) && unit.ability3.IsAbilityRequirementfulfilled(unit, target)) {
            abilityToUse = "ability3";
        } else if (EnemyInKillRange(unit, target, unit.ability4) && unit.ability4.UnitGotManaForAbility(unit) && !unit.ability4.abilityType.Equals(AbilityType.Heal) && unit.ability4.IsAbilityRequirementfulfilled(unit, target)) {
            abilityToUse = "ability4";
        } else if(unit.ability4.UnitGotManaForAbility(unit) && !unit.ability4.abilityType.Equals(AbilityType.Heal) && unit.ability4.IsAbilityRequirementfulfilled(unit, target) && unit.ability4.IsSpecialAbility()) {
            abilityToUse = "ability4";
        } else if(unit.ability3.UnitGotManaForAbility(unit) && !unit.ability3.abilityType.Equals(AbilityType.Heal) && unit.ability3.IsAbilityRequirementfulfilled(unit, target) && unit.ability3.IsSpecialAbility()) {
            abilityToUse = "ability3";
        } else if(unit.ability2.UnitGotManaForAbility(unit) && !unit.ability2.abilityType.Equals(AbilityType.Heal) && unit.ability2.IsAbilityRequirementfulfilled(unit, target) && unit.ability2.IsSpecialAbility()) {
            abilityToUse = "ability2";
        } else if(unit.currentMP >= 130 && !unit.ability4.abilityType.Equals(AbilityType.Heal) && unit.ability4.IsAbilityRequirementfulfilled(unit, target)) {
            abilityToUse = "ability4";
        } else {
            if (unit.ability4.IsSpecialAbility()) {
                if (unit.ability3.UnitGotManaForAbility(unit) && !unit.ability3.isHeal && abilityRoll >= 4) {
                    abilityToUse = "ability3";
                }
                else if (unit.ability2.UnitGotManaForAbility(unit) && !unit.ability2.isHeal && abilityRoll.Equals(3)) {
                    abilityToUse = "ability2";
                } else {
                    abilityToUse = "ability1";
                }
            } else if(unit.ability3.IsSpecialAbility()) {
                if (unit.ability4.UnitGotManaForAbility(unit) && !unit.ability4.isHeal && abilityRoll.Equals(5)) {
                    abilityToUse = "ability4";
                }
                else if (unit.ability2.UnitGotManaForAbility(unit) && !unit.ability2.isHeal && (abilityRoll.Equals(3) || abilityRoll.Equals(4))) {
                    abilityToUse = "ability2";
                }
                else {
                    abilityToUse = "ability1";
                }
            } else if (unit.ability2.IsSpecialAbility()) {
                if (unit.ability4.UnitGotManaForAbility(unit) && !unit.ability4.isHeal && abilityRoll.Equals(5)) {
                    abilityToUse = "ability4";
                } else if (unit.ability3.UnitGotManaForAbility(unit) && !unit.ability2.isHeal && (abilityRoll.Equals(3) || abilityRoll.Equals(4))) {
                    abilityToUse = "ability3";
                } else {
                    abilityToUse = "ability1";
                }
            } else {
                if (unit.ability4.UnitGotManaForAbility(unit) && !unit.ability4.isHeal && abilityRoll.Equals(5)) {
                    abilityToUse = "ability4";
                } else if (unit.ability3.UnitGotManaForAbility(unit) && !unit.ability3.isHeal && abilityRoll.Equals(4)) {
                    abilityToUse = "ability3";
                } else if (unit.ability2.UnitGotManaForAbility(unit) && !unit.ability2.isHeal && abilityRoll.Equals(3)) {
                    abilityToUse = "ability2";
                } else {
                    abilityToUse = "ability1";
                }
            }
        }

        return abilityToUse;
    }

    /// <summary>
    /// If roll is 1, chosen target will be the enemy with lowest health,
    /// if all units got the same current health it will be the unit with the lowest defence,
    /// if all units got the same defence it will be a random out of the 3.
    /// If roll is 2 a random target will be chosen.
    /// </summary>
    /// <param name="enemyUnit1">Enemy unit 1.</param>
    /// <param name="enemyUnit2">Enemy unit 2.</param>
    /// <param name="enemyUnit3">Enemy unit 3.</param>
    /// <returns>A unit to attack.</returns>
    private Unit ChooseEnemyTarget(Unit enemyUnit1, Unit enemyUnit2, Unit enemyUnit3) {
        Unit chosenTarget;
        if (RandomNumber(1, 3).Equals(1)) {
            if ((GetUnitWithLowestHealth(enemyUnit1, enemyUnit2, enemyUnit3) == enemyUnit1) && !enemyUnit1.isDead) {
                chosenTarget = enemyUnit1;
            } else if ((GetUnitWithLowestHealth(enemyUnit1, enemyUnit2, enemyUnit3) == enemyUnit2) && !enemyUnit2.isDead) {
                chosenTarget = enemyUnit2;
            } else if ((GetUnitWithLowestHealth(enemyUnit1, enemyUnit2, enemyUnit3) == enemyUnit3) && !enemyUnit3.isDead) {
                chosenTarget = enemyUnit3;
            } else if ((GetUnitWithLowestDefence(enemyUnit1, enemyUnit2, enemyUnit3) == enemyUnit1) && !enemyUnit1.isDead) {
                chosenTarget = enemyUnit1;
            } else if ((GetUnitWithLowestDefence(enemyUnit1, enemyUnit2, enemyUnit3) == enemyUnit2) && !enemyUnit2.isDead) {
                chosenTarget = enemyUnit2;
            } else if ((GetUnitWithLowestDefence(enemyUnit1, enemyUnit2, enemyUnit3) == enemyUnit3) && !enemyUnit3.isDead) {
                chosenTarget = enemyUnit3;
            } else {
                chosenTarget = GetRandomUnit(enemyUnit1, enemyUnit2, enemyUnit3);
            }
        } else {
            chosenTarget = GetRandomUnit(enemyUnit1, enemyUnit2, enemyUnit3);
        }
        return chosenTarget;
    }

    private Unit ChooseTarget(Unit unit, Unit enemyUnit1, Unit enemyUnit2, Unit enemyUnit3, Unit friendly1, Unit friendly2, Unit friendly3) {
        Unit chosenTarget;

        if(UnitGotAbilityType(unit, AbilityType.Heal) && (DoUnitHasLessHealthLeft(friendly1, 0.4f) || DoUnitHasLessHealthLeft(friendly2, 0.4f) || DoUnitHasLessHealthLeft(friendly3, 0.4f))) {
            gonnaHeal = true;
            chosenTarget = GetUnitWithLowestHealth(friendly1, friendly2, friendly3);
        } else if (RandomNumber(1, 3).Equals(1)) {
            //GetUnitWithLowestHealth return null if all units got the same health.
            if (GetUnitWithLowestHealth(enemyUnit1, enemyUnit2, enemyUnit3) != null) {
                chosenTarget = GetUnitWithLowestHealth(enemyUnit1, enemyUnit2, enemyUnit3);
            } else if (GetUnitWithLowestDefence(enemyUnit1, enemyUnit2, enemyUnit3) != null) {
                chosenTarget = GetUnitWithLowestDefence(enemyUnit1, enemyUnit2, enemyUnit3);
            } else {
                chosenTarget = GetRandomUnit(enemyUnit1, enemyUnit2, enemyUnit3);
            }
        } else {
            chosenTarget = GetRandomUnit(enemyUnit1, enemyUnit2, enemyUnit3);
        }

        return chosenTarget;
    }

    /// <summary>
    /// Checks if a unit got less than x% amount of health left.
    /// Wont return true on a dead unit.
    /// </summary>
    /// <param name="unit">The unit to check</param>
    /// <param name="x">The % of health left divided by 100</param>
    /// <returns>True if unit got less or equal than maxHP*x amount of health left.</returns>
    private bool DoUnitHasLessHealthLeft(Unit unit, float x) {
        bool unitHasLessHealthLeft = false;

        if((unit.currentHP <= unit.maxHP*x) && !unit.isDead) {
            unitHasLessHealthLeft = true;
        }
        return unitHasLessHealthLeft;
    }

    /// <summary>
    /// Return true if unit got the ability type.
    /// </summary>
    /// <param name="unit">Unit to check.</param>
    /// <param name="abilityType">Ability type to check.</param>
    /// <returns>True of unit got the ability type</returns>
    private bool UnitGotAbilityType(Unit unit, AbilityType abilityType) {
        bool unitGotTheAbilityType = false;

        if(unit.ability1.abilityType.Equals(abilityType) ||
            unit.ability2.abilityType.Equals(abilityType) ||
            unit.ability3.abilityType.Equals(abilityType) ||
            unit.ability4.abilityType.Equals(abilityType)) {
            unitGotTheAbilityType = true;
        }

        return unitGotTheAbilityType;
    }

    /// <summary>
    /// Return true if the ability damage will kill the target.
    /// </summary>
    /// <param name="unit">Unit using the ability.</param>
    /// <param name="target">Target of the ability.</param>
    /// <param name="ability">Ability the unit is gonna check.</param>
    /// <returns>True if the ability will kill the unit.</returns>
    private bool EnemyInKillRange(Unit unit, Unit target, Ability ability) {
        bool unitIsInKillRange = false;
        if(target.currentHP <= ability.GetAbilityDamageOnTargt(unit, target)) {
            unitIsInKillRange = true;
        }
        return unitIsInKillRange;
    }

    /// <summary>
    /// Return the unit with the lowest health out of 3 units which is not dead. 
    /// If all units got the same current health this function will return null.
    /// </summary>
    /// <param name="unit1">First unit to compare.</param>
    /// <param name="unit2">Second unit to compare.</param>
    /// <param name="unit3">Third unit to compare.</param>
    /// <returns>Unit with the lowest current health which is not dead. Null if all units got the same current health.</returns>
    private Unit GetUnitWithLowestHealth(Unit unit1, Unit unit2, Unit unit3) {
        Unit unitWithLowestHealth = null;
        if((unit1.currentHP < unit2.currentHP && unit1.currentHP < unit3.currentHP) && !unit1.isDead) {
            unitWithLowestHealth = unit1;
        } else if((unit2.currentHP < unit1.currentHP && unit2.currentHP < unit3.currentHP) && !unit2.isDead) {
            unitWithLowestHealth = unit2;
        } else if((unit3.currentHP < unit1.currentHP && unit3.currentHP < unit2.currentHP) && !unit3.isDead) {
            unitWithLowestHealth = unit3;
        }
        return unitWithLowestHealth;
    }

    /// <summary>
    /// Return the unit with the lowest defence out of 3 units which is not dead. 
    /// If all units got the same defence this function will return null.
    /// </summary>
    /// <param name="unit1">First unit to compare.</param>
    /// <param name="unit2">Second unit to compare.</param>
    /// <param name="unit3">Third unit to compare.</param>
    /// <returns>Unit with the lowest defence which is not dead. Null if all units got the same defence.</returns>
    private Unit GetUnitWithLowestDefence(Unit unit1, Unit unit2, Unit unit3) {
        Unit unitWithLowestHealth = null;
        if ((unit1.defence < unit2.defence && unit1.defence < unit3.defence) && !unit1.isDead) {
            unitWithLowestHealth = unit1;
        } else if ((unit2.defence < unit1.defence && unit2.defence < unit3.defence) && !unit2.isDead) {
            unitWithLowestHealth = unit2;
        } else if ((unit3.defence < unit1.defence && unit3.defence < unit2.defence) && !unit3.isDead) {
            unitWithLowestHealth = unit3;
        }
        return unitWithLowestHealth;
    }

    /// <summary>
    /// Return a random unit which is not dead out of the 3 units.
    /// If all units are dead this will return unit1.
    /// </summary>
    /// <param name="unit1">First unit.</param>
    /// <param name="unit2">Second unit.</param>
    /// <param name="unit3">Third unit.</param>
    /// <returns>Random unit out of the 3 units which is not dead.</returns>
    private Unit GetRandomUnit(Unit unit1, Unit unit2, Unit unit3) {
        Unit randomUnit = unit1;
        bool foundTargetIsDead = true;

        if (!AllUnitsAreDead(unit1, unit2, unit3)) {
            while (foundTargetIsDead) {
                int randomTarget = RandomNumber(1, 4);

                if (randomTarget.Equals(1)) {
                    if (!unit1.isDead) {
                        randomUnit = unit1;
                        foundTargetIsDead = false;
                    }
                }
                else if (randomTarget.Equals(2)) {
                    if (!unit2.isDead) {
                        randomUnit = unit2;
                        foundTargetIsDead = false;
                    }
                }
                else {
                    if (!unit3.isDead) {
                        randomUnit = unit3;
                        foundTargetIsDead = false;
                    }
                }
            }
        }
        return randomUnit;
    }

    /// <summary>
    /// Return true if all the units are dead.
    /// </summary>
    /// <param name="unit1">First unit.</param>
    /// <param name="unit2">Second unit.</param>
    /// <param name="unit3">Third unit.</param>
    /// <returns>True if all units are dead.</returns>
    private bool AllUnitsAreDead(Unit unit1, Unit unit2, Unit unit3) {
        bool allUnitsAreDead = false;
        if(unit1.isDead && unit2.isDead && unit3.isDead) {
            allUnitsAreDead = true;
        }
        return allUnitsAreDead;
    }

    /// <summary>
    /// Generates a random number between and including min to max and return the number as int.
    /// </summary>
    /// <param name="min">The minium value to be rolled as integer.</param>
    /// <param name="max">The maxium value to be rolled as integer.</param>
    /// <returns>Return the random number as int.</returns>
    private int RandomNumber(int minIncluding, int maxNotIncluding) {
        int random = UnityEngine.Random.Range(minIncluding, maxNotIncluding);
        return random;
    }

    /// <summary>
    /// Use the chosen ability on the chosen target.
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
    private void UseAbilityOnTarget(string abilityToUse, Unit unit, Unit target, Unit friendly1, Unit friendly2, Unit friendly3, Unit enemy1, Unit enemy2, Unit enemy3) {
        unit.UseAbility(abilityToUse, unit, target, friendly1, friendly2, friendly3, enemy1, enemy2, enemy3);
    }
}