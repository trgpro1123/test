using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CriticalRush_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot criticalRushButton;
    public bool criticalRushUnlocked{get; private set;}
    private Button button;

    protected override void Start()
    {
        base.Start();
        button=GetComponent<Button>();
        button?.onClick.AddListener(UnlockCriticalRush);

    }
    public void UnlockCriticalRush(){
        if(button==null) return;
        if(criticalRushButton.unlock&&criticalRushUnlocked==false){
            player.charaterStats.critChance.AddModifier(5);
            Inventory.instance.UpdateStatsUI();
            criticalRushUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockCriticalRush();
    }
}
