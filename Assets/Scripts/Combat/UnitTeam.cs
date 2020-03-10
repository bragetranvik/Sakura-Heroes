using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitTeam : MonoBehaviour
{
    public bool defeated = false;
    public bool playerTeam = false;
    public string previousScene;

    public GameObject unit1;
    public GameObject unit2;
    public GameObject unit3;

    public LocalFlag defeatedTeamList;
    public GameObject thisTeam;


    private void Start() {
        Debug.Log("Start in unitTeam has been run");
        if(playerTeam.Equals(false)) {
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
        defeatedTeamList.AddDefeatedTeamToList(thisTeam);
        defeatedTeamList.PrintList();
    }

    public void CheckIfDefeated() {
        defeatedTeamList.IsMyTeamDefeated(thisTeam);
    }
}
