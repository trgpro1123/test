using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_VikingMage : Enemy
{

    [Header("Viking Mage Skill 1")]
    public GameObject skillObject1;
    public float skillDamagePerCent1;
    
    [Header("Viking Mage Skill 2")]
    public GameObject skillObject2;
    public float skillDamagePerCent2;
    public float speed;
    public float lifeTime;


    
    public VikingMageIdleState idleState { get; private set; }
    public VikingMageMoveState moveState { get; private set; }
    public VikingMageAttackState1 attackState1 { get; private set; }
    public VikingMageAttackState2 attackState2 { get; private set; }
    public VikingMageDeathState deathState { get; private set; }
    public VikingMageChasingState chasingState { get; private set; }
    public VikingMageBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new VikingMageIdleState(this, stateMachine, "Idle", this);
        moveState = new VikingMageMoveState(this, stateMachine, "Move", this);
        attackState1 = new VikingMageAttackState1(this, stateMachine, "Attack 1", this);
        attackState2 = new VikingMageAttackState2(this, stateMachine, "Attack 2", this);
        deathState = new VikingMageDeathState(this, stateMachine, "Death", this);
        chasingState = new VikingMageChasingState(this, stateMachine, "Move", this);
        battleState = new VikingMageBattleState(this, stateMachine, "Idle", this);
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
        Player player=PlayerManager.instance.player;
        int damage=Mathf.RoundToInt(charaterStats.magicPower.GetValue()*skillDamagePerCent1);
        GameObject newSkill1=Instantiate(skillObject1,player.transform.position+new Vector3(0,-1,0),Quaternion.identity);
        newSkill1.GetComponent<VikingMage_Skill1>().SetupSkill(damage,charaterStats);
    }
    public override void Attack2()
    {
        int damage=Mathf.RoundToInt(charaterStats.magicPower.GetValue()*skillDamagePerCent2);
        GameObject newSkill2=Instantiate(skillObject2,attackCheck.transform.position,Quaternion.Euler(0, 0, angleToPlayer ));
        newSkill2.GetComponent<VikingMage_Skill2>().SetupSkill(damage,speed,lifeTime,charaterStats);
    }
}
