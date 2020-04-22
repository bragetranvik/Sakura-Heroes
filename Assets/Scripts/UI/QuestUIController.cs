using UnityEngine;
using UnityEngine.UI;

public class QuestUIController : MonoBehaviour {
    public GameObject questUI;
    public Text questTitle, questDescription, xpReward, goldReward, questState;
    public Button acceptButton, declineButton, claimRewardButton;
    public AudioClip acceptAudio, declineAudio, completeAudio;
    public AudioSource audioSource;
    private Quest quest;

    /// <summary>
    /// Opens the quest GUI and set all the text in it.
    /// </summary>
    /// <param name="currentQuest">The quest to accept/decline.</param>
    public void OpenQuestUI(Quest currentQuest) {
        quest = currentQuest;
        questTitle.text = currentQuest.questName;
        questDescription.text = currentQuest.questDescription;
        xpReward.text = "Xp: " + currentQuest.questXpReward.ToString();
        goldReward.text = "Gold: " + currentQuest.questMoneyReward.ToString();
        questUI.SetActive(true);
    }

    /// <summary>
    /// Adds the quest to the player quest list
    /// if the quest is not null and the player don't already have the quest.
    /// </summary>
    public void AddQuestToPlayerList() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (quest != null && !player.GetComponent<PlayerInventory>().IsQuestAlreadyInList(quest)) {
            DontDestroyOnLoad(quest);
            player.GetComponent<PlayerInventory>().AddQuestToQuestList(quest);
        }

        audioSource.PlayOneShot(acceptAudio);
        questUI.SetActive(false);
    }

    /// <summary>
    /// Play an audio sound and closes the quest GUI.
    /// </summary>
    public void DeclineQuest() {
        audioSource.PlayOneShot(declineAudio);
        questUI.SetActive(false);
    }

    /// <summary>
    /// Opens the quest reward GUI and set all the text in it.
    /// </summary>
    /// <param name="currentQuest">The quest to complete.</param>
    public void OpenQuestRewardUI(Quest currentQuest) {
        quest = currentQuest;
        questTitle.text = currentQuest.questName;
        questDescription.text = currentQuest.questDescription;
        xpReward.text = "Xp: " + currentQuest.questXpReward.ToString();
        goldReward.text = "Gold: " + currentQuest.questMoneyReward.ToString();
        if (quest.rewardHasBeenClaimed) {
            questState.text = "Completed";
            questState.color = new Color(0.05f, 1f, 0.27f);
        } else if (quest.questCompleted) {
            questState.text = "Claim reward";
            questState.color = new Color(1f, 0.78f, 0f);
        } else {
            questState.text = "Not completed";
            questState.color = new Color(1, 0.2f, 0.2f);
        }
        questUI.SetActive(true);
    }

    /// <summary>
    /// Complete the quest and give the player the quest reward if
    /// the quest has been completed and the reward has not been claimed yet.
    /// </summary>
    public void ClaimQuestReward() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (quest != null) {
            if (quest.questCompleted && !quest.rewardHasBeenClaimed) {
                player.GetComponent<PlayerInventory>().GetQuestReward(quest.questXpReward, quest.questMoneyReward);
                quest.rewardHasBeenClaimed = true;
                audioSource.PlayOneShot(completeAudio);
            } else {
                audioSource.PlayOneShot(declineAudio);
            }
        } else {
            audioSource.PlayOneShot(declineAudio);
        }
        questUI.SetActive(false);
    }
}
