using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThousandCutsState : PlayerState
{


    public PlayerThousandCutsState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.playerAnimationTrigger.OnAttackTrigger += player.skillManager.thousandCutsSkill.UseSkill;
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine(player.Busy(0.15f));



        
    }

    public override void Update()
    {
        base.Update();
        if(triggerCalled) stateMachine.ChangeState(player.idleState);
    }

}
