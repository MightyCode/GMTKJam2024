using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{

    [SerializeField] private Damagable Target;

    [SerializeField] private List<Heart> HealthList;
    private int currentLifeIndex = -1;

    [SerializeField] private Heart heartToSpawn;

    private void Start()
    {
        for (int i = 0; i < Target.currentHealth; i++) {
            Heart newHeart = Instantiate(heartToSpawn,this.transform);
            HealthList.Add(newHeart);
            currentLifeIndex++;
        }
    }

    public void RemoveHeart()
    {
        if(currentLifeIndex >= 0)
        {
            Heart currentHeart = HealthList[currentLifeIndex];
            currentHeart.ChangeToEmptyHeart();
            currentLifeIndex--;
            Debug.Log("currentLifeIndex " + currentLifeIndex);
        }
    }

    public void AddHeart()
    {
        if(currentLifeIndex < HealthList.Count)
        {
            currentLifeIndex++;
            Heart currentHeart = HealthList[currentLifeIndex];
            currentHeart.ChangeToFullHeart();
            Debug.Log("currentLifeIndex " + currentLifeIndex);
        }

    }
}
