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

    public void TakeDamage(int dmg) {
        currentHP -= (dmg*(1-(defence/100)));

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
}
