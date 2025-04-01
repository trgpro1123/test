using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Warrior : Enemy
{
    
    public WarriorIdleState idleState { get; private set; }
    public WarriorMoveState moveState { get; private set; }
    public WarriorAttackState attackState { get; private set; }
    public WarriorDeathState deathState { get; private set; }
    public WarriorChasingState chasingState { get; private set; }
    public WarriorBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new WarriorIdleState(this, stateMachine, "Idle", this);
        moveState = new WarriorMoveState(this, stateMachine, "Move", this);
        attackState = new WarriorAttackState(this, stateMachine, "Attack", this);
        deathState = new WarriorDeathState(this, stateMachine, "Death", this);
        chasingState = new WarriorChasingState(this, stateMachine, "Move", this);
        battleState = new WarriorBattleState(this, stateMachine, "Idle", this);
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
