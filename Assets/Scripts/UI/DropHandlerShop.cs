using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandlerShop : MonoBehaviour, IDropHandler
{
    public GameObject thisGameObject;
    private new Camera camera;
    private PlayerInventory playerInventory;
    public PlayerShopInventory playerShopInventory;
    public PlayerShopUIController playerShopUIController;

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform PetSlot = transform as RectTransform;
        Vector3 mousePosInWorld = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosInWorld.z = 0;

        if (RectTransformUtility.RectangleContainsScreenPoint(PetSlot, mousePosInWorld))
        {
            if (eventData.pointerDrag != null)
            {
                string nameOfDraggedPet = eventData.pointerDrag.name;
                int battleSlot = findCorrectSlot(thisGameObject.name);
                switch (nameOfDraggedPet)
                {
                    case "Pet0":
                        playerShopUIController.chosenBattlePet = playerShopInventory.petShopList[0];
                        break;
                    case "Pet1":
                        playerShopUIController.chosenBattlePet = playerShopInventory.petShopList[1];
                        break;
                    case "Pet2":
                        playerShopUIController.chosenBattlePet = playerShopInventory.petShopList[2];
                        break;
                    case "Pet3":
                        playerShopUIController.chosenBattlePet = playerShopInventory.petShopList[3];
                        break;
                    case "Pet4":
                        playerShopUIController.chosenBattlePet = playerShopInventory.petShopList[4];
                        break;
                    case "Pet5":
                        playerShopUIController.chosenBattlePet = playerShopInventory.petShopList[5];
                        break;
                    case "Pet6":
                        playerShopUIController.chosenBattlePet = playerShopInventory.petShopList[6];
                        break;
                    case "Pet7":
                        playerShopUIController.chosenBattlePet = playerShopInventory.petShopList[7];
                        break;
                    case "Pet8":
                        playerShopUIController.chosenBattlePet = playerShopInventory.petShopList[8];
                        break;
                    case "Pet9":
                        playerShopUIController.chosenBattlePet = playerShopInventory.petShopList[9];
                        break;
                    case "Pet10":
                        playerShopUIController.chosenBattlePet = playerShopInventory.petShopList[10];
                        break;
                    case "Pet11":
                        playerShopUIController.chosenBattlePet = playerShopInventory.petShopList[11];
                        break;
                }
            }
        }

    }
    void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    private int findCorrectSlot(string slotString)
    {
        int returnValue = -1;
        if (slotString == "BattleSlot0")
        {
            returnValue = 0;
        }
        else if (slotString == "BattleSlot1")
        {
            returnValue = 1;
        }
        else if (slotString == "BattleSlot2")
        {
            returnValue = 2;
        }
        return returnValue;
    }
}