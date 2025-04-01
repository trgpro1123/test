using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossForestAttackState1 : EnemyState
{
    Enemy_BossForest enemy;
    public BossForestAttackState1(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_BossForest _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }


    public override void Enter()
    {
        // enemy.animationFinishTrigger.SetAttackArea(enemy.attackSize1,enemy.attackDistance1);
        base.Enter();
        stateTimer=enemy.skillAttackDelay;
        enemy.navMeshAgent.enabled=false;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeUseSkill1=Time.time;
        enemy.navMeshAgent.enabled=true;
    }
    public override void Update()
    {
        base.Update();
        if(stateTimer<=0){
            enemy.rb.velocity=enemy.vectorToPlayer*enemy.speed;
            enemy.CloseAttackArea();
        }
        
    }
}
