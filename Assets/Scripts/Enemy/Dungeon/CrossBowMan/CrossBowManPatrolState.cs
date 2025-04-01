using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowManPatrolState : EnemyState
{

    protected Enemy_CrossBowMan enemy;
    

    public CrossBowManPatrolState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_CrossBowMan _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
