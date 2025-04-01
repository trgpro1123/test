using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCaveBattleState : EnemyState
{
    protected Enemy_BossCave enemy;

    private bool canAttackPlayer;

    public BossCaveBattleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_BossCave _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
        if(enemy.isBuff==false&&enemy.charaterStats.currentHealth<=enemy.charaterStats.GetMaxHealth()*enemy.percentHealthToUseSkill){
            enemy.isBuff=true;
            enemy.stateMachine.ChangeState(enemy.attackState3);
            
        }

        
        enemy.FlipController(player.transform.position.x-enemy.transform.position.x);
        if(Vector2.Distance(enemy.transform.position,player.transform.position) <= enemy.attackDistance){
            if(CanAttack()&&enemy.IsPlayerDetected()){
                if(CanUseSkill1()&&enemy.CanUseBossCaveSkill1()){
                    stateMachine.ChangeState(enemy.attackState1);
                }
                else if(CanUseSkill2()&&enemy.CanUseBossCaveSkill2()){
                    // stateMachine.ChangeState(enemy.attackState2);
                    stateMachine.ChangeState(enemy.prepareAttackState2);
                }
                else {
                    stateMachine.ChangeState(enemy.attackState);
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
    public bool CanUseSkill1(){
        if(Time.time>=enemy.lastTimeUseSkill1+enemy.cooldownSkill1){
            enemy.lastTimeUseSkill1=Time.time;
            return true;
        }
        return false;
    }
    public bool CanUseSkill2(){
        if(Time.time>=enemy.lastTimeUseSkill2+enemy.cooldownSkill2){
            enemy.lastTimeUseSkill2=Time.time;
            return true;
        }
        return false;
    }
}
