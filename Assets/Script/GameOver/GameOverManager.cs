using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Text scoreText;

    public string titleScene;

    // Use this for initialization
    void Start()
    {
        scoreText.text = "Score : " + System.String.Format("{0:D7}", ScoreManager.Score);
        var highscore = ScoreManager.HiScoreLoad();
        if (highscore < ScoreManager.Score)
        {
            scoreText.text += " (New Record)";
            ScoreManager.HiScoreSave(ScoreManager.Score);
        }
        ScoreManager.Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene(titleScene);
    }
}
