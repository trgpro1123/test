using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizradPatrolState : EnemyState
{

    protected Enemy_EvilWizrad enemy;
    

    public EvilWizradPatrolState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_EvilWizrad _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
