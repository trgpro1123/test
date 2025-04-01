using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMoveState : GoblinPatrolState
{
    // Transform destination;

    public GoblinMoveState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName, Enemy_Goblin _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName, _enemy)
    {
    }
    public override void Enter()
    {
        base.Enter();
        if(enemy.charaterStats.isForzerTime) return;
        enemy.CreateDestination(enemy.RandomPosition());
        enemy.navMeshAgent.stoppingDistance=0.3f;
    }
    public override void Exit()
    {
        base.Exit();

        enemy.ZeroVelocity();
        enemy.navMeshAgent.stoppingDistance=0;
        enemy.DestroyDestination();

    }
    public override void Update()
    {
        base.Update();
        if(enemy.charaterStats.isForzerTime) return;
        enemy.FlipController(enemy.destination.position.x-enemy.transform.position.x);
        enemy.MoveToDestination(enemy.destination.position);
        
        if(Vector2.Distance(enemy.attackCheck.position,enemy.destination.position)<0.3f){
            stateMachine.ChangeState(enemy.idleState);
            
        }
        if(enemy.IsPlayerDetected()){
            stateMachine.ChangeState(enemy.chasingState);
        }

    }

    
}
