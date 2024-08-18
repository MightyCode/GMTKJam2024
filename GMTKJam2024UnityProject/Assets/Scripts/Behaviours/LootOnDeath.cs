using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootOnDeath : MonoBehaviour
{
    public Damagable MonsterToDrop;

    [SerializeField] public List<GameObject> LootTable;

    public float SpawnRadius = 6f; 

    private void OnDestroy()
    {
        if(MonsterToDrop.currentHealth <= 0)
        {
            foreach (GameObject obj in LootTable)
            {
                Vector3 randomPosition = transform.position + Random.insideUnitSphere * SpawnRadius;
                randomPosition.y = 0.4f;

                GameObject spawned = Instantiate(obj, randomPosition, Quaternion.identity);
            }
        }
        
    }
}
