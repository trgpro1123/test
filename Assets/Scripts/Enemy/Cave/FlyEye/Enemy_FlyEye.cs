using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FlyEye : Enemy
{
    
    public FlyEyeIdleState idleState { get; private set; }
    public FlyEyeMoveState moveState { get; private set; }
    public FlyEyeAttackState attackState { get; private set; }
    public FlyEyeDeathState deathState { get; private set; }
    public FlyEyeChasingState chasingState { get; private set; }
    public FlyEyeBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new FlyEyeIdleState(this, stateMachine, "Idle", this);
        moveState = new FlyEyeMoveState(this, stateMachine, "Idle", this);
        attackState = new FlyEyeAttackState(this, stateMachine, "Attack", this);
        deathState = new FlyEyeDeathState(this, stateMachine, "Death", this);
        chasingState = new FlyEyeChasingState(this, stateMachine, "Idle", this);
        battleState = new FlyEyeBattleState(this, stateMachine, "Idle", this);
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
