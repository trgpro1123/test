using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownReduction_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot cooldownReductionButton;
    public bool cooldownReductionUnlocked{get; private set;}

    private Button button;
    protected override void Start()
    {
        base.Start();
        button=GetComponent<Button>();
        button?.GetComponent<Button>().onClick.AddListener(UnlockCooldownReduction);

    }
    public void UnlockCooldownReduction(){
        if(button==null) return;
        if(cooldownReductionButton.unlock&&cooldownReductionUnlocked==false){
            player.charaterStats.coolDown.AddModifier(5);
            Inventory.instance.UpdateStatsUI();
            cooldownReductionUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockCooldownReduction();
    }
}
