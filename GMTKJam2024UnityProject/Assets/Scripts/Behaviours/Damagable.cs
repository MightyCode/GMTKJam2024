using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{ 
    private static ArrayList disabledDamagable;

    public float maxHealth = 3f;
    public float currentHealth;

    [SerializeField] private float invicibilityFrame = 1.0f;

    [SerializeField] private HealthUI healthUI;

    public ParticleSystem DamageParticule;

    private bool canBeHit = true;

    private void Awake()
    {
        if (disabledDamagable == null)
        {
            disabledDamagable = new ArrayList();
        }

        currentHealth = maxHealth;
    }

    private void Start()
    {
        ResetState();

        if (healthUI == null)
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
        if (healthUI == null)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        }
        else
        {
            for (int i = 0; i < amount; i++)
            {
                if (currentHealth + 1 <= maxHealth)
                {
                    currentHealth += 1;
                    healthUI.AddHeart();
                }
            }
        }
    }

    private IEnumerator TakeDamage(float damage)
    {
        canBeHit = false;

        if(DamageParticule != null)
        {
            DamageParticule.Play();
        }
        currentHealth -= damage;
        if (healthUI != null)
        {
            healthUI.RemoveHeart();
        }
        Debug.Log(this.gameObject.name + "remaning health is : " + currentHealth);
        if (currentHealth <= 0 )
        {
            if (healthUI == null)
            {
                gameObject.SetActive(false);
                disabledDamagable.Add(this.gameObject);
            } else
            {
                Destroy(gameObject);
            }
        }

        yield return new WaitForSeconds(invicibilityFrame);
        canBeHit = true;
    }

    public void ResetState()
    {
        GiveHealth(maxHealth);
        canBeHit = true;
    }

    public static void ResetAll()
    {
        foreach (GameObject damageable in disabledDamagable)
        {
            damageable.SetActive(true);
            Damagable damagable = damageable.GetComponent<Damagable>();
            damagable.ResetState();
        }

        disabledDamagable.Clear();
    }

    public static void Empty()
    {
        disabledDamagable.Clear();
    }
}
