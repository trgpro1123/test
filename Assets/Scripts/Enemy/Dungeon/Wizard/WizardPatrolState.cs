using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardPatrolState : EnemyState
{

    protected Enemy_Wizard enemy;
    

    public WizardPatrolState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_Wizard _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
