using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slash_Skill : Skill
{
    [SerializeField] private UI_SkillTreeSlot slashUnlockButton;
    public bool slahsUnlocked{get;private set;}

    protected override void Awake() {
        slashUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockSlash);
        
    }
    protected override void Start()
    {
        base.Start();
    }
    public void UnlockSlash(){
        if(slashUnlockButton==null){
            Debug.Log("null");
        }
        if(slashUnlockButton.unlock){
            slahsUnlocked=true;
        }
    }
    public override void UseSkill()
    {
        base.UseSkill();
        player.PlayerCreateAreaAttack(skillObject,StatType.strength, skillDamage,percentExtraDamageOfSkill, skillDistance,player.AnglePlayerToMouse());
        AudioManager.instance.PlaySFX(1);
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

