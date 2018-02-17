using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Text hiScoreText;
    public string firstStage;

    // Use this for initialization
    void Start()
    {
        hiScoreText.text = "HI SCORE " + System.String.Format("{0:D7}", ScoreManager.HiScoreLoad());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) UnityEngine.SceneManagement.SceneManager.LoadScene(firstStage);
    }
}
