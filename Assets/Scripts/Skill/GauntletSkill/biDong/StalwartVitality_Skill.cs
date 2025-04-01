using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StalwartVitality_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot stalwartVitalityButton;
    public bool stalwartVitalityUnlocked{get; private set;}

    private Button button;
    protected override void Start()
    {
        base.Start();
        button=GetComponent<Button>();
        button?.GetComponent<Button>().onClick.AddListener(UnlockStalwartVitality);

    }
    public void UnlockStalwartVitality(){
        if(button==null) return;
        if(stalwartVitalityButton.unlock&&stalwartVitalityUnlocked==false){
            player.charaterStats.health.AddModifier(100);
            Inventory.instance.UpdateStatsUI();
            stalwartVitalityUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockStalwartVitality();
    }
}
