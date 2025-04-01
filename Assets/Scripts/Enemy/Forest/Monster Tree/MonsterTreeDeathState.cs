using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTreeDeathState : EnemyState
{
    public Enemy enemy;
    public MonsterTreeDeathState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.ZeroVelocity();
        // enemy.animator.SetBool(enemy.lastAnimBoolName,true);
        // enemy.animator.speed=0;
        // enemy.GetComponentInChildren<Dissolve>().StartCoroutine("Vanish");
        enemy.cd.enabled=false;
        enemy.animationFinishTrigger.CloseAttackArea();
        enemy.animationFinishTrigger.CloseAttackHitBox();
        enemy.GetComponentInChildren<SpriteRenderer>().color=Color.gray;
        enemy.GetComponentInChildren<CircleCollider2D>().enabled=false;
        enemy.GetComponent<CapsuleCollider2D>().enabled=false;
        enemy.transform.GetComponentInChildren<HealthBar_UI>().gameObject.SetActive(false);
        stateTimer=0.2f;
        
    }
}
