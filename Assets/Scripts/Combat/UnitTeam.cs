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


    private void Start() {
        if(playerTeam.Equals(false)) {
            unit1.GetComponent<SpriteRenderer>().flipX = true;
            unit2.GetComponent<SpriteRenderer>().flipX = true;
            unit3.GetComponent<SpriteRenderer>().flipX = true;
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

    public void setPreviousScene() {
        previousScene = SceneManager.GetActiveScene().name;
    }
}
