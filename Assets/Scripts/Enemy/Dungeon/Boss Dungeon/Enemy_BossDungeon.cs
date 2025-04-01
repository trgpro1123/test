using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BossDungeon : Enemy
{


    [Header("Skill Attack 1")]
    public int chanceSkill1;
    public int defaultChanceSkill1;
    // public float cooldownSkill1;
    // public float lastTimeUseSkill1;

    [Header("Skill Attack 2")]
    public GameObject skillObject2;
    public float skilSize;
    public float skill2DamagePerCent;
    public int chanceSkill2;
    public int defaultChanceSkill2;
    private GameObject areaSkillEffect;
    // public float cooldownSkill2;
    // public float lastTimeUseSkill2;

    [Header("Skill Attack 3")]
    public GameObject skillObject3;
    public GameObject objectToSpawn;
    public float radius = 3f;
    public float skill3DamagePerCent;
    public int maxCount;
    public float timeToNextCreateClone;
    public int chanceSkill3;
    public int defaultChanceSkill3;
    // public float cooldownSkill3;
    // public float lastTimeUseSkill3;
    


    
    public BossDungeonIdleState idleState { get; private set; }
    public BossDungeonMoveState moveState { get; private set; }
    public BossDungeonDeathState deathState { get; private set; }
    public BossDungeonChasingState chasingState { get; private set; }
    public BossDungeonBattleState battleState { get; private set; }
    public BossDungeonAttackState attackState { get; private set; }
    public BossDungeonAttackState1 attackState1 { get; private set; }
    public BossDungeonAttackState2 attackState2 { get; private set; }
    public BossDungeonAttackState3 attackState3 { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new BossDungeonIdleState(this, stateMachine, "Idle", this);
        moveState = new BossDungeonMoveState(this, stateMachine, "Move", this);
        deathState = new BossDungeonDeathState(this, stateMachine, "Death", this);
        chasingState = new BossDungeonChasingState(this, stateMachine, "Move", this);
        battleState = new BossDungeonBattleState(this, stateMachine, "Idle", this);
        attackState = new BossDungeonAttackState(this, stateMachine, "Attack", this);
        attackState1 = new BossDungeonAttackState1(this, stateMachine, "Attack 1", this);
        attackState2 = new BossDungeonAttackState2(this, stateMachine, "Attack 2", this);
        attackState3 = new BossDungeonAttackState3(this, stateMachine, "Attack 3", this);
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

    public bool CanUseBossDungeonSkill1(){
        if(Random.Range(0,100)<=chanceSkill1){
            chanceSkill1=defaultChanceSkill1;
            return true;
        }
        chanceSkill1+=5;
        return false;
    }
    public bool CanUseBossDungeonSkill2(){
        if(Random.Range(0,100)<=chanceSkill2){
            chanceSkill2=defaultChanceSkill2;
            return true;
        }
        chanceSkill2+=5;
        return false;
    }
    public bool CanUseBossDungeonSkill3(){
        if(Random.Range(0,100)<=chanceSkill3){
            chanceSkill3=defaultChanceSkill3;
            return true;
        }
        chanceSkill3+=5;
        return false;
    }
    

    public override void Attack1()
    {
        // attackSize=attackSize1;
        // attackDistance=attackDistance1;
        attackObject.transform.localScale=new Vector2(attackSize,attackSize);
        EnemyCreateAreaAttack(attackObject,StatType.strength,charaterStats.strength.GetValue(),attackDistance,angleToPlayer,0);
    }
    public override void Attack2()
    {
        if(areaSkillEffect!=null)
            areaSkillEffect.GetComponent<BossDungeonSkillAttack2>().TriggerSkill();
        
    }
    public override void Attack3()
    {
        int damage=Mathf.RoundToInt(charaterStats.strength.GetValue()*skill3DamagePerCent);
        GameObject newSkillObject3 = Instantiate(skillObject3, transform.position, Quaternion.identity);
        newSkillObject3.GetComponent<BossDungeonSkillAttack3>().SetUpSkill(objectToSpawn,damage,attackSize,attackDistance,maxCount,radius,timeToNextCreateClone,this);
        this.gameObject.SetActive(false);
    }
    public void CreateSkillAttack2(){
        stateMachine.ChangeState(attackState2);
        int damage=Mathf.RoundToInt(charaterStats.strength.GetValue()*skill2DamagePerCent);
        areaSkillEffect=Instantiate(skillObject2,transform.position,Quaternion.identity);
        areaSkillEffect.GetComponent<BossDungeonSkillAttack2>().SetupSkill(damage,skilSize, charaterStats);
    }


}
