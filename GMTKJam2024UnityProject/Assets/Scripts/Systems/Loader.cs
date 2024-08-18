using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] private int level;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private TheBeast theBeast;

    [SerializeField] private GameObject shop;


    // Start is called before the first frame update
    void Awake()
    {
        LevelDataList.LevelData levelData = LevelDataList.GetLevelData(level);

        theBeast.WaitingForFeeding = levelData.initialTimeLimit;
        theBeast.FoodGoal = levelData.goalData;

        foreach (var item in levelData.shopItems)
        {
            // Get the associated shop item
            GameObject shopItem = shop.transform.Find(item.name).gameObject;
            Buyable buyable = shopItem.GetComponent<Buyable>();

            buyable.Price = item.price;
            buyable.IsBought = item.isPurchased;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
