using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;
    protected string animateBoolName;
    protected Rigidbody2D rb;
    protected bool triggerCalled;
    protected float stateTimer;
    protected Player player;

    public EnemyState(Enemy _enemyBase,EnemyStateMachine _enemyStateMachine,string _animateBoolName)
    {
        stateMachine=_enemyStateMachine;
        this.enemyBase=_enemyBase;
        animateBoolName=_animateBoolName;
    }

    public virtual void Update(){
        if(enemyBase.charaterStats.isForzerTime) return;
        stateTimer-=Time.deltaTime;

    }
    public virtual void Enter(){
        enemyBase.animator.SetBool(animateBoolName,true);
        rb=enemyBase.rb;
        triggerCalled=false;
        player=PlayerManager.instance.player;
    }
    public virtual void Exit(){
        enemyBase.animator.SetBool(animateBoolName,false);
        enemyBase.AssignLastAnimBoolName(animateBoolName);
    }
    public void AnimationFininshTrigger(){
        triggerCalled=true;
    }
}
