using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{

    public GameObject thisGameObject;
    private Camera camera;
    private PlayerInventory playerInventory;

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform PetSlot = transform as RectTransform;
        Vector3 mousePosInWorld = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosInWorld.z = 0;

        if (RectTransformUtility.RectangleContainsScreenPoint(PetSlot, mousePosInWorld))
        {
            Debug.Log("Drop");
            if (eventData.pointerDrag != null)
            {
                string nameOfDraggedPet = eventData.pointerDrag.name;
                switch (nameOfDraggedPet)
                {
                    case "Pet0":
                        Debug.Log("Pet0 dropped on slot " + thisGameObject.name);
                        break;
                    case "Pet1":
                        Debug.Log("Pet1 dropped on slot " + thisGameObject.name);
                        break;
                    case "Pet2":
                        Debug.Log("Pet2 dropped on slot " + thisGameObject.name);
                        break;
                    case "Pet3":
                        Debug.Log("Pet3 dropped on slot " + thisGameObject.name);
                        break;
                    case "Pet4":
                        Debug.Log("Pet4 dropped on slot " + thisGameObject.name);
                        break;
                    case "Pet5":
                        Debug.Log("Pet5 dropped on slot " + thisGameObject.name);
                        break;
                    case "Pet6":
                        Debug.Log("Pet6 dropped on slot " + thisGameObject.name);
                        break;
                    case "Pet7":
                        Debug.Log("Pet7 dropped on slot " + thisGameObject.name);
                        break;
                    case "Pet8":
                        Debug.Log("Pet8 dropped on slot " + thisGameObject.name);
                        break;
                    case "Pet9":
                        Debug.Log("Pet9 dropped on slot " + thisGameObject.name);
                        break;
                    case "Pet10":
                        Debug.Log("Pet10 dropped on slot " + thisGameObject.name);
                        break;
                    case "Pet11":
                        Debug.Log("Pet11 dropped on slot " + thisGameObject.name);
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
