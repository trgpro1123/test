using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_IceGolem : Enemy
{
    
    public IceGolemIdleState idleState { get; private set; }
    public IceGolemMoveState moveState { get; private set; }
    public IceGolemAttackState attackState { get; private set; }
    public IceGolemDeathState deathState { get; private set; }
    public IceGolemChasingState chasingState { get; private set; }
    public IceGolemBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new IceGolemIdleState(this, stateMachine, "Idle", this);
        moveState = new IceGolemMoveState(this, stateMachine, "Move", this);
        attackState = new IceGolemAttackState(this, stateMachine, "Attack", this);
        deathState = new IceGolemDeathState(this, stateMachine, "Death", this);
        chasingState = new IceGolemChasingState(this, stateMachine, "Move", this);
        battleState = new IceGolemBattleState(this, stateMachine, "Idle", this);
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
