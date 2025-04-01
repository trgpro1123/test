using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Viking2 : Enemy
{
    
    public Viking2IdleState idleState { get; private set; }
    public Viking2MoveState moveState { get; private set; }
    public Viking2AttackState attackState { get; private set; }
    public Viking2DeathState deathState { get; private set; }
    public Viking2ChasingState chasingState { get; private set; }
    public Viking2BattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new Viking2IdleState(this, stateMachine, "Idle", this);
        moveState = new Viking2MoveState(this, stateMachine, "Move", this);
        attackState = new Viking2AttackState(this, stateMachine, "Attack", this);
        deathState = new Viking2DeathState(this, stateMachine, "Death", this);
        chasingState = new Viking2ChasingState(this, stateMachine, "Move", this);
        battleState = new Viking2BattleState(this, stateMachine, "Idle", this);
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
