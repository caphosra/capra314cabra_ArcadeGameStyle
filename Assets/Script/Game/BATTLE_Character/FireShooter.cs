using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShooter : BATTLE_Character {

    [SerializeField]
    private string m_AttackTrigger;
    [SerializeField]
    private GameObject fire;
    [SerializeField]
    private Transform bullet_from;
    [SerializeField]
    private Transform bullet_to;

    private Animator animator;

    private void Start()
    {
        firePool = GameObject.Find("Fire_EnemyPool").GetComponent<ObjectPooling>();

        animator = GetComponent<Animator>();
        HP = MaxHP;
    }

    private void Update()
    {
        StatusCheck();
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
