using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSavageBarrageSkillState : PlayerState
{


    public PlayerSavageBarrageSkillState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        SkillManager.instance.savageBarrageSkill.CreateSavageBarrageAnimation();

    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine(player.Busy(0.15f));
        SkillManager.instance.savageBarrageSkill.DestroySavageBarrage();


        
    }

    public override void Update()
    {
        base.Update();
        if(SkillManager.instance.savageBarrageSkill.SkillCompleted()){
            SkillManager.instance.savageBarrageSkill.DestroySavageBarrage();
            stateMachine.ChangeState(player.idleState);

        }
    }
}
