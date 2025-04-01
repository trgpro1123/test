using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected SkillManager skillManager;
    protected string animateBoolName;
    protected Rigidbody2D rb;
    protected float xInput;
    protected float yInput;
    protected float stateTimer;
    protected bool triggerCalled;

    public PlayerState(Player _player,PlayerStateMachine _playerStateMachine,string _animateBoolName){
        player=_player;
        stateMachine=_playerStateMachine;
        animateBoolName=_animateBoolName;
        skillManager=player.skillManager;
    }
    public virtual void Update(){
        stateTimer-=Time.deltaTime;
        xInput=Input.GetAxisRaw("Horizontal");
        yInput=Input.GetAxisRaw("Vertical");
        if(player.charaterStats.isReverseControls){
            xInput*=-1;
            yInput*=-1;
        }
    }
    public virtual void Enter(){
        player.animator.SetBool(animateBoolName,true);
        rb=player.rb;
        triggerCalled=false;
    }
    public virtual void Exit(){
        player.animator.SetBool(animateBoolName,false);
    }
    public void AnimationFininshTrigger(){
        triggerCalled=true;
    }
}
