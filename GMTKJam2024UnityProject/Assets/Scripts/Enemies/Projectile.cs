using UnityEngine;

public class Projectile : DamagingElement
{
    public float Speed;
    public GameObject Owner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move forward 
        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    override protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Owner)
            return;

        Damagable damagable = other.gameObject.GetComponent<Damagable>();
        if (damagable == null)
            // Destroy itself
            Destroy(gameObject);

        else {
            damagable.GetHit(base.attackDamage);
        }
    }
}
