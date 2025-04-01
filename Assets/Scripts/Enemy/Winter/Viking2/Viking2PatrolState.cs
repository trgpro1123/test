using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking2PatrolState : EnemyState
{

    protected Enemy_Viking2 enemy;
    

    public Viking2PatrolState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_Viking2 _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
