using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    public PlayerInventory playerInventory;

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

    private int selectedPetIndex = 0;
    private GameObject selectedPet;
    private Button selectedAbilityButton;
    // private bool isPlayerHoveringButton = false;
    public Ability dummyAbility;

    public Button selectPetButton0, selectPetButton1, selectPetButton2, selectPetButton3, selectPetButton4, selectPetButton5;
    public Button selectPetButton6, selectPetButton7, selectPetButton8, selectPetButton9, selectPetButton10, selectPetButton11;

    public GameObject pet0;
    public GameObject pet1;
    public GameObject pet2;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory.AddPetToList(pet0);
        playerInventory.AddPetToList(pet1);
        playerInventory.AddPetToList(pet2);
    }

    // Update is called once per frame
    void Update()
    {
        selectedPet = playerInventory.GetPetInList(selectedPetIndex);
        UpdateUI(selectedPet);
    }

    //private Unit GetCurrentlySelectedUnit()
    //{
    //    Unit unit = playerInventory.petList[1].GetComponent<Unit>();
    //    return unit;
    //}

    private void UpdateUI(GameObject unitGO)
    {
        Unit unit = unitGO.GetComponent<Unit>();
        unit.CalculateStats();
        UpdateStats(unit);
        UpdatePortrait(unit);
        UpdateAbilityButtons(unit);
        UpdatePetSelectButtonImages();
        EnablePetSelectButtons(playerInventory.petList.Count);
        Ability selectedAbility = GetUnitAbility(selectedPet.GetComponent<Unit>(), selectedAbilityButton);
        if (selectedAbility != dummyAbility)
        {
            UpdateTooltip(selectedAbility, selectedPet.GetComponent<Unit>());
        }
    }

    private void UpdateStats(Unit unit)
    {
        unitName.text = unit.unitName;
        statHealth.text = "Health: " + unit.maxHP.ToString();
        statMana.text = "Mana: " + unit.maxMP.ToString();
        statAttack.text = "Attack: " + unit.attack.ToString();
        statDefence.text = "Defence: " + unit.defence.ToString();
    }

    private void UpdatePortrait(Unit unit)
    {
        portraitPicture.sprite = unit.GetPortraitPicture();
    }

    private void UpdateAbilityButtons(Unit unit)
    {
        abilityButton1.GetComponentInChildren<Text>().text = unit.GetAbilityName("ability1");
        abilityButton2.GetComponentInChildren<Text>().text = unit.GetAbilityName("ability2");
        abilityButton3.GetComponentInChildren<Text>().text = unit.GetAbilityName("ability3");
        abilityButton4.GetComponentInChildren<Text>().text = unit.GetAbilityName("ability4");
    }

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
    /// <returns></returns>
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

    private void EnablePetSelectButtons(int buttonToEnable) {
        //List<Button> selectPetButtonList = new List<Button>();
        //selectPetButtonList = GameObject.FindGameObjectsWithTag("SelectButtonList");
        //selectPetButtonList.Add(GameObject.FindGameObjectsWithTag("SelectUnitButton"));
        //for (int i = 0; i < 11 ; i++)
        //{
            //GameObject buttonGO = GameObject.FindGameObjectsWithTag("SelectUnitButton");
            //selectPetButtonList.Add(buttonGO.GetComponent<Button>());
        //}

    }

    private void UpdatePetSelectButtonImages()
    {
        selectPetButton0.transform.Find("Image").GetComponent<Image>().sprite = playerInventory.petList[0].GetComponent<Unit>().portraitPicture;
        selectPetButton1.transform.Find("Image").GetComponent<Image>().sprite = playerInventory.petList[1].GetComponent<Unit>().portraitPicture;
        selectPetButton1.transform.Find("Image").GetComponent<Image>().sprite = playerInventory.petList[1].GetComponent<Unit>().portraitPicture;
    }

    public void SelectedPetIndex(int selectedPetIndex)
    {
        this.selectedPetIndex = selectedPetIndex;
    }

    public void SetSelectedAbilityButton(Button button)
    {
        selectedAbilityButton = button;
    }

    // TODO
    // Buttons need to be enabled depending on how many pets you have, and disable the rest
    // Images need to match the correct pet in the correct slot. --> Find a solution for checking how many pets/buttons you are going to change

}
