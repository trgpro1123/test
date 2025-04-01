using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlurryOfArrowsSkillState : PlayerState
{


    public PlayerFlurryOfArrowsSkillState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.playerAnimationTrigger.OnAttackTrigger += player.skillManager.flurryOfArrowsSkill.UseSkill;
        stateTimer=player.skillManager.flurryOfArrowsSkill.skillDuration;
        AudioManager.instance.PlaySFX(7);

    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine(player.Busy(0.15f));
        player.playerAnimationTrigger.OnAttackTrigger=null;
        AudioManager.instance.StopSFX(7);

        
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer<0)
            stateMachine.ChangeState(player.idleState);

        
    }
}
