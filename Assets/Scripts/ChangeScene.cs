using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public string sceneToLoad;
    public Vector3 playerPosInNewScene;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //If a game object of player enters this Collider2D
    //playerPos in GameStatus will be set to the same as the input cords
    //And the player will load a new scene
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            GameStatus.playerPos = playerPosInNewScene;
            SceneManager.LoadScene(sceneToLoad);
        }
    }


}
