using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SwordAndHammerWarrior : Enemy
{


    
    
    public SwordAndHammerWarriorIdleState idleState { get; private set; }
    public SwordAndHammerWarriorMoveState moveState { get; private set; }
    public SwordAndHammerWarriorAttackState1 attackState1 { get; private set; }
    public SwordAndHammerWarriorAttackState2 attackState2 { get; private set; }
    public SwordAndHammerWarriorDeathState deathState { get; private set; }
    public SwordAndHammerWarriorChasingState chasingState { get; private set; }
    public SwordAndHammerWarriorBattleState battleState { get; private set; }



    protected override void Awake()
    {
        base.Awake();
        idleState = new SwordAndHammerWarriorIdleState(this, stateMachine, "Idle", this);
        moveState = new SwordAndHammerWarriorMoveState(this, stateMachine, "Move", this);
        attackState1 = new SwordAndHammerWarriorAttackState1(this, stateMachine, "Attack 1", this);
        attackState2 = new SwordAndHammerWarriorAttackState2(this, stateMachine, "Attack 2", this);
        deathState = new SwordAndHammerWarriorDeathState(this, stateMachine, "Death", this);
        chasingState = new SwordAndHammerWarriorChasingState(this, stateMachine, "Move", this);
        battleState = new SwordAndHammerWarriorBattleState(this, stateMachine, "Idle", this);
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
