using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float AimedTime { get; private set; }
    public float ElapsedTime { get; private set; }

    public bool Stopped { get; private set; }

    public bool Finished { get; private set; }

    public bool Started { get; private set; }

    public bool ElapsedTimeCapped { get; set; }

    public Timer()
    {
        AimedTime = 0;
        ElapsedTime = 0;

        Started = false;
        Finished = false;
        ElapsedTimeCapped = true;
    }

    public void Update()
    {
        if (!Stopped && !Finished && Started)
        {
            ElapsedTime += Time.deltaTime;

            if (ElapsedTime >= AimedTime)
            {
                if (ElapsedTimeCapped)
                    ElapsedTime = AimedTime;

                Finished = true;
                Started = true;
            }
        }
    }

    public void Start(float aimedTime)
    {
        AimedTime = aimedTime;
        ResetStart();
    }

    public void ResetStart()
    {
        Stopped = false;
        Finished = false;
        Started = true;
        ElapsedTime = 0;
    }

    public void ResetStop()
    {
        ResetStart();
        Stop();
    }

    public void Stop()
    {
        Stopped = true;
    }

    public void Restart()
    {
        Stopped = false;
    }


    public void ForceTime(float newElapsedTime)
    {
        if (this.ElapsedTimeCapped && ElapsedTime > AimedTime)
        {
            ElapsedTime = AimedTime;
        } else 
        {
            ElapsedTime = newElapsedTime;
        }
    }


    public void InitRandomTime()
    {
        ElapsedTime = Random.Range(0, AimedTime);
    }
}
