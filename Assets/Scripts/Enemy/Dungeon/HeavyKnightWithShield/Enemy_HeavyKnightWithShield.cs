using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HeavyKnightWithShield : Enemy
{
    
    public HeavyKnightWithShieldIdleState idleState { get; private set; }
    public HeavyKnightWithShieldMoveState moveState { get; private set; }
    public HeavyKnightWithShieldAttackState attackState { get; private set; }
    public HeavyKnightWithShieldDeathState deathState { get; private set; }
    public HeavyKnightWithShieldChasingState chasingState { get; private set; }
    public HeavyKnightWithShieldBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new HeavyKnightWithShieldIdleState(this, stateMachine, "Idle", this);
        moveState = new HeavyKnightWithShieldMoveState(this, stateMachine, "Move", this);
        attackState = new HeavyKnightWithShieldAttackState(this, stateMachine, "Attack", this);
        deathState = new HeavyKnightWithShieldDeathState(this, stateMachine, "Death", this);
        chasingState = new HeavyKnightWithShieldChasingState(this, stateMachine, "Move", this);
        battleState = new HeavyKnightWithShieldBattleState(this, stateMachine, "Idle", this);
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
