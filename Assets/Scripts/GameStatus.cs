using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour {

    public static Vector3 playerPos;
    private static bool intoNewScene = false;
    public GameObject player;
    private float playerPosX;
    private float playerPosY;
    private float playerPosZ;
    // Start is called before the first frame update
    void Start()
    {
        if (intoNewScene) {
            intoNewScene = false;
            playerPos.x = PlayerPrefs.GetFloat("playerPosX");
            playerPos.y = PlayerPrefs.GetFloat("playerPosY");
            playerPos.z = PlayerPrefs.GetFloat("playerPosZ");
            player.transform.position = playerPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy() {
        savePlayerPos();
    }

    public void setPlayerPos(Vector3 position) {
        playerPos = position;
    }

    private void savePlayerPos() {
        playerPosX = playerPos.x;
        playerPosY = playerPos.y;
        playerPosZ = playerPos.z;

        PlayerPrefs.SetFloat("playerPosX", playerPosX);
        PlayerPrefs.SetFloat("playerPosY", playerPosY);
        PlayerPrefs.SetFloat("playerPosZ", playerPosZ);

        intoNewScene = true;
    }
}
