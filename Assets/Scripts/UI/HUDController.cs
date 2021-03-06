﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Fungus;
using System.Linq;

public class HUDController : MonoBehaviour
{
    public GameObject thisHUD;
    public Image HUDCharacterIcon;
    public Text HUDMoneyCounter;
    public Text HUDLevelCounter;

    private PlayerInventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        playerInventory = playerGO.GetComponent<PlayerInventory>();
        HUDCharacterIcon.sprite = playerInventory.characterPortrait;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHUD();   
    }

    private void UpdateHUD()
    {
        // Set money in UI (Not implemented yet)
        HUDMoneyCounter.text = "Gold: " + Convert.ToString(playerInventory.totalMoney);
        // Set level in UI
        HUDLevelCounter.text = "Level: " + Convert.ToString(playerInventory.level);
    }

    public void HideUI()
    {
        thisHUD.GetComponent<Image>().enabled = false;
        HUDCharacterIcon.enabled = false;
        HUDMoneyCounter.enabled = false;
        HUDLevelCounter.enabled = false;
    }

    public void ShowUI()
    {
        thisHUD.GetComponent<Image>().enabled = false;
        HUDCharacterIcon.enabled = false;
        HUDMoneyCounter.enabled = false;
        HUDLevelCounter.enabled = false;
    }
}
