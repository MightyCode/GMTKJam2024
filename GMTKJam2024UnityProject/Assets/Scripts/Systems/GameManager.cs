using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timerText;

    [SerializeField]
    private TMP_Text resourceCounterText;

    private Timer timer;

    [SerializeField]
    float HarvestTime = 10;
    [SerializeField]
    bool Harvesting = true;

    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        timer = new Timer();
        timer.Start(HarvestTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Harvesting)
        {
            timer.Update();

            float remainingTime = HarvestTime - timer.ElapsedTime;
            TimeSpan time = TimeSpan.FromSeconds(remainingTime);

            if (remainingTime <= 0)
            {
                Harvesting = false;
                timerText.text = "00:00:00:000";
                timerText.color = Color.red;
            }
            else
            {
                timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}", 
                    time.Hours, time.Minutes, time.Seconds, time.Milliseconds);

                // color from blue to red
                timerText.color = Color.Lerp(Color.red, Color.green, remainingTime / HarvestTime);
            }
        }
    }

    public void BlockHarverst()
    {
        Harvesting = false;
    }
    public void UnblockHarverst()
    {
        Harvesting = true;
    }

    public void ResetHarverst()
    {
        timer.ResetStart();
        Harvesting = true;
    }
}
