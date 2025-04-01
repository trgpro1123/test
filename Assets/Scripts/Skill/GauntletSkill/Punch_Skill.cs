using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Punch_Skill : Skill
{


    [SerializeField] private UI_SkillTreeSlot PunchUnlockButton;
    public bool punchUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        PunchUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockPunch);
    }
    public void UnlockPunch(){
        if(PunchUnlockButton.unlock){
            punchUnlocked=true;
        }
    }
    public override void UseSkill()
    {
        base.UseSkill();
        AudioManager.instance.PlaySFX(9);
        player.PlayerCreateAreaAttack(skillObject,StatType.strength, skillDamage,percentExtraDamageOfSkill, skillDistance,player.AnglePlayerToMouse());
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            player.gauntletPrimaryAttackState.SetCurrentSkill(UseSkill);
            player.stateMachine.ChangeState(player.gauntletPrimaryAttackState);
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockPunch();
    }
}
