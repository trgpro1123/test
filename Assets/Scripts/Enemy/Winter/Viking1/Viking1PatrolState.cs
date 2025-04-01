using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking1PatrolState : EnemyState
{

    protected Enemy_Viking1 enemy;
    

    public Viking1PatrolState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_Viking1 _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
