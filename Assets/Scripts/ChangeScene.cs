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

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Player")) {
            GameStatus.playerPos = playerPosInNewScene;
            SceneManager.LoadScene(sceneToLoad);
            //other.transform.position = playerPosition;
            //Set the player cords when entering 28:17
        }
    }


}
