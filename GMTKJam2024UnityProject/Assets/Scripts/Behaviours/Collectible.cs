using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float scale = 1.0f;

    [SerializeField]
    private float distanceDetect = 1.0f;

    [SerializeField]
    private float value = 1.0f;


    // Un peu d'émentage au joueur quand il s'approche
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        // if player is close enough magnet the collectible

        if (Vector3.Distance(player.transform.position, transform.position) < distanceDetect)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.1f);
        }
    }
}
