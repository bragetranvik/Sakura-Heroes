using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShopUIController : MonoBehaviour
{
    public GameObject InventoryUIControllerGO;
    public PlayerShopInventory playerShopInventory;

    public Image portraitPicture;

    public Text unitName;
    public Text statHealth;
    public Text statMana;
    public Text statAttack;
    public Text statDefence;

    public Text tooltipText;
    public Button abilityButton1;
    public Button abilityButton2;
    public Button abilityButton3;
    public Button abilityButton4;

    private PlayerInventory playerInventory;
    private int selectedPetIndex = 0;
    private GameObject selectedPet;
    private Button selectedAbilityButton;
    public GameObject chosenBattlePet;
    public Text petCostText;
    public Text feedbackText;
    public Text feedbackTextBorder;

    public Ability dummyAbility;

    public Button buySlot0;
    public Button buyButton;

    public Button selectPetButton0, selectPetButton1, selectPetButton2, selectPetButton3, selectPetButton4, selectPetButton5;
    public Button selectPetButton6, selectPetButton7, selectPetButton8, selectPetButton9, selectPetButton10, selectPetButton11;
    public List<Button> selectPetButtonList;
    
    // Start is called before the first frame update
    void Start()
    {
        AddButtonsToList();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        // Purely for testing purposes, and should be removed when the player is assigned a team normally!!!
        if (playerInventory.petList.Count < 1)
        {
            playerInventory.AddPetToList(GameObject.FindGameObjectWithTag("Player").GetComponent<UnitTeam>().unit1);
            playerInventory.AddPetToList(GameObject.FindGameObjectWithTag("Player").GetComponent<UnitTeam>().unit2);
            playerInventory.AddPetToList(GameObject.FindGameObjectWithTag("Player").GetComponent<UnitTeam>().unit3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        selectedPet = playerShopInventory.GetPetInShopList(selectedPetIndex);
        UpdateUI(selectedPet);
    }
    // Awake is called when the object is loaded
    private void Awake()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    /// <summary>
    /// Update all the different UI components
    /// </summary>
    /// <param name="unitGO">The unit GameObject that has been selected by the player</param>
    private void UpdateUI(GameObject unitGO)
    {
        Unit unit = unitGO.GetComponent<Unit>();
        unit.CalculateStats();
        UpdateStats(unit);
        UpdatePortrait(unit);
        UpdateAbilityButtons(unit);
        UpdateBattlePetSlots();
        UpdatePetSelectButtonImages();
        EnablePetSelectButtons();
        Ability selectedAbility = GetUnitAbility(unit, selectedAbilityButton);
        if (selectedAbility != dummyAbility)
        {
            UpdateTooltip(selectedAbility, unit);
        }
        UpdatePetCost(chosenBattlePet.GetComponent<Unit>());
    }

    /// <summary>
    /// Update the stats on the right side panel to display the currently selected units stats
    /// </summary>
    /// <param name="unit">The unit whose stats you want to display</param>
    private void UpdateStats(Unit unit)
    {
        unitName.text = unit.unitName;
        statHealth.text = "Health: " + unit.maxHP.ToString();
        statMana.text = "Mana: " + unit.maxMP.ToString();
        statAttack.text = "Attack: " + unit.attack.ToString();
        statDefence.text = "Defence: " + unit.defence.ToString();
    }

    /// <summary>
    /// Update the portrait on the right side panel to display the portrait of the currently selected unit
    /// </summary>
    /// <param name="unit">The units whose portrait you want to display</param>
    private void UpdatePortrait(Unit unit)
    {
        portraitPicture.sprite = unit.GetPortraitPicture();
    }

    /// <summary>
    /// Update the ability buttons to display the right abilities for the currently selected unit
    /// </summary>
    /// <param name="unit">The unit you whoses abilities you want to displayr</param>
    private void UpdateAbilityButtons(Unit unit)
    {
        abilityButton1.GetComponentInChildren<Text>().text = unit.GetAbilityName("ability1");
        abilityButton2.GetComponentInChildren<Text>().text = unit.GetAbilityName("ability2");
        abilityButton3.GetComponentInChildren<Text>().text = unit.GetAbilityName("ability3");
        abilityButton4.GetComponentInChildren<Text>().text = unit.GetAbilityName("ability4");
    }

    /// <summary>
    /// Update the tooltip depending on what ability the player selected
    /// </summary>
    /// <param name="ability">The currently selected ability</param>
    /// <param name="unit">The unit that is currently selected</param>
    public void UpdateTooltip(Ability ability, Unit unit)
    {
        //Debug.Log(ability.abilityName + ": " + ability.abilityTooltip);
        tooltipText.text = ability.abilityName + ": " + ability.abilityTooltip + "\n" +
            "Damage: " + ability.GetAbilityDamage(unit) + ", Mana cost: " + ability.GetManaCost();
    }

    /// <summary>
    /// Gets the ability of a given unit depending on the button that is selected by the player
    /// </summary>
    /// <param name="unit">The unit who is selected</param>
    /// <param name="selectedButton">The button the player has selected</param>
    /// <returns name="currentUnitAbility">The currently selected ability</returns>
    public Ability GetUnitAbility(Unit unit, Button selectedButton)
    {
        Ability currentUnitAbility = dummyAbility;
        if ((selectedButton == abilityButton1) && (unit.ability1 != null))
        {
            currentUnitAbility = unit.ability1;
        }
        else if ((selectedButton == abilityButton2) && (unit.ability2 != null))
        {
            currentUnitAbility = unit.ability2;
        }
        else if ((selectedButton == abilityButton3) && (unit.ability3 != null))
        {
            currentUnitAbility = unit.ability3;
        }
        else if ((selectedButton == abilityButton4) && (unit.ability4 != null))
        {
            currentUnitAbility = unit.ability4;
        }
        return currentUnitAbility;
    }

    /// <summary>
    /// Enable the different select pet buttons depending on how many pets the player has.
    /// </summary>
    private void EnablePetSelectButtons()
    {
        for (int i = 11; i >= playerShopInventory.petShopList.Count; i--)
        {
            selectPetButtonList[i].enabled = false;
            selectPetButtonList[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
    }

    /// <summary>
    /// Update the sprites in the select pet buttons
    /// </summary>
    private void UpdatePetSelectButtonImages()
    {
        for (int i = 0; i < playerShopInventory.petShopList.Count; i++)
        {
            selectPetButtonList[i].transform.GetChild(0).GetComponent<Image>().sprite = playerShopInventory.petShopList[i].GetComponent<Unit>().portraitPicture;
        }
    }

    /// <summary>
    /// Update the sprite in the currently chosen pets for battle buttons
    /// </summary>
    private void UpdateBattlePetSlots()
    {
        buySlot0.transform.GetChild(0).GetComponent<Image>().sprite = chosenBattlePet.GetComponent<Unit>().portraitPicture;
    }

    private void UpdatePetCost(Unit unit)
    {
        petCostText.text = "Costs " + unit.unitPriceInShop + " gold";
    }

    public void SelectedPetIndex(int selectedPetIndex)
    {
        this.selectedPetIndex = selectedPetIndex;
    }

    public void SetSelectedAbilityButton(Button button)
    {
        selectedAbilityButton = button;
    }

    /// <summary>
    /// Used to add the select pet buttons into a list
    /// </summary>
    public void AddButtonsToList()
    {
        selectPetButtonList = new List<Button>()
        {
            selectPetButton0,
            selectPetButton1,
            selectPetButton2,
            selectPetButton3,
            selectPetButton4,
            selectPetButton5,
            selectPetButton6,
            selectPetButton7,
            selectPetButton8,
            selectPetButton9,
            selectPetButton10,
            selectPetButton11
        };
    }

    public void PlayerBoughtPet()
    {
        string feedbackText = "empty";
        bool doesNotHavePet = true;
        foreach (GameObject petInInventory in playerInventory.petList)
        {
            if (petInInventory == chosenBattlePet)
            {
                doesNotHavePet = false;
            }
        }

        if (playerInventory.totalMoney < chosenBattlePet.GetComponent<Unit>().unitPriceInShop)
        {
            feedbackText = "Not enough gold!";
        } else if (playerInventory.petList.Count >= 12)
        {
            feedbackText = "You have too many pets!";
        } else if (!doesNotHavePet)
        {
            feedbackText = "You already have that pet!";
        } else
        {
            feedbackText = chosenBattlePet.name + " purchased!";
            playerInventory.petList.Add(chosenBattlePet);
            playerInventory.totalMoney -= chosenBattlePet.GetComponent<Unit>().unitPriceInShop;
            playerShopInventory.petShopList.Remove(chosenBattlePet);
            chosenBattlePet = playerShopInventory.petShopList[0];
        }
        StartCoroutine(playFeedbackText(feedbackText));
    }

    private IEnumerator playFeedbackText(string stringToPlay)
    {
        feedbackText.color = new Color(0.1960784f, 0.1960784f, 0.1960784f, 1);
        feedbackText.text = stringToPlay;
        feedbackTextBorder.color = new Color(1f, 1f, 1f, 1f);
        feedbackTextBorder.text = stringToPlay;

        for (float i = 1; i >= 0; i -= 0.01f)
        {
            Color newColorForText = new Color(0.1960784f, 0.1960784f, 0.1960784f, i);
            feedbackText.color = newColorForText;
            Color newColorForBorder = new Color(1f, 1f, 1f, i);
            feedbackTextBorder.color = newColorForBorder;
            yield return new WaitForSeconds(0.022f);
        }
    }
}
