using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGauntletSkillAttackState : PlayerState
{
    public System.Action OnCurrentSkill;
    public PlayerGauntletSkillAttackState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.playerAnimationTrigger.OnAttackTrigger += OnCurrentSkill;
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine(player.Busy(0.15f));
        player.playerAnimationTrigger.OnAttackTrigger=null;
        OnCurrentSkill=null;
        
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer<0) player.SetRigidbody(0,0);
        if(triggerCalled) stateMachine.ChangeState(player.idleState);
    }
    public void SetCurrentSkill(System.Action _currentSkill){
        OnCurrentSkill=_currentSkill;
    }
}
