using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Viking3 : Enemy
{
    
    public Viking3IdleState idleState { get; private set; }
    public Viking3MoveState moveState { get; private set; }
    public Viking3AttackState attackState { get; private set; }
    public Viking3DeathState deathState { get; private set; }
    public Viking3ChasingState chasingState { get; private set; }
    public Viking3BattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new Viking3IdleState(this, stateMachine, "Idle", this);
        moveState = new Viking3MoveState(this, stateMachine, "Move", this);
        attackState = new Viking3AttackState(this, stateMachine, "Attack", this);
        deathState = new Viking3DeathState(this, stateMachine, "Death", this);
        chasingState = new Viking3ChasingState(this, stateMachine, "Move", this);
        battleState = new Viking3BattleState(this, stateMachine, "Idle", this);
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
