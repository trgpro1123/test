using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBoost_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot powerBoostButton;
    public bool powerBoostUnlocked{get; private set;}
    private Button button;

    protected override void Start()
    {
        base.Start();
        button=GetComponent<Button>();
        button?.onClick.AddListener(UnlockPowerBoost);

    }
    public void UnlockPowerBoost(){
        if(button==null) return;
        if(powerBoostButton.unlock&&powerBoostUnlocked==false){
            player.charaterStats.strength.AddModifier(30);
            Inventory.instance.UpdateStatsUI();
            powerBoostUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockPowerBoost();
    }
}
