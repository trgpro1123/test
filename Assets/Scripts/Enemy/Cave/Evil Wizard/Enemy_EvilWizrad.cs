using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_EvilWizrad : Enemy
{
    [Header("Projectile Properties")]
    public GameObject projectile;
    public float speed;
    public float lifeTime;

    
    public EvilWizradIdleState idleState { get; private set; }
    public EvilWizradMoveState moveState { get; private set; }
    public EvilWizradAttackState attackState { get; private set; }
    public EvilWizradDeathState deathState { get; private set; }
    public EvilWizradChasingState chasingState { get; private set; }
    public EvilWizradBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new EvilWizradIdleState(this, stateMachine, "Idle", this);
        moveState = new EvilWizradMoveState(this, stateMachine, "Move", this);
        attackState = new EvilWizradAttackState(this, stateMachine, "Attack", this);
        deathState = new EvilWizradDeathState(this, stateMachine, "Death", this);
        chasingState = new EvilWizradChasingState(this, stateMachine, "Move", this);
        battleState = new EvilWizradBattleState(this, stateMachine, "Idle", this);
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
        newProjectile.GetComponent<EvilWizradProjectile>().SetUpProjectile(charaterStats.magicPower.GetValue(),speed,lifeTime,charaterStats);
    }
    public void DestroySeflIn(float _time){
        Destroy(gameObject,_time);
    }
}
