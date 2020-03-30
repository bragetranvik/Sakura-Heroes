using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndCloseUI : MonoBehaviour
{
    public GameObject InventoryUIControllerGO;
    private bool uiState = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !uiState)
        {
            OpenUI();
        }
        else if (Input.GetKeyDown(KeyCode.I) && uiState)
        {
            CloseUI();
        }
    }

    void OpenUI()
    {
        InventoryUIControllerGO.SetActive(true);
        uiState = true;
    }

    void CloseUI()
    {
        InventoryUIControllerGO.SetActive(false);
        uiState = false;
    }
}
