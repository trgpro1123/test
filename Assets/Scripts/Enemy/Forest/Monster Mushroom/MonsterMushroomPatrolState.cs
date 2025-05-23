using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMushroomPatrolState : EnemyState
{

    protected Enemy_MonsterMushroom enemy;
    

    public MonsterMushroomPatrolState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_MonsterMushroom _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
