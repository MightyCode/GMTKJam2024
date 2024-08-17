using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public static float MinValue = 1f; //-> * 1 scale
    public static float MaxValue = 100240; //-> * 3 scale

    public static float MaxCollectibleScale = 3.0f;

    private static ArrayList disabledCollectibles;
    private GameObject player;

    [SerializeField]
    private float distanceDetect = 1.0f;

    [SerializeField]
    private float value = 1.0f;

    private Vector3 initialPosition;

    private float scale = 1.0f;

    private void Awake()
    {
        if (disabledCollectibles == null)
        {
            disabledCollectibles = new ArrayList();
        }
    }

    void Start()
    {
        player = PlayerManager.Instance.gameObject;

        disabledCollectibles = new ArrayList();

        initialPosition = transform.position;

        scale = 1 + Mathf.Log(value, 100);

        transform.localScale = new Vector3(scale, scale, scale);
    }


    // Un peu d'émentage au joueur quand il s'approche

    // Update is called once per frame
    void Update()
    {
        // if player is close enough magnet the collectible, slow magnet speed
        if (Vector3.Distance(player.transform.position, transform.position) < distanceDetect * scale)
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
            gameObject.SetActive(false);
            disabledCollectibles.Add(gameObject);
        }
    }

    public static void ResetAll()
    {
        foreach (GameObject collectible in disabledCollectibles)
        {
            collectible.SetActive(true);
            collectible.transform.position = collectible.GetComponent<Collectible>().initialPosition;
        }

        disabledCollectibles.Clear();
    }
}
