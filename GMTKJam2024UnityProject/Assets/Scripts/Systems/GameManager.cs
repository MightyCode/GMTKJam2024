using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public TextMeshPro text;

    private Timer timer;
    public float HarvestTime = 10;

    public bool Harvesting = true;

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

            if (remainingTime <= 0)
            {
                Harvesting = false;
                text.text = "0.0.0";
            }
            else
            {
                text.text = "0.00";
            }

        }
    }
}
