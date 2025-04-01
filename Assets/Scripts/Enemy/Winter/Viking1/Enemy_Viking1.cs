using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Viking1 : Enemy
{

    [Header("Viking Skill")]
    public GameObject skillObject;
    public float skillDamagePerCent;
    public float speed;
    public float lifeTime;
    public int chanceSkill;
    public int defaultChanceSkill;
    // public float cooldownSkill;
    

    
    public Viking1IdleState idleState { get; private set; }
    public Viking1MoveState moveState { get; private set; }
    public Viking1AttackState1 attackState1 { get; private set; }
    public Viking1AttackState2 attackState2 { get; private set; }
    public Viking1DeathState deathState { get; private set; }
    public Viking1ChasingState chasingState { get; private set; }
    public Viking1BattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new Viking1IdleState(this, stateMachine, "Idle", this);
        moveState = new Viking1MoveState(this, stateMachine, "Move", this);
        attackState1 = new Viking1AttackState1(this, stateMachine, "Attack 1", this);
        attackState2 = new Viking1AttackState2(this, stateMachine, "Attack 2", this);
        deathState = new Viking1DeathState(this, stateMachine, "Death", this);
        chasingState = new Viking1ChasingState(this, stateMachine, "Move", this);
        battleState = new Viking1BattleState(this, stateMachine, "Idle", this);
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

    public bool CanUseViking1Skill(){
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
        int damage=Mathf.RoundToInt(charaterStats.magicPower.GetValue()*skillDamagePerCent);
        GameObject newSkill=Instantiate(skillObject,transform.position,Quaternion.Euler(0, 0, angleToPlayer ));
        newSkill.GetComponent<Viking1_Skill>().SetupSkill(damage,speed,lifeTime,charaterStats);
    }
}
