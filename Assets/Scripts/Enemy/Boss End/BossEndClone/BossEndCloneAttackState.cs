using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEndCloneAttackState : EnemyState
{
    BossEndClone enemy;
    public BossEndCloneAttackState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,BossEndClone _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
        enemy.lastTimeAttack=Time.time;
    }
    public override void Update()
    {
        base.Update();
        enemy.ZeroVelocity();
        if(triggerCalled) stateMachine.ChangeState(enemy.battleState);
    }
}
