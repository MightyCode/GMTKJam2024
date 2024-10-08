using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataList
{
    /* List upgrade */
    /**
     * Time Limit 1 : Time limit * 3
     * Time Limit 2 : Time limit * 3
     * Time Limit 3 : Time limit * 2
     * Time Limit 4 : Time limit * 1.5
     * 
     * Speed 1 : player speed * 1.5
     * Speed 2 : player speed * 2
     * 
     * Attack : Unlock the attack
     * Dash : Unlock the dash
     * 
     * Attack Damage 1 : Attack damage * 1.5
     * Attack Damage 2 : Attack damage * 2
     * 
     * Attack Speed 1 : Attack speed * 1.5
     * 
     * Dash speed 1 : Dash speed * 1.5
     * 
     */

    public static int UNAVAILABLE_ITEM = -1;

    public class LevelData
    {
        public int level;
        public int initialTimeLimit;
        public List<ShopItem> shopItems;
        public int goalData;

        public LevelData()
        {
            shopItems = new List<ShopItem>();
        }
    }

    public class ShopItem
    {
        public string name;
        public int price;
        public bool isPurchased;
    }

    // Describe the shop items for each level
    public static LevelData GetLevelData(int level)
    {
        LevelData levelData = new LevelData();
        List<ShopItem> shopItems = new List<ShopItem>();

        levelData.shopItems = shopItems;
        levelData.level = level;

        switch (level) {
            case 1:
                shopItems.Add(new ShopItem { name = "Time Limit 1", price = 10, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Time Limit 2", price = 100, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Time Limit 3", price = 500, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Time Limit 4", price = UNAVAILABLE_ITEM, isPurchased = false });

                shopItems.Add(new ShopItem { name = "Speed 1", price = 50, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Speed 2", price = UNAVAILABLE_ITEM, isPurchased = false });

                shopItems.Add(new ShopItem { name = "Attack", price = 200, isPurchased = false });

                shopItems.Add(new ShopItem { name = "Attack Damage 1", price = UNAVAILABLE_ITEM, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Attack Damage 2", price = UNAVAILABLE_ITEM, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Attack Speed 1", price = UNAVAILABLE_ITEM, isPurchased = false });

                shopItems.Add(new ShopItem { name = "Dash", price = 0, isPurchased = true });

                shopItems.Add(new ShopItem { name = "Dash Speed 1", price = UNAVAILABLE_ITEM, isPurchased = false });

                levelData.initialTimeLimit = 15;
                levelData.goalData = 585;

                break;
            case 2:
                shopItems.Add(new ShopItem { name = "Time Limit 1", price = 5, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Time Limit 2", price = 100, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Time Limit 3", price = 500, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Time Limit 4", price = UNAVAILABLE_ITEM, isPurchased = false });

                shopItems.Add(new ShopItem { name = "Speed 1", price = 100, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Speed 2", price = UNAVAILABLE_ITEM, isPurchased = false });

                shopItems.Add(new ShopItem { name = "Attack", price = 20, isPurchased = false });

                shopItems.Add(new ShopItem { name = "Attack Damage 1", price = UNAVAILABLE_ITEM, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Attack Damage 2", price = UNAVAILABLE_ITEM, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Attack Speed 1", price = UNAVAILABLE_ITEM, isPurchased = false });

                shopItems.Add(new ShopItem { name = "Dash", price = 0, isPurchased = true });

                shopItems.Add(new ShopItem { name = "Dash Speed 1", price = UNAVAILABLE_ITEM, isPurchased = false });

                levelData.initialTimeLimit = 25;
                levelData.goalData = 505;

                break;
            case 3:
                shopItems.Add(new ShopItem { name = "Time Limit 1", price = 5, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Time Limit 2", price = 20, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Time Limit 3", price = 500, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Time Limit 4", price = UNAVAILABLE_ITEM, isPurchased = false });

                shopItems.Add(new ShopItem { name = "Speed 1", price = 100, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Speed 2", price = UNAVAILABLE_ITEM, isPurchased = false });

                shopItems.Add(new ShopItem { name = "Attack", price = UNAVAILABLE_ITEM, isPurchased = false });

                shopItems.Add(new ShopItem { name = "Attack Damage 1", price = UNAVAILABLE_ITEM, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Attack Damage 2", price = UNAVAILABLE_ITEM, isPurchased = false });
                shopItems.Add(new ShopItem { name = "Attack Speed 1", price = UNAVAILABLE_ITEM, isPurchased = false });

                shopItems.Add(new ShopItem { name = "Dash", price = 20, isPurchased = false });

                shopItems.Add(new ShopItem { name = "Dash Speed 1", price = UNAVAILABLE_ITEM, isPurchased = false });

                levelData.initialTimeLimit = 10;
                levelData.goalData = 522;
                break;
            default:
                break;
        }

        return levelData;
    }

    public static int GetScore(int level, int trip)
    {
        if (level == 1)
        {
            if (trip <= 3)
                return 3;
            else if (trip <= 5)
                return 2;
            else
                return 1;
        }

        if (level == 2)
        {
            if (trip <= 1)
                return 3;
            else if (trip <= 3)
                return 2;
            else
                return 1;
        }

        return 0;
    }
}
