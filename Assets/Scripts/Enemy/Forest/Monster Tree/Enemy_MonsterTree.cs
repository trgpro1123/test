using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MonsterTree : Enemy
{

    // public Transform destination;
    public MonsterTreeIdleState idleState {get;private set;}
    public MonsterTreeMoveState moveState {get;private set;}
    public MonsterTreeAttackState attackState {get;private set;}
    public MonsterTreeBattleState battleState {get;private set;}
    public MonsterTreeChasingState chasingState {get;private set;}
    public MonsterTreePatroldState patroldState {get;private set;}
    public MonsterTreeDeathState deathState {get;private set;}

  
    

    protected override void Awake()
    {
        base.Awake();
        idleState=new MonsterTreeIdleState(this,stateMachine,"Idle",this);
        moveState=new MonsterTreeMoveState(this,stateMachine,"Move",this);
        attackState=new MonsterTreeAttackState(this,stateMachine,"Attack",this);
        battleState=new MonsterTreeBattleState(this,stateMachine,"Idle",this);
        chasingState=new MonsterTreeChasingState(this,stateMachine,"Move",this);
        deathState=new MonsterTreeDeathState(this,stateMachine,"Death",this);

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
        // navMeshAgent.enabled=false;
        // navMeshAgent.updatePosition=false;
        // GetComponentInChildren<Dissolve>().StartCoroutine("Vanish");
        stateMachine.ChangeState(deathState);
    }

    

    public override void Attack1()
    {
        attackObject.transform.localScale=new Vector2(attackSize,attackSize);
        EnemyCreateAreaAttack(attackObject,StatType.strength,charaterStats.strength.GetValue(),attackDistance,angleToPlayer,0);
    }
    
    
}
