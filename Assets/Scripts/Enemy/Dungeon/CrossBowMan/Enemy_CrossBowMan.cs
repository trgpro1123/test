using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_CrossBowMan : Enemy
{
    [Header("Projectile Properties")]
    public GameObject projectile;
    public float speed;
    public float lifeTime;

    
    public CrossBowManIdleState idleState { get; private set; }
    public CrossBowManMoveState moveState { get; private set; }
    public CrossBowManAttackState attackState { get; private set; }
    public CrossBowManDeathState deathState { get; private set; }
    public CrossBowManChasingState chasingState { get; private set; }
    public CrossBowManBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new CrossBowManIdleState(this, stateMachine, "Idle", this);
        moveState = new CrossBowManMoveState(this, stateMachine, "Move", this);
        attackState = new CrossBowManAttackState(this, stateMachine, "Attack", this);
        deathState = new CrossBowManDeathState(this, stateMachine, "Death", this);
        chasingState = new CrossBowManChasingState(this, stateMachine, "Move", this);
        battleState = new CrossBowManBattleState(this, stateMachine, "Idle", this);
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
        newProjectile.GetComponent<CrossBowManProjectile>().SetupProjectile(charaterStats.strength.GetValue(),speed,lifeTime);
    }
    public void DestroySeflIn(float _time){
        Destroy(gameObject,_time);
    }
}
