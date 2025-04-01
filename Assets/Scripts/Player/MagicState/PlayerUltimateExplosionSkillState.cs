using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateExplosionSkillState : PlayerState
{

    
    public PlayerUltimateExplosionSkillState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        stateTimer=skillManager.ultimateExplosionSkill.duration;
        
    }

    public override void Exit()
    {
        base.Exit();
        
        
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer<=0){
            player.skillManager.ultimateExplosionSkill.CreateUltimateExplosion();
            stateMachine.ChangeState(player.idleState);
        }

        

    }
}
