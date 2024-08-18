using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public static void SaveScore(int level, int trip)
    {
        PlayerPrefs.SetInt("Level" + level, trip);
        PlayerPrefs.Save();
    }

    public static int LoadScore(int level)
    {
        if (!PlayerPrefs.HasKey("Level" + level))
        {
            return 0;
        }

        return PlayerPrefs.GetInt("Level" + level, 0);
    }
}
