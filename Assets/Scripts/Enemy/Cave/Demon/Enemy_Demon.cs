using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Demon : Enemy
{
    
    public DemonIdleState idleState { get; private set; }
    public DemonMoveState moveState { get; private set; }
    public DemonAttackState attackState { get; private set; }
    public DemonDeathState deathState { get; private set; }
    public DemonChasingState chasingState { get; private set; }
    public DemonBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new DemonIdleState(this, stateMachine, "Idle", this);
        moveState = new DemonMoveState(this, stateMachine, "Move", this);
        attackState = new DemonAttackState(this, stateMachine, "Attack", this);
        deathState = new DemonDeathState(this, stateMachine, "Death", this);
        chasingState = new DemonChasingState(this, stateMachine, "Move", this);
        battleState = new DemonBattleState(this, stateMachine, "Idle", this);
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
        attackObject.transform.localScale=new Vector2(attackSize,attackSize);
        EnemyCreateAreaAttack(attackObject,StatType.strength,charaterStats.strength.GetValue(),attackDistance,angleToPlayer,0);
    }
}
