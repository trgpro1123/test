using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossForestAttackState : EnemyState
{
    Enemy_BossForest enemy;
    public BossForestAttackState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_BossForest _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }


    public override void Enter()
    {
        // enemy.animationFinishTrigger.SetAttackArea(enemy.attackSize1,enemy.attackDistance1);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeUseSkill1=Time.time;
    }
    public override void Update()
    {
        base.Update();
        enemy.ZeroVelocity();
        if(triggerCalled) stateMachine.ChangeState(enemy.battleState);
    }
}
