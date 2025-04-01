using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreUseSkillState : PlayerState
{
    public System.Action OnCurrentSkill;
    public PlayerPreUseSkillState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
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

        if(triggerCalled) stateMachine.ChangeState(player.useSkillState);

    }
    public void SetCurrentSkill(System.Action _currentSkill){
        OnCurrentSkill=_currentSkill;
    }
}
