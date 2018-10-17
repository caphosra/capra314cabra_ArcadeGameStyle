using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public GameObject player;
    public GameObject gameCanvas;
    public GameObject gameClearCanvas;
    public GameObject gameOverCanvas;

    public GameObject enemys;

    private GameState gameState = GameState.WAIT;

    public GameState GameState
    {
        get
        {
            return gameState;
        }

        set
        {
            gameState = value;
        }
    }

    public string nextStage;
    public string gameOverScene;

    public float moveNextStageTime;
    public float moveGameOverSceneTime;

    // Use this for initialization
    void Start()
    {
        GameState = GameState.GAME;
        time = 0;

        GameObject.Find("ScoreText").GetComponent<UnityEngine.UI.Text>().text =
                "Score : " + System.String.Format("{0:D7}", ScoreManager.Score);
    }

    private float time;
    // Update is called once per frame
    void Update()
    {
        switch(GameState)
        {
            case GameState.GAMECLEAR:
                {
                    time += Time.deltaTime;
                    if(time > moveNextStageTime)
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene(nextStage);
                    }
                }
                break;
            case GameState.GAMEOVER:
                {
                    time += Time.deltaTime;
                    if (time > moveGameOverSceneTime)
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene(gameOverScene);
                    }
                }
                break;
        }
    }

    public void CheckEnemyAllDied()
    {
        if (enemys.transform.childCount == 0)
        {
            GameClear();
        }
    }

    public void GameClear()
    {
        if (GameState == GameState.GAME)
        {
            GameState = GameState.GAMECLEAR;

            foreach (GameObject obj in FindObjectsOfType(typeof(GameObject)))
            {
                foreach (var b in obj.GetComponents<Behaviour>())
                {
                    if (b is BATTLE_Character || b is PlayerController) b.enabled = false;
                }
                if(obj.tag == "Bullet")
                {
                    obj.SetActive(false);
                }
            }

            var p = player.GetComponent(typeof(IBATTLE_Character)) as IBATTLE_Character;
            ScoreManager.Score += p.HP;

            GameObject.Find("ScoreText").GetComponent<UnityEngine.UI.Text>().text =
                "Score : " + System.String.Format("{0:D7}", ScoreManager.Score);

            gameCanvas.SetActive(false);
            gameClearCanvas.SetActive(true);
        }
    }

    public void GameOver()
    {
        if (GameState == GameState.GAME)
        {
            GameState = GameState.GAMEOVER;

            foreach (GameObject obj in FindObjectsOfType(typeof(GameObject)))
            {
                foreach (var b in obj.GetComponents<Behaviour>())
                {
                    if (b is BATTLE_Character || b is PlayerController) b.enabled = false;
                }
            }
            gameCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
        }
    }
}

public class GameFunc
{
    /// <summary>
    /// 相手の方向を向く関数
    /// </summary>
    /// <param name="me">自分</param>
    /// <param name="to">相手</param>
    /// <param name="left">左向きであるか</param>
    /// <returns>localScale</returns>
    public static Vector3 LookAtVector3(Vector3 me, Vector3 to, bool left = true)
    {
        if (me.x > to.x) return new Vector3(left ? 5 : -5, 5, 5);
        else return new Vector3(left ? -5 : 5, 5, 5);
    }

    /// <summary>
    /// 弾を飛ばす関数
    /// </summary>
    public static void ShootSomething(GameObject bullet, Vector3 from, Vector3 to)
    {
        var obj = Object.Instantiate(bullet, from, Quaternion.identity);
        var bulletmove = obj.GetComponent<BulletMove>();
        bulletmove.m_To = to;
    }

    public static void ShootSomething(ObjectPooling pool, Vector3 from, Vector3 to)
    {
        var obj = pool.GetObject();
        obj.transform.position = from;
        obj.transform.rotation = Quaternion.identity;
        var bulletmove = obj.GetComponent<BulletMove>();
        bulletmove.m_To = to;
    }

    /// <summary>
    /// Objectを停止させます(enabledは止める時false,動かす時true)
    /// </summary>
    public static void PauseObjects(GameObject[] obj, bool enabled)
    {
        foreach (var o in obj)
        {
            var behaviours = o.GetComponents<Behaviour>();
            foreach (var b in behaviours)
            {
                b.enabled = enabled;
            }
        }
    }

    /// <summary>
    /// Objectを停止させます(enabledは止める時false,動かす時true)
    /// </summary>
    public static void PauseObject(GameObject obj, bool enabled)
    {
        var behaviours = obj.GetComponents<Behaviour>();
        foreach (var b in behaviours)
        {
            b.enabled = enabled;
        }
    }
}

public enum GameState
{
    WAIT,
    GAME,
    GAMECLEAR,
    GAMEOVER,
}

public enum AttackType
{
    None,
    Fire,
    Water,
    Wind,
    Thunder
}

public enum CharacterStatus
{
    None,
    Poison,
    Freeze
}
