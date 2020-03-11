using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalFlag : MonoBehaviour{
    public static ArrayList defeatedTeams = new ArrayList();

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
            if(teamInList.Equals(team)) {
                team.SetActive(false);
            }
        }
    }

    public void RemoveDefeatedTeams() {
        GameObject[] enemiesOnScene = FindObjectsOfType<GameObject>();
        foreach (GameObject teamInScene in enemiesOnScene) {
            if(teamInScene.CompareTag("Enemy")) {
                foreach (string teamName in defeatedTeams) {
                    if(teamInScene.GetComponent<UnitTeam>().teamName.Equals(teamName)) {
                        teamInScene.SetActive(false);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Add the team name to the defeatedTeams list.
    /// </summary>
    /// <param name="teamName">Name of the defeated team.</param>
    public void AddDefeatedTeamToList(string teamName) {
        defeatedTeams.Add(teamName);
    }

    public void PrintList() {
        foreach (string teamInList in defeatedTeams) {
            Debug.Log(teamInList);
        }
    }
}
