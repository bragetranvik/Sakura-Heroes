using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShopInventory : MonoBehaviour
{
    public List<GameObject> petShopList = new List<GameObject>();
    public GameObject petInShop0, petInShop1, petInShop2, petInShop3, petInShop4, petInShop5, petInShop6;


    // Start is called before the first frame update
    void Start()
    {
        petShopList.Add(petInShop0);
        petShopList.Add(petInShop1);
        petShopList.Add(petInShop2);
        petShopList.Add(petInShop3);
        petShopList.Add(petInShop4);
        petShopList.Add(petInShop5);
        petShopList.Add(petInShop6);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public GameObject GetPetInShopList(int index)
    {
        GameObject petInIndex = petShopList[index];
        return petInIndex;
    }
}
