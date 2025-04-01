using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyFlow_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot energyFlowButton;
    public bool energyFlowUnlocked{get; private set;}
    private Button button;

    protected override void Start()
    {
        base.Start();
        button=GetComponent<Button>();
        button?.GetComponent<Button>().onClick.AddListener(UnlockEnergyFlow);

    }
    public void UnlockEnergyFlow(){
        if(button==null) return;
        if(energyFlowButton.unlock&&energyFlowUnlocked==false){
            player.charaterStats.magicRegeneration.AddModifier(10);
            Inventory.instance.UpdateStatsUI();
            energyFlowUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockEnergyFlow();
    }
}
