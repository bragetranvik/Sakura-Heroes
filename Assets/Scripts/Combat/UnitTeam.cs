using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TeamType { enemyTeam, playerTeam, npcTeam }
public class UnitTeam : MonoBehaviour
{
    [HideInInspector]
    public bool defeated = false;
    public TeamType teamType;
    [HideInInspector]
    public string previousScene;
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

    private void Update() {
        AddPlayerPetsToUnitTeam();
    }

    /// <summary>
    /// Adds pets from battlePetList instead of pets added via inspector.
    /// </summary>
    private void AddPlayerPetsToUnitTeam() {
        if (teamType.Equals(TeamType.playerTeam)) {
            foreach (GameObject pet in GetComponentInParent<PlayerInventory>().battlePetList) {
                switch (GetComponentInParent<PlayerInventory>().battlePetList.IndexOf(pet)) {
                    case 0:
                        unit1 = pet;
                        break;

                    case 1:
                        unit2 = pet;
                        break;

                    case 2:
                        unit3 = pet;
                        break;

                    default:
                        break;
                }
            }
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
