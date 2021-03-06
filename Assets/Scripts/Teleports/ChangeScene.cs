﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public string sceneToLoad;
    public Vector3 playerPosInNewScene;

    public static Vector3 playerPos;
    private static bool intoNewScene = false;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (intoNewScene) {
            intoNewScene = false;
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<UnitTeam>().SetPreviousScene();
            player.transform.position = playerPos;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //If a game object of player enters this Collider2D
    //playerPos in GameStatus will be set to the same as the input cords
    //And the player will load a new scene
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerPos = playerPosInNewScene;
            intoNewScene = true;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
