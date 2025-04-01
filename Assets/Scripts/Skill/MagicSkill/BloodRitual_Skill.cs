using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodRitual_Skill : Skill
{
    [Range(0,1)]
    public float healthDecreasePercent;
    [Range(0,1)]
    public float strengtPercent;
    [Range(0,1)]
    public float magicPowerPercent;
    public float duration;


    [SerializeField] private UI_SkillTreeSlot bloodRitualUnlockButton;
    public bool bloodRitualUnlocked{get;private set;}
    



    protected override void Start()
    {
        base.Start();
        bloodRitualUnlockButton?.GetComponent<Button>().onClick.AddListener(BloodRitualGrace);
    }
    public void BloodRitualGrace(){
        if(bloodRitualUnlockButton.unlock){
            bloodRitualUnlocked=true;
        }
    }


    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            int healthDecreaseValue = Mathf.RoundToInt(playerStats.health.GetValue()*healthDecreasePercent);
            int strengthValue = Mathf.RoundToInt(playerStats.strength.GetValue()*strengtPercent);
            int magicPowerValue = Mathf.RoundToInt(playerStats.magicPower.GetValue()*magicPowerPercent);
            playerStats.IncreaseStatBy(strengthValue,duration,playerStats.strength);
            playerStats.IncreaseStatBy(magicPowerValue,duration,playerStats.magicPower);
            playerStats.DecreaseHealthBy(healthDecreaseValue);
            UI.instance.ingameUI.CreateStatus(iconStatus,"Blood Ritual",statusKey,duration);
        }
        
    }
    protected override void CheckUnlock()
    {
        BloodRitualGrace();
    }
}
