using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAndHammerWarriorBattleState : EnemyState
{
    protected Enemy_SwordAndHammerWarrior enemy;

    private bool canAttackPlayer;

    public SwordAndHammerWarriorBattleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_SwordAndHammerWarrior _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
                if(Random.Range(0,2)==0){
                    stateMachine.ChangeState(enemy.attackState1);
                }
                else{
                    stateMachine.ChangeState(enemy.attackState2);
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
