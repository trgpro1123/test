using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeforceRestoration_Skill : Skill
{

    [SerializeField] private UI_SkillTreeSlot lifeforceRestorationUnlockButton;
    public bool lifeforceRestorationUnlocked{get;private set;}



    protected override void Start()
    {
        base.Start();
        lifeforceRestorationUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockManasGrace);
    }
    public void UnlockManasGrace(){
        if(lifeforceRestorationUnlockButton.unlock){
            lifeforceRestorationUnlocked=true;
        }
    }


    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            AudioManager.instance.PlaySFX(15);
            playerStats.IncreaseHealBy(Mathf.RoundToInt(playerStats.GetMaxHealth()*0.1f));
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockManasGrace();
    }
}
