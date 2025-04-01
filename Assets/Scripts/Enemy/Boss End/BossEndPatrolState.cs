using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEndPatrolState : EnemyState
{

    protected Enemy_BossEnd enemy;
    

    public BossEndPatrolState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_BossEnd _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
