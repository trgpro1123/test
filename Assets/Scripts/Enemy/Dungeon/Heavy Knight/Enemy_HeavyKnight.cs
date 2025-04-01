using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HeavyKnight : Enemy
{
    
    public HeavyKnightIdleState idleState { get; private set; }
    public HeavyKnightMoveState moveState { get; private set; }
    public HeavyKnightAttackState attackState { get; private set; }
    public HeavyKnightDeathState deathState { get; private set; }
    public HeavyKnightChasingState chasingState { get; private set; }
    public HeavyKnightBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new HeavyKnightIdleState(this, stateMachine, "Idle", this);
        moveState = new HeavyKnightMoveState(this, stateMachine, "Move", this);
        attackState = new HeavyKnightAttackState(this, stateMachine, "Attack", this);
        deathState = new HeavyKnightDeathState(this, stateMachine, "Death", this);
        chasingState = new HeavyKnightChasingState(this, stateMachine, "Move", this);
        battleState = new HeavyKnightBattleState(this, stateMachine, "Idle", this);
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
