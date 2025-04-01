using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerFreeState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.canUseSkill=false;
        
        
    }

    public override void Exit()
    {
        base.Exit();
        player.canUseSkill=true;

    }

    public override void Update()
    {
        base.Update();
        if(player.charaterStats.isForzerTime){
            player.SetRigidbody(0,0);
            return;
        }
        if(player.isBusy) return;
        Vector2 moveDir=new Vector2(xInput,yInput).normalized;
        player.SetRigidbody(moveDir.x*player.charaterStats.moveSpeed.GetValue(),moveDir.y*player.charaterStats.moveSpeed.GetValue());
        if(xInput==0&&yInput==0) stateMachine.ChangeState(player.idleState); 

    }
}
