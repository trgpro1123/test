using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwiftDash_Skill : Skill
{

    public float swiftDashSpeed;
    public float swiftDashTime;
    [SerializeField] private UI_SkillTreeSlot swiftDashUnlockButton;
    public bool swiftDashUnlocked{get;private set;}


    protected override void Start()
    {
        swiftDashUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockSwiftDash);
        base.Start();
    }
    public void UnlockSwiftDash(){
        if(swiftDashUnlockButton.unlock){
            swiftDashUnlocked=true;
        }
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            SwiftDash(player,skillDamage,percentExtraDamageOfSkill,swiftDashSpeed,swiftDashTime);
            player.stateMachine.ChangeState(player.swordDashSkillState);
            AudioManager.instance.PlaySFX(2);
        }
        
    }

    public void SwiftDash(Player _player,int _damage,float _percentExtraDamageOfSkill, float _swiftDashSpeed, float _swiftDashTime){
        
        GameObject newSwiftDash=Instantiate(skillObject,player.attackCheck.position,Quaternion.identity);
        newSwiftDash.GetComponent<SwiftDash_Controller>().SetSwiftDash(_player,_damage,_percentExtraDamageOfSkill,_swiftDashSpeed,_swiftDashTime);
        newSwiftDash.transform.parent=player.transform;
    }
    protected override void CheckUnlock()
    {
        UnlockSwiftDash();
    }
}
