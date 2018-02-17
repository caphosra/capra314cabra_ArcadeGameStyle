using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COPY_PLAYER : BATTLE_Character
{
    private GameDirector gameDirector;

    [SerializeField]
    private Transform[] bullet_to;
    [SerializeField]
    private Transform[] bullet_from;
    [SerializeField]
    private GameObject darkBall;

    //MyComponents
    private Animator animator;
    private Rigidbody2D rigidBody2D;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();

        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();

        HP = MaxHP;
    }

    private void Update()
    {
        if(StatusCheck())
        {
            transform.localScale = 
                GameFunc.LookAtVector3(transform.position, gameDirector.player.transform.position, false);
            // Y座標を合わせるように移動
            Vector3 target = transform.position;
            target.y = gameDirector.player.transform.position.y;
            rigidBody2D.MovePosition(Vector3.MoveTowards(transform.position, target, Speed));
        }
    }

    public void ShootDarkBall()
    {
        GameFunc.ShootSomething(darkBall, bullet_from[0].position, bullet_to[0].position);
    }
}
