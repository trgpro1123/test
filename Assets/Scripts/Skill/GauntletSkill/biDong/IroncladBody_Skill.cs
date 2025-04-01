using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IroncladBody_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot ironcladBodyButton;
    public bool ironcladBodyUnlocked{get; private set;}
    private Button button;

    protected override void Start()
    {
        base.Start();
        button=GetComponent<Button>();
        button?.GetComponent<Button>().onClick.AddListener(UnlockIroncladBody);

    }
    public void UnlockIroncladBody(){
        if(button==null) return;
        if(ironcladBodyButton.unlock&&ironcladBodyUnlocked==false){
            player.charaterStats.armor.AddModifier(10);
            Inventory.instance.UpdateStatsUI();
            ironcladBodyUnlocked=true;
        }
    }   
    protected override void CheckUnlock()
    {
        UnlockIroncladBody();
    }
}
