using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMODOKI : BATTLE_Character
{
    public GameDirector gameDirector;
    public Rigidbody2D rigidBody2D;

    private void Start()
    {
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        HP = MaxHP;
    }

    private void Update()
    {
        if (StatusCheck())
        {
            transform.localScale = GameFunc.LookAtVector3(transform.position, gameDirector.player.transform.position);
            rigidBody2D.MovePosition(Vector2.MoveTowards(transform.position, gameDirector.player.transform.position, Speed));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            var hit = collision.gameObject.GetComponent(typeof(IBATTLE_Character)) as IBATTLE_Character;
            hit.Damege(200, AttackType.None);
        }
    }
}
