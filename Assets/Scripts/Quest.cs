using UnityEngine;

public enum QuestType { defeatQuest, exploreQuest }
public class Quest : MonoBehaviour {
    public string questName;
    [TextArea(7, 7)]
    public string questDescription;
    public QuestType questType;
    public string requirement;
    [HideInInspector]
    public bool questCompleted = false;
    [HideInInspector]
    public bool rewardHasBeenClaimed = false;
    public int questXpReward;
    public int questMoneyReward;

    public void CompleteQuest(string other) {

        switch(questType) {
            case QuestType.defeatQuest:
                if(other.Equals(requirement)) {
                    questCompleted = true;
                    Debug.Log("Quest is completed");
                }
                break;

            case QuestType.exploreQuest:
                break;

            default:
                break;
        }
    }
}