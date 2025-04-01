using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollingState : PlayerState
{
    
    public PlayerRollingState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetIsRolling(true);
        player.isBusy=true;
        player.charaterStats.isImmortal=true;
        player.DisableEnemyPlayerCollision();
 
    }

    public override void Exit()
    {
        base.Exit();
        player.SetIsRolling(false);
        player.isBusy=false;
        player.charaterStats.isImmortal=false;
        player.EnableEnemyPlayerCollision();
    }

    public override void Update()
    {
        base.Update();
        if(player.charaterStats.isForzerTime){
            player.SetRigidbody(0,0);
            return;
        }
        player.SetRigidbody((player.charaterStats.moveSpeed.GetValue()*2)*player.rollDir.x,(player.charaterStats.moveSpeed.GetValue()*2)*player.rollDir.y);

        if(triggerCalled) stateMachine.ChangeState(player.idleState);

    }
}
