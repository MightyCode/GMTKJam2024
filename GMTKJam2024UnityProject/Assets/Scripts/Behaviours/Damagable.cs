using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{

    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float invicibilityFrame = 1.0f;

    public float currentHealth;

    private bool canBeHit = true;
    private void Awake()
    {
        currentHealth = maxHealth;
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
        if(currentHealth + amount <= maxHealth)
        {
            currentHealth += amount;
        }
    }

    private IEnumerator TakeDamage(float damage)
    {
        canBeHit = false;
        currentHealth -= damage;
        Debug.Log(this.gameObject.name + "remaning health is : " + currentHealth);
        if (currentHealth <= 0 )
        {
            Destroy(gameObject);
        }

        yield return new WaitForSeconds(invicibilityFrame);
        canBeHit = true;
    }

}
