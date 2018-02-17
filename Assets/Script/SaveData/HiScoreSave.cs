using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiScoreSaver : MonoBehaviour
{
    public static void Save(int score)
    {
        PlayerPrefs.SetInt("HiScore", score);
    }

    public static int Load()
    {
        if(PlayerPrefs.HasKey("HiScore"))
        {
            return PlayerPrefs.GetInt("HiScore");
        }
        else
        {
            Save(0);
            return 0;
        }
    }
}
