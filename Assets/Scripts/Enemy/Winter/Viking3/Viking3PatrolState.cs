using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking3PatrolState : EnemyState
{

    protected Enemy_Viking3 enemy;
    

    public Viking3PatrolState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_Viking3 _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
