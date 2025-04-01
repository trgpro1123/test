using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChaosSlashSkillAttackState : PlayerState
{


    public PlayerChaosSlashSkillAttackState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer=player.skillManager.chaosSlashesSkill.duration;
        SkillManager.instance.chaosSlashesSkill.CreateChaosSlashAnimation();
        AudioManager.instance.PlaySFX(0);

    }

    public override void Exit()
    {
        base.Exit();
        stateTimer=0;
        player.StartCoroutine(player.Busy(0.15f));
        AudioManager.instance.StopSFX(0);


        
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer<=0) player.SetRigidbody(0,0);
        if(stateTimer<=0) stateMachine.ChangeState(player.idleState);
    }
}
