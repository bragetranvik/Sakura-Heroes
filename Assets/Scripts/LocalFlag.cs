using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalFlag : MonoBehaviour{
    public ArrayList defeatedTeams = new ArrayList();

    private void Start() {
        GameObject[] enemiesOnScene = FindObjectsOfType<GameObject>();

        foreach (GameObject team in enemiesOnScene) {
                if(team.CompareTag("Enemy")) {
                if(team.GetComponent<UnitTeam>().deafeted) {
                    defeatedTeams.Add(team);
                    team.SetActive(false);
                }
            }
        }
    }
}
