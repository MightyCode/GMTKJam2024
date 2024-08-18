using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingElement : MonoBehaviour
{

    [SerializeField] protected float attackDamage = 1f;
    [SerializeField] private float attackDuration = 1f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private GameObject owner;

    [SerializeField] private Collider DamagingCollider;
    [SerializeField] private MeshRenderer DamagingRenderer;

    private bool canAttack = true;

    public void Attack()
    {
        if(canAttack)
        {
            StartCoroutine(AttackWithWeapon());
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide with" + other);
        Damagable damagable = other.gameObject.GetComponent<Damagable>();
        if (damagable != null)
        {
            if(damagable.gameObject != owner)
            {
                damagable.GetHit(attackDamage);
            }
        }
    }
 
    private IEnumerator AttackWithWeapon()
    {
        canAttack = false;
        DamagingCollider.enabled = true;
        DamagingRenderer.enabled = true;
        Debug.Log("Attack begin");
        yield return new WaitForSeconds(attackDuration);
        Debug.Log("Attack is done");
        DamagingCollider.enabled = false;
        DamagingRenderer.enabled = false;


        yield return new WaitForSeconds(attackCooldown);
        Debug.Log("Attack Cooldown is done");
        canAttack = true;

    }
}
