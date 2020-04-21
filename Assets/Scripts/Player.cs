using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform target;

    public GameObject gameCharacter;

    // private Button saveButton;


    //Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Debug.Log("AWAKE");

        /*
        if (saveButton != null)
        {
            Debug.Log("LISTENER");
            saveButton.onClick.AddListener(() => { SavePlayer(); });
        }
        */
    }


    // Update is called once per frame
    void Start()
    {
        gameCharacter = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        /*
        if (saveButton == null)
        {
            Debug.Log("UPDATE");
            saveButton = GameObject.Find("Save Game").GetComponent<Button>();
        }
        */

        if (Input.GetKeyDown("o"))
        {
            SavePlayer();
        }

        if (Input.GetKeyDown("l"))
        {
            LoadPlayer();
        }
    }

    public void SavePlayer()
    {
        Debug.Log("Attempting to run SavePlayer()");

        if (this == null)
        {
            Debug.Log("'gameCharacter' is null");
        }
        else
        {
            Debug.Log("'gameCharacter' is NOT null");
            Debug.Log(gameCharacter.ToString());
            SaveSystem.SavePlayer(gameCharacter);
        }

    }

    public void LoadPlayer()
    {
        Debug.Log("Attempting to run LoadPlayer()");
        PlayerData data = SaveSystem.LoadPlayer();

        if (data == null)
        {
            Debug.Log("'data' is null");
        }
        else
        {
            Debug.Log("'data' is not null");

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];

            Debug.Log("Loading position: " + position);

            gameCharacter.GetComponent<Transform>().position = position;

            gameCharacter.GetComponent<PlayerInventory>().level = data.playerLevel;
        }

    }


}
