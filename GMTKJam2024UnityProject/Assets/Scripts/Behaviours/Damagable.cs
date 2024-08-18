using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{

    public float maxHealth = 3f;
    public float currentHealth;

    [SerializeField] private float invicibilityFrame = 1.0f;

    [SerializeField] private HealthUI healthUI;



    private bool canBeHit = true;
    private void Awake()
    {
        currentHealth = maxHealth;
        if(healthUI == null)
        {
            healthUI = GetComponentInChildren<HealthUI>();
        }
    }

    public void GetHit(float damage)
    {
        if (canBeHit)
        {
            StartCoroutine(TakeDamage(damage));
        }
    }

    public void GiveHealth(float amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Debug.Log(i);
            if (currentHealth + 1 <= maxHealth)
            {
                currentHealth += 1;
                if (healthUI != null)
                {
                    healthUI.AddHeart();
                }
            }
        }

    }

    private IEnumerator TakeDamage(float damage)
    {
        canBeHit = false;
        currentHealth -= damage;
        if (healthUI != null)
        {
            healthUI.RemoveHeart();
        }
        Debug.Log(this.gameObject.name + "remaning health is : " + currentHealth);
        if (currentHealth <= 0 )
        {
            Destroy(gameObject);
        }

        yield return new WaitForSeconds(invicibilityFrame);
        canBeHit = true;
    }

}
