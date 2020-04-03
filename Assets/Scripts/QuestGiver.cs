using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour {
    public Quest quest1;
    public Quest quest2;
    public Quest quest3;
    public GameObject questUI, completeQuestUI;

    /// <summary>
    /// Opens the quest GUI for quest3.
    /// </summary>
    public void OpenQuestOneUI() {
        questUI.GetComponent<QuestUIController>().OpenQuestUI(quest1);
    }

    /// <summary>
    /// Opens the quest GUI for quest3.
    /// </summary>
    public void OpenQuestTwoUI() {
        questUI.GetComponent<QuestUIController>().OpenQuestUI(quest2);
    }

    /// <summary>
    /// Opens the quest GUI for quest3.
    /// </summary>
    public void OpenQuestThreeUI() {
        questUI.GetComponent<QuestUIController>().OpenQuestUI(quest3);
    }

    /// <summary>
    /// Open the quest reward GUI for quest1.
    /// </summary>
    public void CompleteQuestOne() {
        completeQuestUI.GetComponent<QuestUIController>().OpenQuestRewardUI(quest1);
    }

    /// <summary>
    /// Open the quest reward GUI for quest2.
    /// </summary>
    public void CompleteQuestTwo() {
        completeQuestUI.GetComponent<QuestUIController>().OpenQuestRewardUI(quest2);
    }

    /// <summary>
    /// Open the quest reward GUI for quest3.
    /// </summary>
    public void CompleteQuestThree() {
        completeQuestUI.GetComponent<QuestUIController>().OpenQuestRewardUI(quest3);
    }
}