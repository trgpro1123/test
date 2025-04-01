using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MonsterBugFly : Enemy
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

    
    public MonsterBugFlyIdleState idleState { get; private set; }
    public MonsterBugFlyMoveState moveState { get; private set; }
    public MonsterBugFlyAttackState attackState { get; private set; }
    public MonsterBugFlyDeathState deathState { get; private set; }
    public MonsterBugFlyChasingState chasingState { get; private set; }
    public MonsterBugFlyBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new MonsterBugFlyIdleState(this, stateMachine, "Idle", this);
        moveState = new MonsterBugFlyMoveState(this, stateMachine, "Move", this);
        attackState = new MonsterBugFlyAttackState(this, stateMachine, "Attack", this);
        deathState = new MonsterBugFlyDeathState(this, stateMachine, "Death", this);
        chasingState = new MonsterBugFlyChasingState(this, stateMachine, "Move", this);
        battleState = new MonsterBugFlyBattleState(this, stateMachine, "Idle", this);
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
        newProjectile.GetComponent<MonsterBugFlyProjectile>().SetupProjectile(charaterStats.strength.GetValue(),speed,lifeTime,iconStatus,statusNameKey,statusDescriptionKey,duration,charaterStats);
    }
    public void DestroySeflIn(float _time){
        Destroy(gameObject,_time);
    }

}
