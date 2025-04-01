using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWinterAttackState3 : EnemyState
{
    Enemy_BossWinter enemy;
    public BossWinterAttackState3(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_BossWinter _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
        enemy.lastTimeUseSkill3=Time.time;
    }
    public override void Update()
    {
        base.Update();
        enemy.ZeroVelocity();
        if(triggerCalled) stateMachine.ChangeState(enemy.battleState);
    }
}
