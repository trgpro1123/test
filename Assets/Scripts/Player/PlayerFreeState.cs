using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeState : PlayerState
{
    public PlayerFreeState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }


    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(player.charaterStats.isForzerTime||player.isBusy||player.canUseSkill==false) return;
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            skillManager.listSkills[0]?.ActiveSkill();
        } 
        else if(Input.GetKeyDown(KeyCode.Mouse1)){
            skillManager.listSkills[1]?.ActiveSkill();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1)){
            skillManager.listSkills[2]?.ActiveSkill();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)){
            skillManager.listSkills[3]?.ActiveSkill();
            
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3)){
            skillManager.listSkills[4]?.ActiveSkill();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4)){
            skillManager.listSkills[5]?.ActiveSkill();
                
        }

        
    }
}
