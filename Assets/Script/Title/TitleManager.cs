using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public Text hiScoreText;

    public string firstStage;
    public string optionScene;

    // Use this for initialization
    void Start()
    {
        hiScoreText.text = "HI SCORE " + System.String.Format("{0:D7}", ScoreManager.HiScoreLoad());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(firstStage);
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(optionScene);
    }
}
