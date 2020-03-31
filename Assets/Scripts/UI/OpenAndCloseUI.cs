﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndCloseUI : MonoBehaviour
{
    public GameObject objectToOpenAndClose;
    public KeyCode keyToOpenAndClose;
    private bool uiState = false;
    public bool notOpenAnywhere;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() { 
        if (Input.GetKeyDown(keyToOpenAndClose) && !uiState && !notOpenAnywhere)
        {
            OpenUI();
        }
        else if (Input.GetKeyDown(keyToOpenAndClose) && uiState)
        {
            CloseUI();
        }
    }

    void OpenUI()
    {
        objectToOpenAndClose.SetActive(true);
        uiState = true;
    }

    void CloseUI()
    {
        objectToOpenAndClose.SetActive(false);
        uiState = false;
    }
}
