using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MonsterMushroom : Enemy
{
    
    public MonsterMushroomIdleState idleState { get; private set; }
    public MonsterMushroomMoveState moveState { get; private set; }
    public MonsterMushroomAttackState attackState { get; private set; }
    public MonsterMushroomDeathState deathState { get; private set; }
    public MonsterMushroomChasingState chasingState { get; private set; }
    public MonsterMushroomBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new MonsterMushroomIdleState(this, stateMachine, "Idle", this);
        moveState = new MonsterMushroomMoveState(this, stateMachine, "Move", this);
        attackState = new MonsterMushroomAttackState(this, stateMachine, "Attack", this);
        deathState = new MonsterMushroomDeathState(this, stateMachine, "Death", this);
        chasingState = new MonsterMushroomChasingState(this, stateMachine, "Move", this);
        battleState = new MonsterMushroomBattleState(this, stateMachine, "Idle", this);
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
