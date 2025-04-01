using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDungeonChasingState : EnemyState
{

    protected Enemy_BossDungeon enemy;
    protected Transform transformToMove;
    

    public BossDungeonChasingState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_BossDungeon _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer=enemy.timeChasing;
        transformToMove=PlayerManager.instance.player.transform;

    }
    public override void Exit()
    {
        base.Exit();
        
    }
    public override void Update()
    {
        base.Update();
        if(enemy.charaterStats.isForzerTime) return;
        if(stateTimer<=0){
            transformToMove=enemy.transform;
            enemy.NavMeshAgentStopByTime(1f);
            stateMachine.ChangeState(enemy.idleState);
        }
        if(Vector2.Distance(enemy.transform.position,transformToMove.transform.position) <= enemy.attackDistance&&enemy.IsPlayerDetected()==true){
            stateTimer=enemy.timeChasing;
            transformToMove=enemy.transform;
            enemy.NavMeshAgentStopByTime(1f);
            stateMachine.ChangeState(enemy.battleState);
        }
        enemy.MoveToDestination(transformToMove.position);
        enemy.FlipController(transformToMove.position.x-enemy.transform.position.x);

    }
}
