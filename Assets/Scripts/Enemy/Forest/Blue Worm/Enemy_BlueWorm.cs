using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BlueWorm : Enemy
{
    [Header("Projectile Properties")]
    public GameObject projectile;
    public float speed;
    public float lifeTime;

    
    public BlueWormIdleState idleState { get; private set; }
    public BlueWormMoveState moveState { get; private set; }
    public BlueWormAttackState attackState { get; private set; }
    public BlueWormDeathState deathState { get; private set; }
    public BlueWormChasingState chasingState { get; private set; }
    public BlueWormBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new BlueWormIdleState(this, stateMachine, "Idle", this);
        moveState = new BlueWormMoveState(this, stateMachine, "Move", this);
        attackState = new BlueWormAttackState(this, stateMachine, "Attack", this);
        deathState = new BlueWormDeathState(this, stateMachine, "Death", this);
        chasingState = new BlueWormChasingState(this, stateMachine, "Move", this);
        battleState = new BlueWormBattleState(this, stateMachine, "Idle", this);
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
        newProjectile.GetComponent<BlueWormProjectile>().SetUpProjectile(charaterStats.strength.GetValue(),speed,lifeTime,charaterStats);
    }
    public void DestroySeflIn(float _time){
        Destroy(gameObject,_time);
    }
}
