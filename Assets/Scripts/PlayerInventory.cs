using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    public GameObject player;
    private static int currentXP;
    public int level = 1;

    public Image UICharacterIcon;
    public Text UIMoneyCounter;
    public Text UILevelCounter;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        SetPetLevelsToPlayerLevel();
        UpdateUI();
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

    private void UpdateUI()
    {
        Debug.Log("UpdateUI called");
        // Set money in UI (Not implemented yet)
        UIMoneyCounter.text = "Money: " + Convert.ToString(37);
        // Set level in UI
        UILevelCounter.text = "Level: " + Convert.ToString(level);
    }
}
