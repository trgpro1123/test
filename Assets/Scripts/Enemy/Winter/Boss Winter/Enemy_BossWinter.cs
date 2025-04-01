using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BossWinter : Enemy
{

    // [Header("Viking Skill")]
    // public GameObject skillObject;
    // public float skillDamagePerCent;
    // public float speed;
    // public float lifeTime;
    // public int chanceSkill;
    // public int defaultChanceSkill;
    // public float cooldownSkill;
    [Header("Nomar Attack")]
    public float attackDistance1;
    public float attackSize1;

    [Header("Attack 1")]
    public GameObject skillObject1;
    public float skill1DamagePerCent;
    public int chanceSkill1;
    public int defaultChanceSkill1;
    public float cooldownSkill1;
    public float lastTimeUseSkill1;

    [Header("Attack 2")]
    public GameObject skillObject2;
    public AnimationCurve animationCurve;
    public float skill2DamagePerCent;
    public int chanceSkill2;
    public int defaultChanceSkill2;
    public float skillDuration;
    public float skillHeight;
    public float cooldownSkill2;
    public float lastTimeUseSkill2;
    private IEnumerator JumpCoroutine;

    [Header("Attack 3")]
    public float attackDistance3;
    public float attackSize3;
    public float skill3DamagePerCent;
    public int chanceSkill3;
    public int defaultChanceSkill3;
    public float cooldownSkill3;
    public float lastTimeUseSkill3;
    


    
    public BossWinterIdleState idleState { get; private set; }
    public BossWinterMoveState moveState { get; private set; }
    public BossWinterDeathState deathState { get; private set; }
    public BossWinterChasingState chasingState { get; private set; }
    public BossWinterBattleState battleState { get; private set; }
    public BossWinterJumpState jumpState { get; private set; }
    public BossWinterAirState airState { get; private set; }
    public BossWinterAttackState attackState { get; private set; }
    public BossWinterAttackState1 attackState1 { get; private set; }
    public BossWinterAttackState2 attackState2 { get; private set; }
    public BossWinterAttackState attackState3 { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new BossWinterIdleState(this, stateMachine, "Idle", this);
        moveState = new BossWinterMoveState(this, stateMachine, "Move", this);
        deathState = new BossWinterDeathState(this, stateMachine, "Death", this);
        chasingState = new BossWinterChasingState(this, stateMachine, "Move", this);
        battleState = new BossWinterBattleState(this, stateMachine, "Idle", this);
        jumpState = new BossWinterJumpState(this, stateMachine, "Jump", this);
        airState = new BossWinterAirState(this, stateMachine, "Air", this);
        attackState = new BossWinterAttackState(this, stateMachine, "Attack", this);
        attackState1 = new BossWinterAttackState1(this, stateMachine, "Attack 1", this);
        attackState2 = new BossWinterAttackState2(this, stateMachine, "Attack 2", this);
        attackState3 = new BossWinterAttackState(this, stateMachine, "Attack 3", this);
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

    public bool CanUseBossWinterSkill1(){
        if(Random.Range(0,100)<=chanceSkill1){
            chanceSkill1=defaultChanceSkill1;
            return true;
        }
        chanceSkill1+=5;
        return false;
    }
    public bool CanUseBossWinterSkill2(){
        if(Random.Range(0,100)<=chanceSkill2){
            chanceSkill2=defaultChanceSkill2;
            return true;
        }
        chanceSkill2+=5;
        return false;
    }
    public bool CanUseBossWinterSkill3(){
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
        int damage=Mathf.RoundToInt(charaterStats.strength.GetValue()*skill1DamagePerCent);
        GameObject newSkill=Instantiate(skillObject1,player.transform.position,Quaternion.identity);
        newSkill.GetComponent<BossWinterSkill1>().SetupSkill(damage,charaterStats);
    }
    public override void Attack3()
    {
        JumpCoroutine=JumpRoutine(transform.position,player.transform.position);
        StartCoroutine(JumpCoroutine);
    }
    public override void Attack4()
    {
        // attackSize=attackSize3;
        // attackDistance=attackDistance3;
        int damage=Mathf.RoundToInt(charaterStats.strength.GetValue()*skill3DamagePerCent);
        attackObject.transform.localScale=new Vector2(attackSize,attackSize3);
        EnemyCreateAreaAttack(attackObject,StatType.strength,damage,attackDistance,angleToPlayer,0);
    }
    IEnumerator JumpRoutine(Vector3 startPosition,Vector3 endPosition){
        FlipController(player.transform.position.x-transform.position.x);
        stateMachine.ChangeState(jumpState);
        charaterStats.MakeInvinsable(true);
        GameObject newBossWinterJumpEffect=Instantiate(skillObject2,endPosition,Quaternion.identity);
        BossWinterSkill2 bossWinterSkill2=newBossWinterJumpEffect.GetComponent<BossWinterSkill2>();
        float timePassed=0f;
        while(timePassed<skillDuration){
            timePassed+=Time.deltaTime;
            float linearT=timePassed/skillDuration;
            float heightT=animationCurve.Evaluate(linearT);
            float height=Mathf.Lerp(0f,skillHeight,heightT);

            transform.position=Vector2.Lerp(startPosition,endPosition,linearT)+new Vector2(0f,height);


            yield return null;
        }
        stateMachine.ChangeState(attackState2);
        bossWinterSkill2.SetupSkill(Mathf.RoundToInt(charaterStats.strength.GetValue()*skill2DamagePerCent),charaterStats);
        bossWinterSkill2.TriggerSkill();
        charaterStats.MakeInvinsable(false);
        StopCoroutine(JumpCoroutine);
        // StopAllCoroutines();
        
    }

}
