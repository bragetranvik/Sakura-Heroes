using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalFlag : MonoBehaviour{
    public ArrayList defeatedTeams = new ArrayList();

    private void Start() {
        GameObject[] enemiesOnScene = FindObjectsOfType<GameObject>();

        foreach (GameObject team in enemiesOnScene) {
                if(team.CompareTag("Enemy")) {
                if(team.GetComponent<UnitTeam>().defeated) {
                    defeatedTeams.Add(team);
                    team.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// Removes all teams in the defetedTeams list, set defeted in UnitTeam to false
    /// and set the team as active.
    /// </summary>
    public void ResetAllEnemies() {
        foreach(GameObject team in defeatedTeams) {
            team.GetComponent<UnitTeam>().defeated = false;
            team.SetActive(true);
            defeatedTeams.Remove(team);
        }
    }
}
