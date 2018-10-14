using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSniper : BATTLE_Character
{
    [SerializeField]
    private string m_AttackTrigger;
    [SerializeField]
    private GameObject fire;
    [SerializeField]
    private Transform bullet_from;
    [SerializeField]
    private Transform bullet_to;

    private Animator animator;
    private Rigidbody2D rigid2D;
    private GameDirector gameDirector;

    private void Start()
    {
        firePool = GameObject.Find("Fire_EnemyPool").GetComponent<ObjectPooling>();

        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        HP = MaxHP;
    }

    private void Update()
    {
        if(StatusCheck())
        {
            var me = transform;
            var player = gameDirector.player.transform;
            transform.localScale = GameFunc.LookAtVector3(me.position, player.position);
            rigid2D.MovePosition(Vector2.MoveTowards(me.position, new Vector2(me.position.x, player.position.y), Speed));
        }
    }

    public override void Attack()
    {
        // 自動
    }

    [SerializeField]
    private ObjectPooling firePool;
    public void ShootFire()
    {
        GameFunc.ShootSomething(firePool, bullet_from.position, bullet_to.position);
    }
}
