using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwordAura_Skill : Skill
{
    [SerializeField] private UI_SkillTreeSlot swordAuraUnlockButton;
    public bool swoAuraUnlocked{get;private set;}


    protected override void Start()
    {
        swordAuraUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockSlash);
        base.Start();
    }
    public void UnlockSlash(){
        if(swordAuraUnlockButton.unlock){
            swoAuraUnlocked=true;
        }
    }
    public override void UseSkill()
    {
        base.UseSkill();
        AudioManager.instance.PlaySFX(1);
        player.PlayerCreateAreaAttack(skillObject,StatType.strength, skillDamage,percentExtraDamageOfSkill, skillDistance,player.AnglePlayerToMouse());
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            player.primarySwordAttackState.SetCurrentSkill(UseSkill);
            player.stateMachine.ChangeState(player.primarySwordAttackState);
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockSlash();
    }
}
