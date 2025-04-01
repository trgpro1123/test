using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlinkBladeState : PlayerState
{


    public PlayerBlinkBladeState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer=player.skillManager.blinkBladeSkill.timeUseSkill;
    }

    public override void Exit()
    {
        base.Exit();
              
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer<=0) stateMachine.ChangeState(player.idleState);
        player.ZeroVelocity();
    }
}
