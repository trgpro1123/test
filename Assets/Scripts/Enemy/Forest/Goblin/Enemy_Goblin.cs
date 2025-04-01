using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Goblin : Enemy
{

    [Header("Goblin Boom Skill")]
    public GameObject boomObject;
    public GameObject BoomEffect;
    public float skillDamagePerCent;
    public float skillDuration;
    public float skillHeight;
    public int chanceSkill;
    public int defaultChanceSkill;
    // public float cooldownSkill;

    
    public GoblinIdleState idleState { get; private set; }
    public GoblinMoveState moveState { get; private set; }
    public GoblinAttackState1 attackState1 { get; private set; }
    public GoblinAttackState2 attackState2 { get; private set; }
    public GoblinDeathState deathState { get; private set; }
    public GoblinChasingState chasingState { get; private set; }
    public GoblinPatrolState patrolState { get; private set; }
    public GoblinBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new GoblinIdleState(this, stateMachine, "Idle", this);
        moveState = new GoblinMoveState(this, stateMachine, "Move", this);
        attackState1 = new GoblinAttackState1(this, stateMachine, "Attack 1", this);
        attackState2 = new GoblinAttackState2(this, stateMachine, "Attack 2", this);
        deathState = new GoblinDeathState(this, stateMachine, "Death", this);
        chasingState = new GoblinChasingState(this, stateMachine, "Move", this);
        battleState = new GoblinBattleState(this, stateMachine, "Idle", this);
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

    public bool CanUseBoomGoblinSkill(){
        if(Random.Range(0,100)<=chanceSkill){
            chanceSkill=defaultChanceSkill;
            return true;
        }
        chanceSkill+=5;
        return false;
    }
    

    public override void Attack1()
    {
        attackObject.transform.localScale=new Vector2(attackSize,attackSize);
        EnemyCreateAreaAttack(attackObject,StatType.strength,charaterStats.strength.GetValue(),attackDistance,angleToPlayer,0);
    }
    public override void Attack2()
    {
        int damage=Mathf.RoundToInt(charaterStats.strength.GetValue()*skillDamagePerCent);
        GameObject boom=Instantiate(boomObject,transform.position,Quaternion.identity);
        boom.GetComponent<BoomGoblin_Skill>().SetUpBoomGoblinSkill(BoomEffect,damage,skillDuration,skillHeight,charaterStats);

    }
}
