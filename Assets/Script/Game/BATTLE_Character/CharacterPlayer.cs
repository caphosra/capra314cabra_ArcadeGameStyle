#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPlayer : BATTLE_Character
{
    public GameDirector gameDirector;

    public PlayerType type;

    [SerializeField]
    private string m_AttackTrigger;
    [SerializeField]
    private GameObject died;
    [SerializeField]
    private GameObject fire;
    [SerializeField]
    private GameObject ice;
    [SerializeField]
    private Sprite fireMode;
    [SerializeField]
    private Sprite iceMode;
    [SerializeField]
    private Transform bullet_from;
    [SerializeField]
    private Transform bullet_to;

    [SerializeField]
    private Text HPText;
    [SerializeField]
    private Image PlayerModeImage;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        HP = MaxHP;
        HPTextUpdate();
    }

    private void Update()
    {
        StatusCheck();
        gameDirector.CheckEnemyAllDied();
    }

    public override void Attack()
    {
        animator.SetTrigger(m_AttackTrigger);
    }

    public override void Damege(int damege, AttackType attack)
    {
        HP -= damege;
        if (HP <= 0)
        {
            HP = 0;
            HPTextUpdate();
            GetComponent<SpriteRenderer>().enabled = false;
            Instantiate(died, transform.position, transform.rotation);
            gameDirector.GameOver();
        }
        else HPTextUpdate();
    }

    public void ChangeCharacterType(PlayerType p)
    {
        if (p == type) return;

        type = p;
        switch(p)
        {
            case PlayerType.Fire:
                {
                    animator.SetTrigger("Change_REDMAN");
                    PlayerModeImage.sprite = fireMode;
                }
                break;
            case PlayerType.Water:
                {
                    animator.SetTrigger("Change_BLUEMAN");
                    PlayerModeImage.sprite = iceMode;
                }
                break;
            case PlayerType.Wind:
                {
                    animator.SetTrigger("Change_GREENMAN");
                }
                break;
            case PlayerType.Thunder:
                {
                    animator.SetTrigger("Change_YELLOWMAN");
                }
                break;
        }
    }

    public void ShootFire()
    {
        GameFunc.ShootSomething(fire, bullet_from.position, bullet_to.position);
    }

    public void ShootIce()
    {
        GameFunc.ShootSomething(ice, bullet_from.position, bullet_to.position);
    }

    public void HPTextUpdate()
    {
        HPText.text = "HP : " + System.String.Format("{0:D4}", HP) + "/" + System.String.Format("{0:D4}", MaxHP);
    }
}
