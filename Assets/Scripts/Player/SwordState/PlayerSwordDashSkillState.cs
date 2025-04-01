using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordDashSkillState : PlayerState
{
    Vector2 positionToMove;
    public PlayerSwordDashSkillState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer=player.skillManager.swordDashSkill.swiftDashTime;
        positionToMove=(player.mousePosition-player.transform.position).normalized;
        player.DisableEnemyPlayerCollision();


    }

    public override void Exit()
    {
        base.Exit();
        player.EnableEnemyPlayerCollision();

    }

    public override void Update()
    {
        base.Update();
        player.SetRigidbody(skillManager.swordDashSkill.swiftDashSpeed*positionToMove.x,
            skillManager.swordDashSkill.swiftDashSpeed*positionToMove.y);

        if(stateTimer<=0) stateMachine.ChangeState(player.idleState);

    }
}
