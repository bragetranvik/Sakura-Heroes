using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShopInventory : MonoBehaviour
{
    public List<GameObject> petShopList = new List<GameObject>();

    public GameObject GetPetInShopList(int index)
    {
        GameObject petInIndex = petShopList[index];
        return petInIndex;
    }
}
