using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusHUD : MonoBehaviour
{

    public Text unit1CharNameText, unit2CharNameText, unit3CharNameText;
    public Text unit1LevelText, unit2LevelText, unit3LevelText;
    public Slider unit1HPSlider, unit2HPSlider, unit3HPSlider;
    public Text unit1HPText, unit2HPText, unit3HPText;
    public Slider unit1MPSlider, unit2MPSlider, unit3MPSlider;
    public Text unit1MPText, unit2MPText, unit3MPText;

    public Text unitAbility1Name, unitAbility2Name, unitAbility3Name, unitAbility4Name;

    public void SetHUD(Unit unit1, Unit unit2, Unit unit3)
    {
        unit1CharNameText.text = unit1.unitName;
        unit1LevelText.text = "Lvl " + unit1.unitLevel;
        unit1HPSlider.maxValue = unit1.maxHP;
        unit1HPSlider.value = unit1.currentHP;
        unit1MPSlider.maxValue = unit1.maxMP;
        unit1MPSlider.value = unit1.currentMP;
        unit1HPText.text = "HP: " + unit1.currentHP + "/" + unit1.maxHP;
        unit1MPText.text = "MP: " + unit1.currentMP + "/" + unit1.maxMP;

        unit2CharNameText.text = unit2.unitName;
        unit2LevelText.text = "Lvl " + unit2.unitLevel;
        unit2HPSlider.maxValue = unit2.maxHP;
        unit2HPSlider.value = unit2.currentHP;
        unit2MPSlider.maxValue = unit2.maxMP;
        unit2MPSlider.value = unit2.currentMP;
        unit2HPText.text = "HP: " + unit2.currentHP + "/" + unit2.maxHP;
        unit2MPText.text = "MP: " + unit2.currentMP + "/" + unit2.maxMP;

        unit3CharNameText.text = unit3.unitName;
        unit3LevelText.text = "Lvl " + unit3.unitLevel;
        unit3HPSlider.maxValue = unit3.maxHP;
        unit3HPSlider.value = unit3.currentHP;
        unit3MPSlider.maxValue = unit3.maxMP;
        unit3MPSlider.value = unit3.currentMP;
        unit3HPText.text = "HP: " + unit3.currentHP + "/" + unit3.maxHP;
        unit3MPText.text = "MP: " + unit3.currentMP + "/" + unit3.maxMP;

    }

    public void SetHPandMP(Unit unit1, Unit unit2, Unit unit3)
    {
        unit1HPSlider.value = unit1.currentHP;
        unit2HPSlider.value = unit2.currentHP;
        unit3HPSlider.value = unit3.currentHP;
        unit1HPText.text = "HP: " + unit1.currentHP + "/" + unit1.maxHP;
        unit2HPText.text = "HP: " + unit2.currentHP + "/" + unit2.maxHP;
        unit3HPText.text = "HP: " + unit3.currentHP + "/" + unit3.maxHP;

        unit1MPSlider.value = unit1.currentMP;
        unit2MPSlider.value = unit2.currentMP;
        unit3MPSlider.value = unit3.currentMP;
        unit1MPText.text = "MP: " + unit1.currentMP + "/" + unit1.maxMP;
        unit2MPText.text = "MP: " + unit2.currentMP + "/" + unit2.maxMP;
        unit3MPText.text = "MP: " + unit3.currentMP + "/" + unit3.maxMP;
    }

    /// <summary>
    /// Set the ability names of the unit.
    /// </summary>
    /// <param name="unit">The unit to set the ability names of.</param>
    public void SetAbilityName(Unit unit) {
        unitAbility1Name.text = unit.GetAbilityName("ability1");
        unitAbility2Name.text = unit.GetAbilityName("ability2");
        unitAbility3Name.text = unit.GetAbilityName("ability3");
        unitAbility4Name.text = unit.GetAbilityName("ability4");
    }

    /// <summary>
    /// Sets the color of the HP slider whenever the value of the slider is updated.
    /// </summary>
    /// <param name="hpSlider">The slider that had its value updated, and needs its color updated</param>
    public void SetHPSliderColor(Slider hpSlider) {
        // To get values in rgb, times the number by 255. Last digit is alpha(opacity).
        Color selfGreen = new Color(0, 1, 0, 1);
        Color selfYellow = new Color(1, 0.92f, 0.016f, 1);
        Color selfOrange = new Color(1, 0.55f, 0, 1);
        Color selfRed = new Color(1, 0, 0, 1);
        Color selfDead = new Color(0, 0, 0, 0);

        if (hpSlider.value > (hpSlider.maxValue* 0.75f) && (hpSlider.value <= (hpSlider.maxValue * 1))) {
            ColorBlock colorblock = hpSlider.colors;
            colorblock.disabledColor = selfGreen;
            hpSlider.GetComponent<Slider>().colors = colorblock;
        } else if ((hpSlider.value > (hpSlider.maxValue * 0.5f)) && (hpSlider.value <= (hpSlider.maxValue * 0.75))) {
            ColorBlock colorblock = hpSlider.colors;
            colorblock.disabledColor = selfYellow;
            hpSlider.GetComponent<Slider>().colors = colorblock;
        } else if (hpSlider.value > (hpSlider.maxValue * 0.25f) && (hpSlider.value <= (hpSlider.maxValue * 0.5))) {
            ColorBlock colorblock = hpSlider.colors;
            colorblock.disabledColor = selfOrange;
            hpSlider.GetComponent<Slider>().colors = colorblock;
        } else if (hpSlider.value > (hpSlider.maxValue * 0) && (hpSlider.value <= (hpSlider.maxValue * 0.25))) {
            ColorBlock colorblock = hpSlider.colors;
            colorblock.disabledColor = selfRed;
            hpSlider.GetComponent<Slider>().colors = colorblock;
        } else if (hpSlider.value <= (hpSlider.maxValue * 0)) {
            ColorBlock colorblock = hpSlider.colors;
            colorblock.disabledColor = selfDead;
            hpSlider.GetComponent<Slider>().colors = colorblock;
        }
    }
}

    
