using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeavyCutter_Skill : Skill
{

    [SerializeField] private UI_SkillTreeSlot heavyCutterUnlockButton;
    public bool heavyCutterUnlocked{get;private set;}


    protected override void Start()
    {
        heavyCutterUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockHeavyCutter);
        base.Start();
    }
    public void UnlockHeavyCutter(){
        if(heavyCutterUnlockButton.unlock){
            heavyCutterUnlocked=true;
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();
        player.PlayerCreateAreaAttack(skillObject,StatType.strength, skillDamage,percentExtraDamageOfSkill, skillDistance,player.AnglePlayerToMouse());
        AudioManager.instance.PlaySFX(4);
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            player.heavyCutterAttackState.SetCurrentSkill(UseSkill);
            player.stateMachine.ChangeState(player.heavyCutterAttackState);
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockHeavyCutter();
    }
}
