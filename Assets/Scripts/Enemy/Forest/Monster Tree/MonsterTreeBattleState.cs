using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTreeBattleState : EnemyState
{
    protected Enemy_MonsterTree enemy;

    private bool canAttackPlayer;

    public MonsterTreeBattleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_MonsterTree _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.navMeshAgent.enabled=false;
        //stateTimer=enemy.enemyAIPathfinding.aiUpdateDelay;

    }

    public override void Exit()
    {
        base.Exit();
        enemy.ZeroVelocity();
        enemy.navMeshAgent.enabled=true;
    }


    public override void Update()
    {
        base.Update();
        if(enemy.charaterStats.isForzerTime) return;
        
        enemy.FlipController(player.transform.position.x-enemy.transform.position.x);
        if(Vector2.Distance(enemy.transform.position,player.transform.position) <= enemy.attackDistance){
            if(CanAttack()&&enemy.IsPlayerDetected()){

                stateMachine.ChangeState(enemy.attackState);
            }
            if(enemy.IsPlayerDetected()==false){
                Debug.Log("Change to chase");
                stateMachine.ChangeState(enemy.chasingState);
            }
        }
        else{
            if(enemy.playerDetectTimer<=0){
                stateMachine.ChangeState(enemy.idleState);
            }
            else{
                stateMachine.ChangeState(enemy.chasingState);
            }
        }


        // if(enemy.IsPlayerDetected()){
        //     if(CanAttack()){

        //             stateMachine.ChangeState(enemy.attackState);
        //         }
        //     if(Vector2.Distance(enemy.transform.position,player.transform.position) <= enemy.attackDistance){

        //         // if(CanAttack()){

        //         //     stateMachine.ChangeState(enemy.attackState);
        //         // }
        //         // else{
        //         //     stateMachine.ChangeState(enemy.idleState);
        //         // }
        //     }
        // }
        // else{
        //     stateMachine.ChangeState(enemy.moveState);

        // }
        // if(Vector2.Distance(enemy.transform.position,player.transform.position) >enemy.distanceDetect){
        // }

        
    }
    public bool CanAttack(){
        if(Time.time>=enemy.lastTimeAttack+enemy.attackCoolDown){
            enemy.lastTimeAttack=Time.time;
            enemy.attackCoolDown=Random.Range(enemy.minAttackCoolDown,enemy.maxAttackCoolDown);
            return true;
        }
        return false;
    }
}
