using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSeismicShockwaveSkillState : PlayerState
{

    Vector2 positionToMove;
    Vector3 mousePosition;
    public PlayerSeismicShockwaveSkillState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        mousePosition=player.mousePosition;
        stateTimer = player.skillManager.seismicShockwaveSkill.durationJump;
        positionToMove=(mousePosition-player.transform.position).normalized;
    }

    public override void Exit()
    {
        base.Exit();


        
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer<0||Vector2.Distance(player.transform.position,mousePosition)<0.1f){
            player.SetRigidbody(0,0);
            player.gauntletSkillAttackState.SetCurrentSkill(skillManager.seismicShockwaveSkill.UseSkill);
            player.stateMachine.ChangeState(player.gauntletSkillAttackState);
        }
        // player.transform.position=Vector2.MoveTowards(player.transform.position,mousePosition,player.skillManager.earthquakeJumpSkill.moveSpeed*Time.deltaTime);
        player.SetRigidbody(skillManager.seismicShockwaveSkill.moveSpeed*positionToMove.x,
            skillManager.seismicShockwaveSkill.moveSpeed*positionToMove.y);

    }
}
