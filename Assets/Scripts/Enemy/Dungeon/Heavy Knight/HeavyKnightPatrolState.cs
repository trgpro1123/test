using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyKnightPatrolState : EnemyState
{

    protected Enemy_HeavyKnight enemy;
    

    public HeavyKnightPatrolState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_HeavyKnight _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
