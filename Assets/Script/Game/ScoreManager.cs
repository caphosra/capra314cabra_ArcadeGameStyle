using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager
{
    public static int Score = 0;

    public static void HiScoreSave(int score)
    {
        PlayerPrefs.SetInt("HiScore", score);
    }

    public static int HiScoreLoad()
    {
        if (PlayerPrefs.HasKey("HiScore"))
        {
            return PlayerPrefs.GetInt("HiScore");
        }
        else
        {
            HiScoreSave(0);
            return 0;
        }
    }
}
