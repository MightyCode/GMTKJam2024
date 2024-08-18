using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TheBeast : MonoBehaviour
{
    public static TheBeast Instance;

    [SerializeField] private GameManager manager;
    private PlayerManager player;

    [SerializeField]
    private TMP_Text timerText;

    [SerializeField]
    private TMP_Text counterTripText;

    private Timer feedTimer;

    [SerializeField]
    public float WaitingForFeeding = 20;
    [SerializeField]
    bool IsWaiting = true;

    [SerializeField]
    private TMP_Text foodText;

    public float FoodGoal;

    private float currentFood;

    public float CurrentFood => currentFood;

    [FormerlySerializedAs("collectSuccesfull")]
    [SerializeField]
    private UnityEvent m_onEnter = new UnityEvent();

    private int counterFeeding = 0;

    private Vector3 initialScale;

    private float initalXRef;

    public void SetFood(float food)
    {
        currentFood = food;

        foodText.text = currentFood+ "/" + FoodGoal;

        float scale = Mathf.Log(player.BaseLog + currentFood, player.BaseLog);
        scale = Mathf.Min(scale, player.MaxScale);

        transform.localScale = new Vector3(initialScale.x * scale, initialScale.y * scale, initialScale.z * scale);
        /*transform.position =
            new Vector3(initalXRef - transform.localScale.x / 2 - scale * 0.95f,
            transform.localScale.y - scale * 1.45f, transform.position.z);*/

        transform.position =
            new Vector3(initalXRef - transform.localScale.x / 2 - scale * 0.95f,
            transform.localScale.y - scale * 1.45f, transform.position.z);
    }


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player  = PlayerManager.Instance;

        feedTimer = new Timer();
        feedTimer.Start(WaitingForFeeding);


        // Write a text with 1 digit precision for currentFood + "/" + FoodGoal
        foodText.text = currentFood + "/" + FoodGoal;

        initialScale = transform.localScale;

        initalXRef = transform.position.x + transform.localScale.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsWaiting)
        {
            feedTimer.Update();

            float remainingTime = WaitingForFeeding - feedTimer.ElapsedTime;
            TimeSpan time = TimeSpan.FromSeconds(remainingTime);

            if (remainingTime <= 0)
            {
                IsWaiting = false;
                timerText.text = "00:00:00:000";
                timerText.color = Color.red;
            }
            else
            {
                timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}",
                    time.Hours, time.Minutes, time.Seconds, time.Milliseconds);
            }

            // color from blue to red
            timerText.color = Color.Lerp(Color.red, Color.green, remainingTime / WaitingForFeeding);
        }
    }

    public void BlockHarverst()
    {
        IsWaiting = false;
    }
    public void UnblockHarverst()
    {
        IsWaiting = true;
    }

    public void ResetHarverst()
    {
        feedTimer.Start(WaitingForFeeding);
        IsWaiting = true;
    }

    public void Collect()
    {
        if (feedTimer.Finished)
        {
            // TODO MORT

            DeathPanelUI deathPanel = DeathPanelUI.Instance;
            if (deathPanel != null)
            {
                deathPanel.SetExplanationToTimeOut();
                deathPanel.ShowDeathPanel();
            }
        } else
        {
            if (player.Resource > 0)
            {
                counterFeeding++;
                counterTripText.text = counterFeeding.ToString();
            }

            SetFood(currentFood + player.Resource);
            player.RemoveResource(player.Resource);

            if (currentFood >= FoodGoal)
            {
               // TODO Réussite jeu
                VictoryPanelUI victoryPanelUI = VictoryPanelUI.Instance;
                victoryPanelUI.ShowVictoryPanel(counterFeeding);
            } else
            {
                // Reset certain thing in worlds
                Collectible.ResetAll();
                Damagable.ResetAll();

                // Reseting health of the player
                Damagable playerDamagable = player.GetComponent<Damagable>();
                playerDamagable.GiveHealth(playerDamagable.maxHealth + 12);

                m_onEnter.Invoke();
            }
        }
    }
}
