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
}