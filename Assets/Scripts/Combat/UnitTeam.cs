using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TeamType { enemyTeam, playerTeam, npcTeam }
public class UnitTeam : MonoBehaviour
{
    public bool defeated = false;
    public TeamType teamType;
    [HideInInspector]
    public string previousScene;
    private static bool aTeamHasBeenDefeated = false;
    public string teamName;

    public GameObject unit1;
    public GameObject unit2;
    public GameObject unit3;

    public LocalFlag defeatedTeamList;
    public GameObject thisTeam;


    private void Start() {
        if(teamType != TeamType.playerTeam) {
            unit1.GetComponent<SpriteRenderer>().flipX = true;
            unit2.GetComponent<SpriteRenderer>().flipX = true;
            unit3.GetComponent<SpriteRenderer>().flipX = true;
        } else {
            unit1.GetComponent<SpriteRenderer>().flipX = false;
            unit2.GetComponent<SpriteRenderer>().flipX = false;
            unit3.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    /// <summary>
    /// This need to change!
    /// </summary>
    private void Update() { 
        if(teamType != TeamType.playerTeam && aTeamHasBeenDefeated.Equals(true)) {
            defeatedTeamList.RemoveDefeatedTeams();
            aTeamHasBeenDefeated = false;
        }
    }
    

    public GameObject GetUnit1GO() {
        return unit1;
    }
    public GameObject GetUnit2GO() {
        return unit2;
    }
    public GameObject GetUnit3GO() {
        return unit3;
    }

    public void SetAnEnemyTeamHasBeenDefeated(bool state) {
        aTeamHasBeenDefeated = state;
    }

    public bool GetAnEnemyTeamHasBeenDefeated() {
        return aTeamHasBeenDefeated;
    }

    public void SetPreviousScene() {
        previousScene = SceneManager.GetActiveScene().name;
    }

    public void AddToDefeatedList() {
        defeatedTeamList.AddDefeatedTeamToList(teamName);
    }

    public void CheckIfDefeated() {
        defeatedTeamList.IsMyTeamDefeated(thisTeam);
    }
}
