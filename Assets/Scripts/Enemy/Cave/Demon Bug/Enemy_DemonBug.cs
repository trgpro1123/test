using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DemonBug : Enemy
{
    [Header("Projectile Properties")]
    public GameObject projectile;
    public float speed;
    public float lifeTime;

    
    public DemonBugIdleState idleState { get; private set; }
    public DemonBugMoveState moveState { get; private set; }
    public DemonBugAttackState attackState { get; private set; }
    public DemonBugDeathState deathState { get; private set; }
    public DemonBugChasingState chasingState { get; private set; }
    public DemonBugBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new DemonBugIdleState(this, stateMachine, "Idle", this);
        moveState = new DemonBugMoveState(this, stateMachine, "Move", this);
        attackState = new DemonBugAttackState(this, stateMachine, "Attack", this);
        deathState = new DemonBugDeathState(this, stateMachine, "Death", this);
        chasingState = new DemonBugChasingState(this, stateMachine, "Move", this);
        battleState = new DemonBugBattleState(this, stateMachine, "Idle", this);
    }
    protected override void Start() {
        base.Start();
        stateMachine.Initialize(idleState);
        
        
        
    }
    protected override void Update()
    {
        base.Update();
        
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
    }


    

    public override void Attack1()
    {
        GameObject newProjectile = Instantiate(projectile, attackCheck.transform.position,Quaternion.Euler(0, 0, angleToPlayer ));
        newProjectile.GetComponent<DemonBugProjectile>().SetUpProjectile(charaterStats.magicPower.GetValue(),speed,lifeTime,charaterStats);
    }
    public void DestroySeflIn(float _time){
        Destroy(gameObject,_time);
    }
}
