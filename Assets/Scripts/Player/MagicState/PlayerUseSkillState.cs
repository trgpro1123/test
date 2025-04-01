using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseSkillState : PlayerState
{

    
    public PlayerUseSkillState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        
    }

    public override void Exit()
    {
        base.Exit();
        
        
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer<=0){
            stateMachine.ChangeState(player.idleState);
        }

        

    }

    public void SetTimeUseSkill(float _time){
        stateTimer=_time;
    }
    
}
