using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Luckyyy_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot luckyyyButton;
    public bool luckyyyUnlocked{get; private set;}
    private Button button;

    protected override void Start()
    {
        base.Start();
        button=GetComponent<Button>();
        button?.GetComponent<Button>().onClick.AddListener(UnlockLuckyyy);

    }
    public void UnlockLuckyyy(){
        if(button==null) return;
        if(luckyyyButton.unlock&&luckyyyUnlocked==false){
            player.charaterStats.luck.AddModifier(5);
            Inventory.instance.UpdateStatsUI();
            luckyyyUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockLuckyyy();
    }
}
