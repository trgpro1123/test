using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGauntletPrimaryAttackState : PlayerState
{

    public int comboCounter;
    private float lastTimeAttack;
    private float comboWindow=2f;
    public System.Action OnCurrentSkill;

    public PlayerGauntletPrimaryAttackState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        xInput=0;
        if(comboCounter>2||Time.time>lastTimeAttack+comboWindow) comboCounter=0;
        player.animator.SetInteger("ComboCounter",comboCounter);
        float attackDir=player.facingDir;
        if(xInput!=0) attackDir=xInput;
        stateTimer=.1f;
        player.playerAnimationTrigger.OnAttackTrigger += OnCurrentSkill;
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastTimeAttack=Time.time;
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
