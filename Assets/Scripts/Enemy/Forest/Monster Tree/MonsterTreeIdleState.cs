using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTreeIdleState : MonsterTreePatroldState
{
    public MonsterTreeIdleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName, Enemy_MonsterTree _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName, _enemy)
    {
    }
    public override void Enter()
    {
        base.Enter();
        enemy.ZeroVelocity();
        stateTimer=enemy.idleTime;
        if(enemy.isWaiting) {
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
        // if(enemy.playerDetectTimer<=0){
        //     stateMachine.ChangeState(enemy.idleState);
        // }
        // else{
        //     stateMachine.ChangeState(enemy.chasingState);
        // }

    }

}
