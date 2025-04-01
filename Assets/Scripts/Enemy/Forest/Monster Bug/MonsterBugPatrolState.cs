using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBugPatrolState : EnemyState
{

    protected Enemy_MonsterBug enemy;
    

    public MonsterBugPatrolState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_MonsterBug _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
