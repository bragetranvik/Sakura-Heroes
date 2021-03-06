﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    public GameObject player;
    public Sprite characterPortrait;

    private static int currentXP;
    public int level = 1;
    public int totalMoney;

    public List<Quest> questList = new List<Quest>();
    public List<GameObject> petList = new List<GameObject>();
    public List<GameObject> battlePetList = new List<GameObject>();

    // Update is called once per frame
    void Update() {
        SetPetLevelsToPlayerLevel();
    }

    // Update is called once per frame
    void Start()
    {
        battlePetList.Add(player.GetComponent<UnitTeam>().unit1);
        battlePetList.Add(player.GetComponent<UnitTeam>().unit2);
        battlePetList.Add(player.GetComponent<UnitTeam>().unit3);
    }

    /// <summary>
    /// Increase the players current xp. If the xp is greater than
    /// the xp needed for next level the players level will increase by one,
    /// and then set the remaining xp to current xp.
    /// </summary>
    /// <param name="enemyLevel">Level of the enemy team.</param>
    /// <returns>True if the player level up from the xp.</returns>
    public bool GainXPFromEnemies(int enemyLevel) {
        bool leveledUp = false;

        currentXP += Convert.ToInt32(enemyLevel+50f*Mathf.Pow(1.7f, (enemyLevel/7f)));
        if(currentXP >= CalculateXpToNextLevel()) {
            currentXP -= CalculateXpToNextLevel();
            level++;
            leveledUp = true;
        }
        return leveledUp;
    }

    /// <summary>
    /// Increase the players current xp. If the xp is greater than
    /// the xp needed for next level the players level will increase by one,
    /// and then set the remaining xp to current xp.
    /// </summary>
    /// <param name="xpToGain">Xp to gain from quest.</param>
    private void GainXpFromQuest(int xpToGain) {
        currentXP += xpToGain;
        if (currentXP >= CalculateXpToNextLevel()) {
            currentXP -= CalculateXpToNextLevel();
            level++;
        }
    }

    /// <summary>
    /// Calculate xp the player need for the next level.
    /// </summary>
    /// <returns>Xp the player need for the next level.</returns>
    private int CalculateXpToNextLevel() {
        return Convert.ToInt32(Mathf.Floor(((level + 1f) - 1f + 300f * Mathf.Pow(2, ((level + 1f) - 1f) / 7f)) / 4f));
    }

    /// <summary>
    /// Return the player level.
    /// </summary>
    /// <returns>Level of the player.</returns>
    public int GetPlayerLevel() {
        return level;
    }

    /// <summary>
    /// Sets the players team level to the same level as the player.
    /// </summary>
    private void SetPetLevelsToPlayerLevel() {
        UnitTeam playerTeam = player.GetComponent<UnitTeam>();

        if(playerTeam.unit1 != null) {
            playerTeam.unit1.GetComponent<Unit>().unitLevel = level;
        }
        if (playerTeam.unit2 != null) {
            playerTeam.unit2.GetComponent<Unit>().unitLevel = level;
        }
        if (playerTeam.unit3 != null) {
            playerTeam.unit3.GetComponent<Unit>().unitLevel = level;
        }
    }

    /// <summary>
    /// Add a quest to the quest list.
    /// </summary>
    /// <param name="quest">Quest to add to the list.</param>
    /// <returns>True if quest was successfully added to the list.</returns>
    public bool AddQuestToQuestList(Quest quest) {
        bool questWasAdded = false;

        if(!IsQuestAlreadyInList(quest)) {
            questList.Add(quest);
            questWasAdded = true;
        }
        return questWasAdded;
    }

    /// <summary>
    /// Check if the quest is already in the list. 
    /// </summary>
    /// <param name="quest">The quest to check for.</param>
    /// <returns>True if the quest is already in the list.</returns>
    public bool IsQuestAlreadyInList(Quest quest) {
        bool questIsInList = false;

        foreach (Quest questInList in questList) {
            if (questInList.questName.Equals(quest.questName)) {
                questIsInList = true;
            }
        }
        return questIsInList;
    }

    /// <summary>
    /// Check if a quest is completed and complete it if it is.
    /// </summary>
    /// <param name="objective">Example the name of the enemyTeam or an area.</param>
    public void CheckForCompletedQuest(string objective) {
        foreach(Quest questInList in questList) {
            if(questInList.requirement.Equals(objective) && !questInList.questCompleted) {
                questInList.CompleteQuest(objective);
            }
        }
    }

    /// <summary>
    /// Calculate what the new total money will be
    /// </summary>
    /// <param name="moneyEarned">How much money the player just earned</param>
    public void EarnMoney(int moneyEarned) {

        totalMoney += moneyEarned;

    }

    public int CalculateHowMuchMoneyToEarn(int enemyLevel) {

        int howMuchToEarn = Convert.ToInt32(50f * Mathf.Pow(1.7f, (enemyLevel / 7f)));

        return howMuchToEarn;

    }

    /// <summary>
    /// Gain the quest reward.
    /// </summary>
    /// <param name="xpReward">Xp to get from the quest.</param>
    /// <param name="moneyReward">Money to get from the quest.</param>
    public void GetQuestReward(int xpReward, int moneyReward) {
        GainXpFromQuest(xpReward);
        EarnMoney(moneyReward);
    }

    public void AddPetToList(GameObject petToAdd)
    {
        petList.Add(petToAdd);
    }

    public GameObject GetPetInList(int index)
    {
        GameObject petInIndex = petList[index];
        return petInIndex;
    }
}