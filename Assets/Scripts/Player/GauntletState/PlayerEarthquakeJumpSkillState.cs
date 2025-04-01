using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEarthquakeJumpSkillState : PlayerState
{

    Vector2 positionToMove;
    Vector3 mousePosition;
    public PlayerEarthquakeJumpSkillState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        mousePosition=player.mousePosition;
        stateTimer = player.skillManager.earthquakeJumpSkill.durationJump;
        positionToMove=(mousePosition-player.transform.position).normalized;
    }

    public override void Exit()
    {
        base.Exit();

        
    }

    public override void Update()
    {
        base.Update();
        player.SetRigidbody(skillManager.earthquakeJumpSkill.moveSpeed*positionToMove.x,
            skillManager.earthquakeJumpSkill.moveSpeed*positionToMove.y);
        if(stateTimer<0||Vector2.Distance(player.transform.position,mousePosition)<0.1f){
            player.SetRigidbody(0,0);
            player.gauntletSkillAttackState.SetCurrentSkill(skillManager.earthquakeJumpSkill.UseSkill);
            player.stateMachine.ChangeState(player.gauntletSkillAttackState);
        }

    }
}
