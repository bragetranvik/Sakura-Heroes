using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour {
    public GameObject quest1;
    public GameObject quest2;
    public GameObject quest3;

    public void AcceptQuestOne() {
        AddQuestToPlayerList(quest1);
    }

    /// <summary>
    /// Adds the quest to the player quest list
    /// if the quest is not null and the player don't already have the quest.
    /// </summary>
    /// <param name="quest">Quest to add to the list.</param>
    private void AddQuestToPlayerList(GameObject quest) {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (quest != null && !player.GetComponent<PlayerInventory>().IsQuestAlreadyInList(quest)) {
            DontDestroyOnLoad(quest);
            player.GetComponent<PlayerInventory>().AddQuestToQuestList(quest);
        }
    }

    /// <summary>
    /// Checks if the player has completed any of the quests the quest giver have and its not been completed already.
    /// If thats the case the player will gain the xp and money from all the quests.
    /// </summary>
    public void CompleteQuest() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (quest1 != null) {
            if (quest1.GetComponent<Quest>().questCompleted && !quest1.GetComponent<Quest>().rewardHasBeenClaimed) {
                player.GetComponent<PlayerInventory>().GetQuestReward(quest1.GetComponent<Quest>().questXpReward, quest1.GetComponent<Quest>().questMoneyReward);
                quest1.GetComponent<Quest>().rewardHasBeenClaimed = true;
            }
        }
        if (quest2 != null) {
            if (quest2.GetComponent<Quest>().questCompleted && !quest2.GetComponent<Quest>().rewardHasBeenClaimed) {
                player.GetComponent<PlayerInventory>().GetQuestReward(quest2.GetComponent<Quest>().questXpReward, quest2.GetComponent<Quest>().questMoneyReward);
                quest2.GetComponent<Quest>().rewardHasBeenClaimed = true;
            }
        }
        if (quest3 != null) {
            if (quest3.GetComponent<Quest>().questCompleted && !quest3.GetComponent<Quest>().rewardHasBeenClaimed) {
                player.GetComponent<PlayerInventory>().GetQuestReward(quest3.GetComponent<Quest>().questXpReward, quest3.GetComponent<Quest>().questMoneyReward);
                quest3.GetComponent<Quest>().rewardHasBeenClaimed = true;
            }
        }
    }
}