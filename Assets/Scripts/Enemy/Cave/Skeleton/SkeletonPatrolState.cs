using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPatrolState : EnemyState
{

    protected Enemy_Skeleton enemy;
    

    public SkeletonPatrolState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_Skeleton _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }
    public override void Enter()
    {
        base.Enter();

    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
    }
}
