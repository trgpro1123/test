using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAndHammerWarriorAttackState1 : EnemyState
{
    Enemy_SwordAndHammerWarrior enemy;
    public SwordAndHammerWarriorAttackState1(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_SwordAndHammerWarrior _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
