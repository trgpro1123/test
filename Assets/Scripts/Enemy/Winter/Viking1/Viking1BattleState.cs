using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking1BattleState : EnemyState
{
    protected Enemy_Viking1 enemy;

    private bool canAttackPlayer;

    public Viking1BattleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_Viking1 _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.navMeshAgent.enabled=false;

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
                if(enemy.CanUseViking1Skill()){
                    stateMachine.ChangeState(enemy.attackState2);
                }
                else{
                    stateMachine.ChangeState(enemy.attackState1);
                }
            }
            if(enemy.IsPlayerDetected()==false){
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
