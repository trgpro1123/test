using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaReservoir_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot manaReservoirButton;
    public bool manaReservoirUnlocked{get; private set;}
    private Button button;

    protected override void Start()
    {
        base.Start();
        button=GetComponent<Button>();
        button?.GetComponent<Button>().onClick.AddListener(UnlockManaReservoir);

    }
    public void UnlockManaReservoir(){
        if(button==null) return;
        if(manaReservoirButton.unlock&&manaReservoirUnlocked==false){
            player.charaterStats.magicPower.AddModifier(10);
            Inventory.instance.UpdateStatsUI();
            manaReservoirUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockManaReservoir();
    }
}
