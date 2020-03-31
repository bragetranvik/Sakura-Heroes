using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandlerInventory : MonoBehaviour, IDropHandler
{

    public GameObject thisGameObject;
    private new Camera camera;
    private PlayerInventory playerInventory;

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
                        playerInventory.battlePetList[battleSlot] = playerInventory.petList[0];
                        break;
                    case "Pet1":
                        playerInventory.battlePetList[battleSlot] = playerInventory.petList[1];
                        break;
                    case "Pet2":
                        playerInventory.battlePetList[battleSlot] = playerInventory.petList[2];
                        break;
                    case "Pet3":
                        playerInventory.battlePetList[battleSlot] = playerInventory.petList[3];
                        break;
                    case "Pet4":
                        playerInventory.battlePetList[battleSlot] = playerInventory.petList[4];
                        break;
                    case "Pet5":
                        playerInventory.battlePetList[battleSlot] = playerInventory.petList[5];
                        break;
                    case "Pet6":
                        playerInventory.battlePetList[battleSlot] = playerInventory.petList[6];
                        break;
                    case "Pet7":
                        playerInventory.battlePetList[battleSlot] = playerInventory.petList[7];
                        break;
                    case "Pet8":
                        playerInventory.battlePetList[battleSlot] = playerInventory.petList[8];
                        break;
                    case "Pet9":
                        playerInventory.battlePetList[battleSlot] = playerInventory.petList[9];
                        break;
                    case "Pet10":
                        playerInventory.battlePetList[battleSlot] = playerInventory.petList[10];
                        break;
                    case "Pet11":
                        playerInventory.battlePetList[battleSlot] = playerInventory.petList[11];
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
        } else if (slotString == "BattleSlot1")
        {
            returnValue = 1;
        } else if (slotString == "BattleSlot2")
        {
            returnValue = 2;
        }
        return returnValue;
    }
}
