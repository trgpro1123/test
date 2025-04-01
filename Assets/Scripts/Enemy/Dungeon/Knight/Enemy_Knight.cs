using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Knight : Enemy
{
    
    public KnightIdleState idleState { get; private set; }
    public KnightMoveState moveState { get; private set; }
    public KnightAttackState attackState { get; private set; }
    public KnightDeathState deathState { get; private set; }
    public KnightChasingState chasingState { get; private set; }
    public KnightBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new KnightIdleState(this, stateMachine, "Idle", this);
        moveState = new KnightMoveState(this, stateMachine, "Move", this);
        attackState = new KnightAttackState(this, stateMachine, "Attack", this);
        deathState = new KnightDeathState(this, stateMachine, "Death", this);
        chasingState = new KnightChasingState(this, stateMachine, "Move", this);
        battleState = new KnightBattleState(this, stateMachine, "Idle", this);
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
