using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootOnDeath : MonoBehaviour
{
    [SerializeField] public List<GameObject> LootTable;


    private void OnDestroy()
    {
        Debug.Log("WE ARE DEAD WE MUST LOOT !");
        foreach(GameObject obj in LootTable)
        {
            Instantiate(obj,this.transform,true);
        }
    }
}
