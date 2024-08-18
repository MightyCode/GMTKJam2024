using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiouPiou : MonoBehaviour
{
    [SerializeField] private float WaitBeforeStart;
    [SerializeField] private float WaitBetweenShots;

    [SerializeField] private float SpeedProjectile;

    [SerializeField] private GameObject container; 

    private Timer timer;

    private GameObject spawnPoint;

    [SerializeField] private GameObject projectilePrefab;

     void Start()
    {
        timer = new Timer();

        if (WaitBeforeStart > 0)
            timer.Start(WaitBeforeStart);
        else
            timer.Start(WaitBetweenShots);
        // Named spawnPoint
        spawnPoint = transform.Find("SpawnPoint").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timer.Update();

        if (timer.Finished)
        {
            // Create the projectile entity from the prefab with an argument witch is attackDamage
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;

            // set container as parent of the projectile
            projectile.transform.parent = container.transform;

            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.Speed = SpeedProjectile;
            projectileScript.Owner = gameObject;

            timer.Start(WaitBetweenShots);
        }
    }
}
