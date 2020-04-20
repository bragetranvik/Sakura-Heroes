using Fungus;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class MainMenu : MonoBehaviour
{

    public GameObject playerGO;
    public GameObject playerUnit;

    public Sprite characterSprite1;
    public Sprite characterSprite2;
    public Sprite characterSprite3;
    public Sprite characterSprite4;

    public string state;

    public GameObject mainStateGO;
    public GameObject newGameSelectCharGO;
    public GameObject newGameSelectPetGO;
    public GameObject loadGameGO;
    public GameObject optionsGO;
    public GameObject quitGameGO;

    private bool hasChangedStates = false;

    void Start()
    {
        playerGO.GetComponent<SpriteRenderer>().enabled = false;
        playerGO.GetComponent<Transform>().position = new Vector3(9f, 9f, 0f);
        StartCoroutine(MenuSwitchCase());
    }

    void Update()
    {
        
    }

    private IEnumerator MenuSwitchCase()
    {
        bool stillInMenu = true;
        while (stillInMenu) {
            if (hasChangedStates)
            {
                mainStateGO.SetActive(false);
                newGameSelectCharGO.SetActive(false);
                newGameSelectPetGO.SetActive(false);
                optionsGO.SetActive(false);
                loadGameGO.SetActive(false);
                quitGameGO.SetActive(false);
            }
            switch (state)
            {
                // The main state of the menu
                case "MAIN":
                    mainStateGO.SetActive(true);
                    break;
                case "NEW_GAME_SELECT_CHAR":
                    newGameSelectCharGO.SetActive(true);
                    break;
                case "NEW_GAME_SELECT_PET":
                    newGameSelectPetGO.SetActive(true);
                    // If the player has 3 objects in the player inventory, go to startHub
                    if (playerGO.GetComponent<PlayerInventory>().petList.Count >= 3 && stillInMenu)
                    {
                        stillInMenu = false;
                        SetBattlePetToChosenPets();
                        yield return new WaitForSeconds(1f);
                        NewGame();
                    }
                    break;
                case "OPTIONS":
                    optionsGO.SetActive(true);
                    break;
                case "LOAD_GAME":
                    loadGameGO.SetActive(true);
                    break;
                case "QUIT_GAME":
                    quitGameGO.SetActive(true);
                    break;
                default:
                    state = "MAIN";
                    break;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    
    public void NewGame()
    {
        SceneManager.LoadScene("StartHub");
        playerGO.GetComponent<SpriteRenderer>().enabled = true;
        playerGO.GetComponent<Transform>().position = new Vector3(4.5f, -0.3f, 0f);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    public void ChangeState(string stateToChangeTo)
    {
        state = stateToChangeTo;
        hasChangedStates = true;
    }

    public void BackButtonClicked()
    {
        if (state == "MAIN")
        {
            // Do nothing
        }
        else if (state == "NEW_GAME_SELECT_CHAR")
        {
            ChangeState("MAIN");
        }
        else if (state == "NEW_GAME_SELECT_PET")
        {
            ChangeState("NEW_GAME_SELECT_CHAR");
        }
        else if (state == "LOAD_GAME")
        {
            ChangeState("MAIN");
        }
        else if (state == "OPTIONS")
        {
            ChangeState("MAIN");
        }
        else if (state == "QUIT_GAME")
        {
            ChangeState("MAIN");
        }
    }
    
    /// <summary>
    /// Used to set the player object the right character sprite and animations
    /// </summary>
    /// <param name="characterID">The ID used to identify what character the player selected</param>
    public void SetCurrentCharacter(int characterID)
    {
        if (characterID == 1)
        {
            playerGO.GetComponent<SpriteRenderer>().sprite = characterSprite1;
        } else if (characterID == 2)
        {
            playerGO.GetComponent<SpriteRenderer>().sprite = characterSprite2;
        }
        else if (characterID == 3)
        {
            playerGO.GetComponent<SpriteRenderer>().sprite = characterSprite3;
        }
        else if (characterID == 4)
        {
            playerGO.GetComponent<SpriteRenderer>().sprite = characterSprite4;
        }
    }

    public void AddPetToInventory(GameObject petToAdd)
    {
        if (playerGO.GetComponent<PlayerInventory>().petList.Count == 0)
        {
            AddPlayerUnitToInventory();
        }
        if (playerGO.GetComponent<PlayerInventory>().petList.Count < 3)
        {
            DontDestroyOnLoad(petToAdd);
            playerGO.GetComponent<PlayerInventory>().AddPetToList(petToAdd);
        }
    }

    private void AddPlayerUnitToInventory()
    {
        DontDestroyOnLoad(playerUnit);
        playerGO.GetComponent<PlayerInventory>().AddPetToList(playerUnit);
    }

    private void SetBattlePetToChosenPets()
    {
        playerGO.GetComponent<PlayerInventory>().battlePetList.Clear();
        playerGO.GetComponent<PlayerInventory>().battlePetList.Add(playerGO.GetComponent<PlayerInventory>().petList.ElementAt(0));
        playerGO.GetComponent<PlayerInventory>().battlePetList.Add(playerGO.GetComponent<PlayerInventory>().petList.ElementAt(1));
        playerGO.GetComponent<PlayerInventory>().battlePetList.Add(playerGO.GetComponent<PlayerInventory>().petList.ElementAt(2));
    }
}
