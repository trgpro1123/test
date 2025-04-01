using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossForestAttackState2 : EnemyState
{
    Enemy_BossForest enemy;
    public BossForestAttackState2(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_BossForest _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
        enemy.lastTimeUseSkill2=Time.time;
    }
    public override void Update()
    {
        base.Update();
        if(triggerCalled) stateMachine.ChangeState(enemy.battleState);
    }
}
