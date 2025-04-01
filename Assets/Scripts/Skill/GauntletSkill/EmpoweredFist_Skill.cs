using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmpoweredFist_Skill : Skill
{

    [SerializeField] private UI_SkillTreeSlot empoweredFistUnlockButton;
    public bool empoweredFistUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        empoweredFistUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockEEmpoweredFist);
    }
    public void UnlockEEmpoweredFist(){
        if(empoweredFistUnlockButton.unlock){
            empoweredFistUnlocked=true;
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();
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
        UnlockEEmpoweredFist();
    }
}
