using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    public string unitName;
    public int unitLevel;

    public int baseAttack;
    public int baseDefence;

    public int baseHP;
    public int currentHP;

    public int baseMP;
    public int currentMP;

    [HideInInspector]
    public int attack;
    public int defence;
    public int maxHP;
    public int maxMP;

    private void Start() {
        attack = Convert.ToInt32(baseAttack + (unitLevel * (1+(baseAttack/100))));
        defence = Convert.ToInt32(baseDefence + (unitLevel * (1 + (baseDefence / 100))));
        maxHP = Convert.ToInt32(baseHP + (unitLevel * (1 + (baseHP / 100))));
        maxMP = Convert.ToInt32(baseMP + (unitLevel * (1 + (baseMP / 100))));
    }
}
