using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    public GameObject InventoryUIControllerGO;
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

    public Ability dummyAbility;

    public Button battlePetSlot0, battlePetSlot1, battlePetSlot2;

    public Button selectPetButton0, selectPetButton1, selectPetButton2, selectPetButton3, selectPetButton4, selectPetButton5;
    public Button selectPetButton6, selectPetButton7, selectPetButton8, selectPetButton9, selectPetButton10, selectPetButton11;
    public List<Button> selectPetButtonList;

    // Start is called before the first frame update
    void Start()
    {
        AddButtonsToList();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        // Purely for testing purposes, and should be removed when the player is assigned a team normally!!!
        if (playerInventory.petList.Count < 1) {
            playerInventory.AddPetToList(GameObject.FindGameObjectWithTag("Player").GetComponent<UnitTeam>().unit1);
            playerInventory.AddPetToList(GameObject.FindGameObjectWithTag("Player").GetComponent<UnitTeam>().unit2);
            playerInventory.AddPetToList(GameObject.FindGameObjectWithTag("Player").GetComponent<UnitTeam>().unit3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        selectedPet = playerInventory.GetPetInList(selectedPetIndex);
        UpdateUI(selectedPet);
    }
    // Awake is called when the object is loaded
    private void Awake()
    {
        //playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
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
    private void EnablePetSelectButtons() {
        for (int i = 0; i < playerInventory.petList.Count; i++)
        {
            selectPetButtonList[i].enabled = true;
            selectPetButtonList[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
        }
    }

    /// <summary>
    /// Update the sprites in the select pet buttons
    /// </summary>
    private void UpdatePetSelectButtonImages()
    {
        for(int i = 0; i < playerInventory.petList.Count; i++)
        {
            selectPetButtonList[i].transform.GetChild(0).GetComponent<Image>().sprite = playerInventory.petList[i].GetComponent<Unit>().portraitPicture;
        }
    }

    /// <summary>
    /// Update the sprite in the currently chosen pets for battle buttons
    /// </summary>
    private void UpdateBattlePetSlots()
    {
        battlePetSlot0.transform.GetChild(0).GetComponent<Image>().sprite = playerInventory.battlePetList[0].GetComponent<Unit>().portraitPicture;
        battlePetSlot1.transform.GetChild(0).GetComponent<Image>().sprite = playerInventory.battlePetList[1].GetComponent<Unit>().portraitPicture;
        battlePetSlot2.transform.GetChild(0).GetComponent<Image>().sprite = playerInventory.battlePetList[2].GetComponent<Unit>().portraitPicture;
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
    /// Used to add the select pet buttons into a 
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
}
