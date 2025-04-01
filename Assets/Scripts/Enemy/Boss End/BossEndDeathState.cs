using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEndDeathState : EnemyState
{
    public Enemy_BossEnd enemy;
    public BossEndDeathState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_BossEnd _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
        stateTimer=0.2f;
        
    }
    public override void Update()
    {
        base.Update();
        player.isBusy=true;
    }
}
