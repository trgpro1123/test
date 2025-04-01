using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWinterIdleState : BossWinterPatrolState
{
    public BossWinterIdleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName, Enemy_BossWinter _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName, _enemy)
    {
    }
    public override void Enter()
    {
        base.Enter();
        enemy.ZeroVelocity();
        stateTimer=enemy.idleTime;
        if(enemy.isWaiting==true){
            enemyBase.animator.SetBool("Idle", true);
        }

        
    }
    public override void Exit()
    {
        base.Exit();
        
       // AudioManager.instance.PlaySFX(24,PlayerManage.instance.transform);
    }
    public override void Update()
    {
        base.Update();
        if(enemy.isWaiting==true){
            enemyBase.animator.SetBool("Idle", true);
            return;
        }

        if(stateTimer<0){
            stateMachine.ChangeState(enemy.moveState);
        }
        if(enemy.IsPlayerDetected()){
            stateMachine.ChangeState(enemy.chasingState);
        }


    }
}
