using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEndClone : Enemy
{
    public BossEndCloneIdleState idleState { get; private set; }
    public BossEndCloneMoveState moveState { get; private set; }
    public BossEndCloneDeathState deathState { get; private set; }
    public BossEndCloneChasingState chasingState { get; private set; }
    public BossEndCloneBattleState battleState { get; private set; }
    public BossEndCloneAttackState attackState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new BossEndCloneIdleState(this, stateMachine, "Idle", this);
        moveState = new BossEndCloneMoveState(this, stateMachine, "Move", this);
        deathState = new BossEndCloneDeathState(this, stateMachine, "Death", this);
        chasingState = new BossEndCloneChasingState(this, stateMachine, "Move", this);
        battleState = new BossEndCloneBattleState(this, stateMachine, "Idle", this);
        attackState = new BossEndCloneAttackState(this, stateMachine, "Attack", this);

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
