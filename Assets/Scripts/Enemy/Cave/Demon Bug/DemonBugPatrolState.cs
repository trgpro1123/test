using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBugPatrolState : EnemyState
{

    protected Enemy_DemonBug enemy;
    

    public DemonBugPatrolState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_DemonBug _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
