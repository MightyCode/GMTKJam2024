using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootOnDeath : MonoBehaviour
{
    [SerializeField] public List<GameObject> LootTable;

    [SerializeField] public int value;

    public float SpawnRadius = 6f; 

    private void OnDestroy()
    {

        foreach(GameObject obj in LootTable)
        {
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * SpawnRadius;
            randomPosition.y = 0.4f;

            GameObject spawned = Instantiate(obj,randomPosition,Quaternion.identity);

            Collectible collectible = spawned.GetComponent<Collectible>();
            collectible.value = value;
        }
    }
}
