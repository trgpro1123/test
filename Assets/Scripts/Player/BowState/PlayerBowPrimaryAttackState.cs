using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowPrimaryAttackState : PlayerState
{
    public PlayerBowPrimaryAttackState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Bow");
        

    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine(player.Busy(0.15f));
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer<0) player.SetRigidbody(0,0);
        if(triggerCalled) stateMachine.ChangeState(player.idleState);
    }
}
