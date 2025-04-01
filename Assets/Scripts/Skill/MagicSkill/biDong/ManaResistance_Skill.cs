using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaResistance_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot manaResistanceButton;
    public bool manaResistanceUnlocked{get; private set;}
    private Button button;

    protected override void Start()
    {
        base.Start();
        button=GetComponent<Button>();
        button?.GetComponent<Button>().onClick.AddListener(UnlockManaResistance);

    }
    public void UnlockManaResistance(){
        if(button==null) return; 
        if(manaResistanceButton.unlock&&manaResistanceUnlocked==false){
            player.charaterStats.magicResistance.AddModifier(10);
            Inventory.instance.UpdateStatsUI();
            manaResistanceUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockManaResistance();
    }
}
