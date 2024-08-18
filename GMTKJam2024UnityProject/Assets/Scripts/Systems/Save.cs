using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public static int NO_SCORE = -1;

    public static void SaveTripScore(int level, int trip)
    {
        PlayerPrefs.SetInt("Level" + level, trip);
        PlayerPrefs.Save();
    }

    public static int LoadTripScore(int level)
    {
        if (!PlayerPrefs.HasKey("Level" + level))
        {
            return NO_SCORE;
        }

        return PlayerPrefs.GetInt("Level" + level, NO_SCORE);
    }
}
