using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObliteratingPunch_Skill : Skill
{

    [SerializeField] private UI_SkillTreeSlot obliteratingPunchUnlockButton;
    public bool obliteratingPunchUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        obliteratingPunchUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockObliteratingPunch);
    }
    public void UnlockObliteratingPunch(){
        if(obliteratingPunchUnlockButton.unlock){
            obliteratingPunchUnlocked=true;
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();
        AudioManager.instance.PlaySFX(11);
        player.PlayerCreateAreaAttack(skillObject,StatType.strength, skillDamage,percentExtraDamageOfSkill, skillDistance,player.AnglePlayerToMouse());
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            player.gauntletSkillAttackState.SetCurrentSkill(UseSkill);
            player.stateMachine.ChangeState(player.gauntletSkillAttackState);
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockObliteratingPunch();
    }
}
