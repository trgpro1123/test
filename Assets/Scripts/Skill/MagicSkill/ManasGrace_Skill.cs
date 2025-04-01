using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManasGrace_Skill : Skill
{

    [SerializeField] private UI_SkillTreeSlot manasGraceUnlockButton;
    public bool manasGraceUnlocked{get;private set;}



    protected override void Start()
    {
        base.Start();
        manasGraceUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockManasGrace);
    }
    public void UnlockManasGrace(){
        if(manasGraceUnlockButton.unlock){
            manasGraceUnlocked=true;
        }
    }


    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            AudioManager.instance.PlaySFX(15);
            playerStats.IncreaseManaBy(playerStats.GetMaxMana());
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockManasGrace();
    }
}
