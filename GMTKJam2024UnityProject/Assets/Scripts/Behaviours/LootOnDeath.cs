using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootOnDeath : MonoBehaviour
{
    [SerializeField] public List<GameObject> LootTable;

    public float SpawnRadius = 6f; 

    private void OnDestroy()
    {

        foreach(GameObject obj in LootTable)
        {
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * SpawnRadius;
            randomPosition.y = 0.4f;

            Instantiate(obj,randomPosition,Quaternion.identity);
        }
    }
}
