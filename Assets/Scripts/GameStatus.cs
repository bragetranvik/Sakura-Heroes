using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour {

    public static Vector3 playerPos;
    private static bool intoNewScene = false;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (intoNewScene) {
            intoNewScene = false;
            player.transform.position = playerPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //OnDestroy is called once the scene has been destroyed
    private void OnDestroy() {
        intoNewScene = true;
    }
}
