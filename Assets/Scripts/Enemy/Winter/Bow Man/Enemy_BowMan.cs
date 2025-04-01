using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BowMan : Enemy
{
    [Header("Projectile Properties")]
    public GameObject projectile;
    public float speed;
    public float lifeTime;

    
    public BowManIdleState idleState { get; private set; }
    public BowManMoveState moveState { get; private set; }
    public BowManAttackState attackState { get; private set; }
    public BowManDeathState deathState { get; private set; }
    public BowManChasingState chasingState { get; private set; }
    public BowManBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new BowManIdleState(this, stateMachine, "Idle", this);
        moveState = new BowManMoveState(this, stateMachine, "Move", this);
        attackState = new BowManAttackState(this, stateMachine, "Attack", this);
        deathState = new BowManDeathState(this, stateMachine, "Death", this);
        chasingState = new BowManChasingState(this, stateMachine, "Move", this);
        battleState = new BowManBattleState(this, stateMachine, "Idle", this);
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
        GameObject newProjectile = Instantiate(projectile, attackCheck.transform.position,Quaternion.Euler(0, 0, angleToPlayer-90));
        newProjectile.GetComponent<BowManProjectile>().SetupProjectile(charaterStats.strength.GetValue(),speed,lifeTime,charaterStats);
    }
    public void DestroySeflIn(float _time){
        Destroy(gameObject,_time);
    }
}
