using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalFlag : MonoBehaviour{
    public static ArrayList defeatedTeams = new ArrayList();

    private void Start() {
       //GameObject[] enemiesOnScene = FindObjectsOfType<GameObject>();
       //
       //foreach (GameObject team in enemiesOnScene) {
       //        if(team.CompareTag("Enemy")) {
       //        if(team.GetComponent<UnitTeam>().defeated) {
       //            defeatedTeams.Add(team);
       //            team.SetActive(false);
       //        }
       //    }
       //}
    }

    /// <summary>
    /// Removes all teams in the defetedTeams list, set defeted in UnitTeam to false
    /// and set the team as active.
    /// </summary>
    public void ResetAllEnemies() {
        foreach(GameObject team in defeatedTeams) {
            team.SetActive(true);
            defeatedTeams.Remove(team);
        }
    }

    /// <summary>
    /// If the team is in the defeatedTeams list the team
    /// will be hidden from scene.
    /// </summary>
    /// <param name="team">GameObject team to hide from scene.</param>
    public void IsMyTeamDefeated(GameObject team) {
        foreach(GameObject teamInList in defeatedTeams) {
            //Debug.Log(teamInList.name);
            if(teamInList.Equals(team)) {
                team.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Add a team to the defeatedTeams list.
    /// </summary>
    /// <param name="team">GameObject team to add to the list.</param>
    public void AddDefeatedTeamToList(GameObject team) {
        defeatedTeams.Add(team);
    }

    public void PrintList() {
        foreach (GameObject teamInList in defeatedTeams) {
            Debug.Log(teamInList.name);
        }
    }
}
