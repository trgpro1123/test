using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DemonAxe : Enemy
{
    
    public DemonAxeIdleState idleState { get; private set; }
    public DemonAxeMoveState moveState { get; private set; }
    public DemonAxeAttackState attackState { get; private set; }
    public DemonAxeDeathState deathState { get; private set; }
    public DemonAxeChasingState chasingState { get; private set; }
    public DemonAxeBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new DemonAxeIdleState(this, stateMachine, "Idle", this);
        moveState = new DemonAxeMoveState(this, stateMachine, "Move", this);
        attackState = new DemonAxeAttackState(this, stateMachine, "Attack", this);
        deathState = new DemonAxeDeathState(this, stateMachine, "Death", this);
        chasingState = new DemonAxeChasingState(this, stateMachine, "Move", this);
        battleState = new DemonAxeBattleState(this, stateMachine, "Idle", this);
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
