using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTreePatroldState : EnemyState
{

    protected Enemy_MonsterTree enemy;
    

    public MonsterTreePatroldState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_MonsterTree _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
