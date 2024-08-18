using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public static float MinValue = 1f; //-> * 1 scale
    public static float MaxValue = 100240; //-> * 3 scale

    public static float MaxCollectibleScale = 3.0f;

    public ParticleSystem CollectibleParticule;

    private static ArrayList disabledCollectibles;
    private GameObject player;


    [SerializeField]
    private float distanceDetect = 1.0f;

    public float value = 1.0f;

    private Vector3 initialPosition;

    private float scale = 1.0f;

    private FloatTweening floatTweening;

    private float baseRotation;

    private void Awake()
    {
        if (disabledCollectibles == null)
        {
            disabledCollectibles = new ArrayList();
        }

        baseRotation = Random.Range(0, 360);
        transform.Rotate(0, baseRotation, 0);
    }

    void Start()
    {
        player = PlayerManager.Instance.gameObject;

        disabledCollectibles = new ArrayList();

        initialPosition = transform.position;

        scale = 1 + Mathf.Log(value, 100);

        transform.localScale = new Vector3(scale, scale, scale);

        floatTweening = new FloatTweening();
        floatTweening.SetTweeningOption(ETweeningOption.Loop);
        floatTweening.SetTweeningValues(ETweeningType.Linear, ETweeningBehaviour.In);

        floatTweening.InitTwoValue(8, 0, 360);
    }


    // Un peu d'émentage au joueur quand il s'approche

    // Update is called once per frame
    void Update()
    {
        if(player != null) {
            // if player is close enough magnet the collectible, slow magnet speed
            if (Vector3.Distance(player.transform.position, transform.position) < distanceDetect * scale)
            {
                float force = 0.005f;
                transform.position = Vector3.Lerp(transform.position, player.transform.position, force);
            }
        }

        floatTweening.Update();

        float newRotation = baseRotation + floatTweening.Value;

        if (newRotation > 180)
            newRotation -= 360;

        transform.rotation = Quaternion.Euler(0, newRotation, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponentInChildren<PlayerManager>().AddResource(value);
            ParticleSystem particule = Instantiate(CollectibleParticule, transform.position, CollectibleParticule.transform.rotation,other.transform);
            particule.Play();
            Destroy(particule, 2f);
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
