using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeDeathState : EnemyState
{
    public Enemy_FlyEye enemy;
    public FlyEyeDeathState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_FlyEye _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.ZeroVelocity();
        enemy.cd.enabled=false;
        enemy.animationFinishTrigger.CloseAttackArea();
        enemy.animationFinishTrigger.CloseAttackHitBox();
        enemy.GetComponentInChildren<SpriteRenderer>().color=Color.gray;
        enemy.GetComponentInChildren<CircleCollider2D>().enabled=false;
        enemy.GetComponent<CapsuleCollider2D>().enabled=false;
        enemy.transform.GetComponentInChildren<HealthBar_UI>().gameObject.SetActive(false);
    }
}
