using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Wizard : Enemy
{
    [Header("Projectile Properties")]
    public GameObject projectile;
    public float speed;
    public float lifeTime;
    [Header("Status Effect")]
    public Sprite iconStatus;
    public string statusNameKey;
    public string statusDescriptionKey;
    public float duration;

    
    public WizardIdleState idleState { get; private set; }
    public WizardMoveState moveState { get; private set; }
    public WizardAttackState attackState { get; private set; }
    public WizardDeathState deathState { get; private set; }
    public WizardChasingState chasingState { get; private set; }
    public WizardBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new WizardIdleState(this, stateMachine, "Idle", this);
        moveState = new WizardMoveState(this, stateMachine, "Move", this);
        attackState = new WizardAttackState(this, stateMachine, "Attack", this);
        deathState = new WizardDeathState(this, stateMachine, "Death", this);
        chasingState = new WizardChasingState(this, stateMachine, "Move", this);
        battleState = new WizardBattleState(this, stateMachine, "Idle", this);
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
        GameObject newProjectile = Instantiate(projectile, attackCheck.transform.position,Quaternion.Euler(0, 0, angleToPlayer ));
        newProjectile.GetComponent<WizardProjectile>().SetupProjectile(charaterStats.magicPower.GetValue(),speed,lifeTime,iconStatus,statusNameKey,statusDescriptionKey,duration,charaterStats);
    }
    public void DestroySeflIn(float _time){
        Destroy(gameObject,_time);
    }
}
