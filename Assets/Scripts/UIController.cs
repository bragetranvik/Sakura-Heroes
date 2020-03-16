using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject HUD;
    public Image UICharacterIcon;
    public Text UIMoneyCounter;
    public Text UILevelCounter;

    private PlayerInventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();   
    }

    private void UpdateUI()
    {
        // Set money in UI (Not implemented yet)
        UIMoneyCounter.text = "Money: " + Convert.ToString(37);
        // Set level in UI
        UILevelCounter.text = "Level: " + Convert.ToString(playerInventory.level);
    }
}
