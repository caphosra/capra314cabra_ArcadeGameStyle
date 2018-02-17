using UnityEngine;

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

