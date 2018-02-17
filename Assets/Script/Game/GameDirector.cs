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
            }
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

/// <summary>
/// キャラクターのインターフェース
/// </summary>
public interface IBATTLE_Character
{
    int HP { get; set; }
    int MaxHP { get; }
    float Speed { get; }
    CharacterStatus Status { get; set; }
    GameObject Me { get; }
    void Damege(int damege, AttackType attack);
    void Heal(int heal);
    void AddStatus(CharacterStatus status);
    void Attack();
}

/// <summary>
/// キャラクターはこのクラスを継承してください
/// </summary>
public class BATTLE_Character : MonoBehaviour, IBATTLE_Character
{
    [SerializeField]
    protected CharacterDataTable dataTable;

    protected int hp;
    public virtual int HP { get { return hp; } set { hp = value; } }

    public virtual int MaxHP { get { return dataTable.MaxHP; } }

    public virtual float Speed { get { return dataTable.Speed; } }

    public virtual float FreezeTime { get { return dataTable.FreezeTime; } }

    protected CharacterStatus status;
    public CharacterStatus Status { get { return status; } set { status = value; } }

    public GameObject Me { get { return gameObject; } }

    public virtual void AddStatus(CharacterStatus status)
    {
        this.status = status;
        if (status == CharacterStatus.Freeze)
        {
            freezeTime = FreezeTime;
            var behaviour = gameObject.GetComponents<Behaviour>();
            foreach (var b in behaviour)
            {
                if (b is Animator || b is PlayerController) b.enabled = false;
            }
        }
    }

    public virtual void Attack()
    {

    }

    public virtual void Damege(int damege, AttackType attack)
    {
        hp -= damege;
        if (hp <= 0)
        {
            ScoreManager.Score += dataTable.Score;
            GameObject.Find("ScoreText").GetComponent<UnityEngine.UI.Text>().text =
                "Score : " + System.String.Format("{0:D7}", ScoreManager.Score);
            Destroy(gameObject);
        }
    }

    public virtual void Heal(int heal)
    {
        hp += heal;
    }

    private float freezeTime = 0;

    /// <summary>
    /// 現在のステータスを確認
    /// </summary>
    /// <returns>行動可能</returns>
    public virtual bool StatusCheck()
    {
        switch (Status)
        {
            case CharacterStatus.Freeze:
                {
                    freezeTime -= Time.deltaTime;
                    if (freezeTime <= 0)
                    {
                        freezeTime = 0f;
                        Status = CharacterStatus.None;
                        GameFunc.PauseObject(gameObject, true);
                    }
                    else return false;
                }
                break;
        }
        return true;
    }
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
