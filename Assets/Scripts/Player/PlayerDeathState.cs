using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
       player.ZeroVelocity();
    }


    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.ZeroVelocity();
        if((xInput!=0||yInput!=0)&&!player.isBusy) stateMachine.ChangeState(player.moveState);
    }
}
