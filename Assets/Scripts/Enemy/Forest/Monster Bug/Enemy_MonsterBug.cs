using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MonsterBug : Enemy
{
    
    public MonsterBugIdleState idleState { get; private set; }
    public MonsterBugMoveState moveState { get; private set; }
    public MonsterBugAttackState attackState { get; private set; }
    public MonsterBugDeathState deathState { get; private set; }
    public MonsterBugChasingState chasingState { get; private set; }
    public MonsterBugBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new MonsterBugIdleState(this, stateMachine, "Idle", this);
        moveState = new MonsterBugMoveState(this, stateMachine, "Move", this);
        attackState = new MonsterBugAttackState(this, stateMachine, "Attack", this);
        deathState = new MonsterBugDeathState(this, stateMachine, "Death", this);
        chasingState = new MonsterBugChasingState(this, stateMachine, "Move", this);
        battleState = new MonsterBugBattleState(this, stateMachine, "Idle", this);
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
