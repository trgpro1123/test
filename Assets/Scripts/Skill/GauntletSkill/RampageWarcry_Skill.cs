using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RampageWarcry_Skill : Skill
{

    [Range(0,1)]
    public float strengthPercent;
    [Range(0,1)]
    public float armorPercent;
    [Range(0,1)]
    public float magicResistancePercent;
    public float duration;



    [SerializeField] private UI_SkillTreeSlot rampageWarcryUnlockButton;
    public bool rampageWarcryUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        rampageWarcryUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockRampageWarcry);
    }
    public void UnlockRampageWarcry(){
        if(rampageWarcryUnlockButton.unlock){
            rampageWarcryUnlocked=true;
        }
    }


    
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            AudioManager.instance.PlaySFX(10);
            // UI.instance.ingameUI.CreateStatus(iconStatus, "Strength: "+Mathf.RoundToInt(playerStats.strength.GetValue()*strengthPercent)+"\nArmor: "+Mathf.RoundToInt(playerStats.armor.GetValue()*armorPercent)+"\nMagic Resistance: "+Mathf.RoundToInt(playerStats.magicResistance.GetValue()*magicResistancePercent),duration);
            UI.instance.ingameUI.CreateStatus(iconStatus,"Rampage Warcry",statusKey,duration);
            playerStats.IncreaseStatBy(Mathf.RoundToInt(playerStats.strength.GetValue()*strengthPercent),duration,playerStats.strength);
            playerStats.IncreaseStatBy(Mathf.RoundToInt(playerStats.armor.GetValue()*armorPercent),duration,playerStats.armor); 
            playerStats.IncreaseStatBy(Mathf.RoundToInt(playerStats.magicResistance.GetValue()*magicResistancePercent),duration,playerStats.magicResistance);
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockRampageWarcry();
    }
}
