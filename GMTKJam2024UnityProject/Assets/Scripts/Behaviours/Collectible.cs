using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float distanceDetect = 1.0f;

    [SerializeField]
    private float value = 1.0f;


    void Start()
    {
     
    }


    // Un peu d'émentage au joueur quand il s'approche

    // Update is called once per frame
    void Update()
    {
        // if player is close enough magnet the collectible, slow magnet speed
        if (Vector3.Distance(player.transform.position, transform.position) < distanceDetect)
        {
            float force = 0.005f;
            transform.position = Vector3.Lerp(transform.position, player.transform.position, force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponentInChildren<PlayerManager>().AddResource(value);
            Destroy(gameObject);
        }
    }
}
