using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private static ArrayList DestructedCollectibles;

    public static float MinValue = 1f; //-> * 1 scale
    public static float MaxValue = 100240; //-> * 3 scale

    public static float MaxCollectibleScale = 3.0f;

    public AudioClip collectibleSound;

    public ParticleSystem CollectibleParticule;
    private GameObject player;


    [SerializeField]
    private float distanceDetect = 1.0f;

    public float value = 1.0f;

    private Vector3 initialPosition;

    private float scale = 1.0f;

    private FloatTweening floatTweening;

    private float baseRotation;

    public bool BeenSpawn;

    private void Awake()
    {
        if (DestructedCollectibles == null)
        {
            DestructedCollectibles = new ArrayList();
        }
    }

    void Start()
    {
        player = PlayerManager.Instance.gameObject;

        initialPosition = transform.position;

        scale = 1 + Mathf.Log(value, 100);

        transform.localScale = new Vector3(scale, scale, scale);

        floatTweening = new FloatTweening();
        floatTweening.SetTweeningOption(ETweeningOption.Loop);
        floatTweening.SetTweeningValues(ETweeningType.Linear, ETweeningBehaviour.In);

        floatTweening.InitTwoValue(8, 0, 360);

        baseRotation = Random.Range(0, 360);
        transform.Rotate(0, baseRotation, 0);
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
            AudioPlayer.audioPlayer.PlayAudioWithRandomPitch(collectibleSound);
            player.GetComponentInChildren<PlayerManager>().AddResource(value);
            ParticleSystem particule = Instantiate(CollectibleParticule, transform.position, CollectibleParticule.transform.rotation, other.transform);
            particule.Play();
            Destroy(particule, 2f);

            if (BeenSpawn)
                Destroy(gameObject);
            else
            {
                gameObject.SetActive(false);
                DestructedCollectibles.Add(gameObject);
            }
        }
    }

    public void ResetState()
    {
        transform.position = GetComponent<Collectible>().initialPosition;
    }

    public static void ResetAll()
    {
        foreach (GameObject collectible in DestructedCollectibles)
        {
            collectible.SetActive(true);
            collectible.GetComponent<Collectible>().ResetState();
        }

        DestructedCollectibles.Clear();
    }

    public static void Empty()
    {
        DestructedCollectibles.Clear();
    }
}
