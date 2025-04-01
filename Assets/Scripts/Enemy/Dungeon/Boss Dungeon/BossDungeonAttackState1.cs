using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDungeonAttackState1 : EnemyState
{
    Enemy_BossDungeon enemy;
    public BossDungeonAttackState1(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_BossDungeon _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
    }
    public override void Update()
    {
        base.Update();
        enemy.ZeroVelocity();
        if(triggerCalled) stateMachine.ChangeState(enemy.battleState);
    }
}
