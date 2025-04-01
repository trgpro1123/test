using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BossForest : Enemy
{

    [Header("Nomar Attack")]
    public float attackDistance1;
    public float attackSize1;

    [Header("Attack 1")]
    public GameObject skillObject1;
    public float skillAttackDelay;
    public float speed;
    public float skill1DamagePerCent;
    public float attackDistance2;
    public float attackSize2;
    public int chanceSkill1;
    public int defaultChanceSkill1;
    public float cooldownSkill1;
    public float lastTimeUseSkill1;
    public Vector2 vectorToPlayer;

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
    [Header("Skill disorder")]
    public Sprite iconStatus;
    public string statusNameKey;
    public string statusDescriptionKey;
    public float duration;
    public int chanceSkill3;
    public int defaultChanceSkill3;
    public float cooldownSkill3;
    public float lastTimeUseSkill3;


    


    
    public BossForestIdleState idleState { get; private set; }
    public BossForestMoveState moveState { get; private set; }
    public BossForestDeathState deathState { get; private set; }
    public BossForestChasingState chasingState { get; private set; }
    public BossForestBattleState battleState { get; private set; }
    public BossForestJumpState jumpState { get; private set; }
    // public BossForestAirState airState { get; private set; }
    public BossForestUncoilState uncoilState { get; private set; }
    public BossForestAttackState attackState { get; private set; }
    public BossForestAttackState1 attackState1 { get; private set; }
    public BossForestAttackState2 attackState2 { get; private set; }


    protected override void Awake()
    {
        base.Awake();
        idleState = new BossForestIdleState(this, stateMachine, "Idle", this);
        moveState = new BossForestMoveState(this, stateMachine, "Move", this);
        deathState = new BossForestDeathState(this, stateMachine, "Death", this);
        chasingState = new BossForestChasingState(this, stateMachine, "Move", this);
        battleState = new BossForestBattleState(this, stateMachine, "Idle", this);
        jumpState = new BossForestJumpState(this, stateMachine, "Jump", this);
        // airState = new BossForestAirState(this, stateMachine, "Air", this);
        uncoilState = new BossForestUncoilState(this, stateMachine, "Uncoil", this);
        attackState = new BossForestAttackState(this, stateMachine, "Attack", this);
        attackState1 = new BossForestAttackState1(this, stateMachine, "Attack 1", this);
        attackState2 = new BossForestAttackState2(this, stateMachine, "Attack 2", this);

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

    public bool CanUseBossForestSkill1(){
        if(Random.Range(0,100)<=chanceSkill1){
            chanceSkill1=defaultChanceSkill1;
            return true;
        }
        chanceSkill1+=5;
        return false;
    }
    public bool CanUseBossForestSkill2(){
        if(Random.Range(0,100)<=chanceSkill2){
            chanceSkill2=defaultChanceSkill2;
            return true;
        }
        chanceSkill2+=5;
        return false;
    }
    public bool CanUseBossForestSkill3(){
        if(Random.Range(0,100)<=chanceSkill3){
            chanceSkill3=defaultChanceSkill3;
            return true;
        }
        chanceSkill3+=5;
        return false;
    }

    

    public override void Attack1()
    {
        attackObject.transform.localScale=new Vector2(attackSize,attackSize);
        EnemyCreateAreaAttack(attackObject,StatType.strength,charaterStats.strength.GetValue(),attackDistance,angleToPlayer,0);
    }
    public override void Attack2()
    {
        stateMachine.ChangeState(attackState1);
        int damage=Mathf.RoundToInt(charaterStats.strength.GetValue()*skill1DamagePerCent);
        Debug.Log("damage: "+damage);
        GameObject newBossForestSkill1=Instantiate(skillObject1,attackCheck.position,Quaternion.identity,transform);
        newBossForestSkill1.GetComponent<BossForestSkillAttack2>().SetupSkill(damage,this);
    }
    public override void Attack3()
    {
        JumpCoroutine=JumpRoutine(transform.position,player.transform.position);
        StartCoroutine(JumpCoroutine);
        
    }
    public void ApplyReverseControlsEffect(){
        player.charaterStats.SetUpReverseControlsEffect(duration);
        UI.instance.ingameUI.CreateStatus(iconStatus,statusNameKey,statusDescriptionKey,duration);
    }


    IEnumerator JumpRoutine(Vector3 startPosition,Vector3 endPosition){
        FlipController(player.transform.position.x-transform.position.x);
        stateMachine.ChangeState(jumpState);
        charaterStats.MakeInvinsable(true);
        GameObject newBossForestJumpEffect=Instantiate(skillObject2,endPosition,Quaternion.identity);
        BossForestSkillAttack3 bossForestSkill3=newBossForestJumpEffect.GetComponent<BossForestSkillAttack3>();
        float timePassed=0f;
        while(timePassed<skillDuration){
            timePassed+=Time.deltaTime;
            float linearT=timePassed/skillDuration;
            float heightT=animationCurve.Evaluate(linearT);
            float height=Mathf.Lerp(0f,skillHeight,heightT);

            transform.position=Vector2.Lerp(startPosition,endPosition,linearT)+new Vector2(0f,height);


            yield return null;
        }
        bossForestSkill3.SetupSkill(Mathf.RoundToInt(charaterStats.strength.GetValue()*skill2DamagePerCent),charaterStats);
        bossForestSkill3.TriggerSkill();
        charaterStats.MakeInvinsable(false);
        stateMachine.ChangeState(attackState2);
        StopCoroutine(JumpCoroutine);
        
    }
}
