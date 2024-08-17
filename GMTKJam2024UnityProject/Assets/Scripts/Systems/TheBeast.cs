using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TheBeast : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    [SerializeField] private PlayerManager player;

    [SerializeField]
    private TMP_Text timerText;

    private Timer feedTimer;

    [SerializeField]
    float waitingForFeeding = 20;
    [SerializeField]
    bool IsWaiting = true;

    [SerializeField]
    private TMP_Text foodText;
    public float FoodGoal;
    private float currentFood;

    [FormerlySerializedAs("collectSuccesfull")]
    [SerializeField]
    private UnityEvent m_onEnter = new UnityEvent();

    private Vector3 initialScale;
    private float initalXRef;

    // Start is called before the first frame update
    void Start()
    {
        feedTimer = new Timer();
        feedTimer.Start(waitingForFeeding);

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

            float remainingTime = waitingForFeeding - feedTimer.ElapsedTime;
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
            timerText.color = Color.Lerp(Color.red, Color.green, remainingTime / waitingForFeeding);
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
        feedTimer.ResetStart();
        IsWaiting = true;
    }

    public void Collect()
    {
        if (feedTimer.Finished)
        {
            // TODO MORT
        } else
        {
            currentFood += player.Resource;
            player.RemoveResource(player.Resource);

            if (currentFood != 0)
            {
                float scale = Mathf.Log(currentFood, player.BaseLog);
                scale = Mathf.Min(scale, player.MaxScale);

                transform.localScale = new Vector3(initialScale.x * scale, 1, initialScale.z * scale);
                transform.position =
                    new Vector3(initalXRef - transform.localScale.x / 2,
                    transform.localScale.y, transform.position.z);
            }


            if (currentFood >= FoodGoal)
            {
               // TODO Réussite jeu
            } else
            {

                foodText.text = currentFood + "/" + FoodGoal;
                
                m_onEnter.Invoke();
            }
        }
    }
}
